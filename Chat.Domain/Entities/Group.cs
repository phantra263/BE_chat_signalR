using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Chat.Domain.Entities
{
    public class Group : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        [JsonProperty("Name")]
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        [JsonProperty("Status")]
        public bool Status { get; set; }
    }
}
