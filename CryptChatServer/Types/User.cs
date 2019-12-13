using MongoDB.Bson;

namespace CryptChat.Server.Types
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
}
