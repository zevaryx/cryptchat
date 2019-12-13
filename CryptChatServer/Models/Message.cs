using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptChat.Server.Models
{
    public class Message
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        [BsonRepresentation(BsonType.ObjectId)]
        public string Chat { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> key { get; set; }
        public string nonce { get; set; }
        public double timestamp { get; set; }
        public string sender { get; set; }
        public bool edited { get; set; }
        public double edited_at { get; set; }
        public string file { get; set; }
    }
}
