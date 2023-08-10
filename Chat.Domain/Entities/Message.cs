using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Chat.Domain.Entities
{
    public class Message
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
        [JsonProperty("ConversationId")]
        public string ConversationId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("SenderId")]
        public string SenderId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("ReceiverId")]
        public string ReceiverId { get; set; }

        [JsonProperty("Content")]
        public string Content { get; set; }

        [JsonProperty("IsSeen")]
        public bool IsSeen { get; set; }
    }
}
