using System.ComponentModel.DataAnnotations;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DeLavant.Domain.Abstractions
{
    public abstract class ContentItem : IEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = ObjectId.GenerateNewId().ToString();



        [Required]
        public string Title { get; set; } = "";

        public string? Description { get; set; }
    }
}
