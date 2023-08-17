using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Chat.Domain.Entities
{
    public class BaseEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("Id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [JsonProperty("Created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [BsonRepresentation(BsonType.Boolean)]
        [JsonProperty("Deleted")]
        public bool Deleted { get; set; }
    }
}
