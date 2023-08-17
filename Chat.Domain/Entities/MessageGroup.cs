using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Chat.Domain.Entities
{
    public class MessageGroup : BaseEntity
    {
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("UserGroupId")]
        public string UserGroupId { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonProperty("Content")]
        public string Content { get; set; }
    }
}
