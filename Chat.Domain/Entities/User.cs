using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using System;

namespace Chat.Domain.Entities
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [JsonProperty("Id")]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        [JsonProperty("Created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [BsonRepresentation(BsonType.Boolean)]
        [JsonProperty("Deleted")]
        public bool Deleted { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonProperty("Nickname")]
        public string Nickname { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonProperty("Password")]
        public string Password { get; set; }

        [BsonRepresentation(BsonType.String)]
        [JsonProperty("AvatarBgColor")]
        public string AvatarBgColor { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        [JsonProperty("Status")]
        public bool Status { get; set; }

        [BsonRepresentation(BsonType.Boolean)]
        [JsonProperty("IsOnline")]
        public bool IsOnline { get; set; }
    }
}
