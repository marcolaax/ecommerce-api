using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SkateStore.Models;

public class Usuario
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }
    public string Email { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
}