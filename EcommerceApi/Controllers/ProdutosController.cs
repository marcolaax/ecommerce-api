using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SkateStore.Models;

namespace SkateStore.Controllers;

[ApiController]
[Route("api/produtos")]
public class ProdutosController : ControllerBase
{
    private readonly IMongoCollection<Produto> _col;

    public ProdutosController(IMongoDatabase db)
    {
        _col = db.GetCollection<Produto>("produtos");
    }

    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        var lista = await _col.Find(_ => true).ToListAsync();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Buscar(string id)
    {
        var item = await _col.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (item is null) return NotFound(new { message = $"Produto {id} não encontrado." });
        return Ok(item);
    }

    [HttpGet("categoria/{categoria}")]
    public async Task<IActionResult> BuscarPorCategoria(string categoria)
    {
        var lista = await _col.Find(p => p.Categoria == categoria).ToListAsync();
        return Ok(lista);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Produto produto)
    {
        await _col.InsertOneAsync(produto);
        return CreatedAtAction(nameof(Buscar), new { id = produto.Id }, produto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(string id, [FromBody] Produto produto)
    {
        var result = await _col.ReplaceOneAsync(p => p.Id == id, produto);
        if (result.MatchedCount == 0) return NotFound(new { message = $"Produto {id} não encontrado." });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(string id)
    {
        var result = await _col.DeleteOneAsync(p => p.Id == id);
        if (result.DeletedCount == 0) return NotFound(new { message = $"Produto {id} não encontrado." });
        return NoContent();
    }
}