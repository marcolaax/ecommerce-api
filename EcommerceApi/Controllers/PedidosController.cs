using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using SkateStore.Models;

namespace SkateStore.Controllers;

[ApiController]
[Route("api/pedidos")]
public class PedidosController : ControllerBase
{
    private readonly IMongoCollection<Pedido> _col;

    public PedidosController(IMongoDatabase db)
    {
        _col = db.GetCollection<Pedido>("pedidos");
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
        if (item is null) return NotFound(new { message = $"Pedido {id} não encontrado." });
        return Ok(item);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] Pedido pedido)
    {
        await _col.InsertOneAsync(pedido);
        return CreatedAtAction(nameof(Buscar), new { id = pedido.Id }, pedido);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Atualizar(string id, [FromBody] Pedido pedido)
    {
        var result = await _col.ReplaceOneAsync(p => p.Id == id, pedido);
        if (result.MatchedCount == 0) return NotFound(new { message = $"Pedido {id} não encontrado." });
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Deletar(string id)
    {
        var result = await _col.DeleteOneAsync(p => p.Id == id);
        if (result.DeletedCount == 0) return NotFound(new { message = $"Pedido {id} não encontrado." });
        return NoContent();
    }

    [HttpPatch("{id}/status")]
    public async Task<IActionResult> AtualizarStatus(string id, [FromBody] StatusRequest req)
    {
        var pedido = await _col.Find(p => p.Id == id).FirstOrDefaultAsync();
        if (pedido is null) return NotFound(new { message = $"Pedido {id} não encontrado." });
        pedido.Status = req.Status;
        await _col.ReplaceOneAsync(p => p.Id == id, pedido);
        return NoContent();
    }
}

public record StatusRequest(string Status);