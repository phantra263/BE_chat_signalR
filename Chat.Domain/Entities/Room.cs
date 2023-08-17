using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Chat.Domain.Entities
{
    public class Room : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        [JsonProperty("Status")]
        public bool Status { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("UserId")]
        public string UserId { get; set; }
    }
}
