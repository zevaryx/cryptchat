using System.Collections.Generic;

using MongoDB.Bson;

namespace CryptChat.Server.Types
{
    public class Chat
    {
        public List<string> members { get; set; }
        public ObjectId _id { get; set; }
        public int msg_count { get; set; }
        public Dictionary<string, Queue> queue { get; set; }

        public Chat() { }
    }
}
