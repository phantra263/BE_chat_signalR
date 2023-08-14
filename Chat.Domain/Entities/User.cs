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

        [JsonProperty("Created")]
        public DateTime Created { get; set; } = DateTime.Now;

        [JsonProperty("Deleted")]
        public bool Deleted { get; set; }

        [JsonProperty("Nickname")]
        public string Nickname { get; set; }

        [JsonProperty("AvatarBgColor")]
        public string AvatarBgColor { get; set; }

        [JsonProperty("Status")]
        public bool Status { get; set; }

        [JsonProperty("IsOnline")]
        public bool IsOnline { get; set; }
    }
}
