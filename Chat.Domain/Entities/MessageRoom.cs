using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Chat.Domain.Entities
{
    public class MessageRoom : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        [JsonProperty("Content")]
        public string Content { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("SenderId")]
        public string SenderId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("RoomId")]
        public string RoomId { get; set; }
    }
}
