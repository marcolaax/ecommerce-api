using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkateStore.Models;

public class Pedido
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string ProdutoId { get; set; } = string.Empty;
    public int Quantidade { get; set; }
    public string Status { get; set; } = "pendente";
}