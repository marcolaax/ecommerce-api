using BCrypt.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using SkateStore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SkateStore.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMongoCollection<Usuario> _col;
    private readonly IConfiguration _config;

    public AuthController(IMongoDatabase db, IConfiguration config)
    {
        _col = db.GetCollection<Usuario>("usuarios");
        _config = config;
    }

    [HttpPost("registro")]
    public async Task<IActionResult> Registro([FromBody] LoginRequest req)
    {
        var existe = await _col.Find(u => u.Email == req.Email).FirstOrDefaultAsync();

        if (existe is not null)
            return BadRequest(new { message = "Email já cadastrado." });

        var usuario = new Usuario
        {
            Email = req.Email,
            SenhaHash = BCrypt.Net.BCrypt.HashPassword(req.Senha)
        };

        await _col.InsertOneAsync(usuario);

        return Ok(new { message = "Usuário criado." });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest req)
    {
        var usuario = await _col.Find(u => u.Email == req.Email).FirstOrDefaultAsync();

        if (usuario is null ||
            !BCrypt.Net.BCrypt.Verify(req.Senha, usuario.SenhaHash))
        {
            return Unauthorized(new { message = "Email ou senha inválidos." });
        }

        var chave = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_config["Jwt:Secret"]!)
        );

        var creds = new SigningCredentials(
            chave,
            SecurityAlgorithms.HmacSha256
        );

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: new[]
            {
                new Claim(ClaimTypes.Email, usuario.Email),
                new Claim(ClaimTypes.NameIdentifier, usuario.Id!)
            },
            expires: DateTime.UtcNow.AddHours(2),
            signingCredentials: creds
        );

        return Ok(new
        {
            token = new JwtSecurityTokenHandler().WriteToken(token)
        });
    }
}

public record LoginRequest(string Email, string Senha);