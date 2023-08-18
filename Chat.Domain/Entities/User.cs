using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace Chat.Domain.Entities
{
    public class User : BaseEntity
    {
        [BsonRepresentation(BsonType.String)]
        [JsonProperty("Nickname")]
        public string Nickname { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonProperty("Password")]
        public string Password { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonProperty("AvatarBgColor")]
        public string AvatarBgColor { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonProperty("AvatarId")]
        public string AvatarId { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonProperty("AnonymousName")]
        public string AnonymousName { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        [JsonProperty("Status")]
        public bool Status { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        [JsonProperty("IsOnline")]
        public bool IsOnline { get; set; }
    }
}
