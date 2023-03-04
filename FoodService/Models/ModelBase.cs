using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace FoodService.Models;

public class ModelBase
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = ObjectId.GenerateNewId().ToString();
    public string Name { get; set; } = string.Empty;
    public bool Deleted { get; set; }
}
