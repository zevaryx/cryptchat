using MongoDB.Bson;
using MongoDB.Driver;

using CryptChatServerTCP.Types;

namespace CryptChatServerTCP.Utils
{
    class Tokens
    {
        public static bool FindToken(string token)
        {
            var filter = Builders<User>.Filter.Eq(x => x.token, token);
            User document = Globals.USERS.Find(filter).First();
            return document != null;
        }

        public static User GetTokenOwner(string token)
        {
            var filter = Builders<User>.Filter.Eq(x => x.token, token);
            User document = Globals.USERS.Find(filter).First();
            return document;
        }

        public static bool CheckOwnership(string token, string username)
        {
            var filter = Builders<User>.Filter.Eq(x => x.token, token) 
                       & Builders<User>.Filter.Eq(x => x.username, username);
            User document = Globals.USERS.Find(filter).First();
            return document != null;
        }

        public static string GetToken(ObjectId _id)
        {
            var filter = Builders<User>.Filter.Eq(x => x._id, _id);
            User document = Globals.USERS.Find(filter).First();
            return document.token;
        }
    }
}
