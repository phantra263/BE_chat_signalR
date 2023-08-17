using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Chat.Domain.Entities
{
    public class Message : BaseEntity
    {

        [BsonRepresentation(BsonType.String)]
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
