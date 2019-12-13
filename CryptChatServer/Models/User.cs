using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace CryptChat.Server.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Username { get; set; }
        public string PublicKey { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public string Token { get; set; }
    }
}
