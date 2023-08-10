using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Chat.Domain.Entities
{
    public class Box
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("Id")]
        public string Id { get; set; }

        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [JsonProperty("Deleted")]
        public bool Deleted { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("User1Id")]
        public string User1Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("User2Id")]
        public string User2Id { get; set; }

        [JsonProperty("IsLock")]
        public bool IsLock { get; set; }

        [JsonProperty("IsMute")]
        public bool IsMute { get; set; }
    }
}
