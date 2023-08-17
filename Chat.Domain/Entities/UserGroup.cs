using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Chat.Domain.Entities
{
    public class UserGroup : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("UserId")]
        public string UserId { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("GroupId")]
        public string GroupId { get; set; }
    }
}
