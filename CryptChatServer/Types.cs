using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptChatServer.Types
{
    public class User
    {
        public string hash { get; set; }
        public string salt { get; set; }
        public string token { get; set; }
        public ObjectId _id { get; set; }
        public string username { get; set; }
        public string pubkey { get; set; }

        public User() { }
    }

    public class Message
    {
        public ObjectId _id { get; set; }
        public ObjectId chat { get; set; }
        public string message { get; set; }
        public Dictionary<string, string> key { get; set; }
        public string signature { get; set; }
        public double timestamp { get; set; }
        public string sender { get; set; }
        public bool edited { get; set; }
        public double edited_at { get; set; }
        public string file { get; set; }

        public Message() { }
    }

    public class Chat
    {
        public List<string> members { get; set; }
        public ObjectId _id { get; set; }
        public int msg_count { get; set; }
        public Dictionary<string, Queue> queue { get; set; }

        public Chat() { }
    }

    public class Queue
    {
        public List<string> deleted { get; set; }
        public List<string> edited { get; set; }
        public List<string> resync { get; set; }

        public Queue() { }
    }
}
