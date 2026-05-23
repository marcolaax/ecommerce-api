using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkateStore.Models;

public class Produto
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty;
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
}