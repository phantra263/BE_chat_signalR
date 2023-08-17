using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Chat.Domain.Entities
{
    public class Box : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        [JsonProperty("ConversationId")]
        public string ConversationId { get; set; }

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
