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
        public Dictionary<string, string> Keys { get; set; }
        public string Nonce { get; set; }
        public double Timestamp { get; set; }
        public string Sender { get; set; }
        public bool Edited { get; set; }
        public double Edited_At { get; set; }
        public string File { get; set; }
    }
}
