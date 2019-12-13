using System.Collections.Generic;

using MongoDB.Bson;

namespace CryptChat.Server.Types
{
    public class Message
    {
        public ObjectId _id { get; set; }
        public ObjectId chat { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> key { get; set; }
        public string nonce { get; set; }
        public double timestamp { get; set; }
        public string sender { get; set; }
        public bool edited { get; set; }
        public double edited_at { get; set; }
        public string file { get; set; }

        public Message() { }
    }
}
