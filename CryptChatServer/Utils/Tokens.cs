using MongoDB.Bson;
using MongoDB.Driver;

using CryptChatServer.Types;

namespace CryptChatServer.Utils
{
    class Tokens
    {
        public static bool find_token(string token)
        {
            var filter = Builders<User>.Filter.Eq(x => x.token, token);
            User document = Globals.USERS.Find(filter).First();
            return document != null;
        }

        public static User get_token_owner(string token)
        {
            var filter = Builders<User>.Filter.Eq(x => x.token, token);
            User document = Globals.USERS.Find(filter).First();
            return document;
        }

        public static bool check_ownership(string token, string username)
        {
            var filter = Builders<User>.Filter.Eq(x => x.token, token) 
                       & Builders<User>.Filter.Eq(x => x.username, username);
            User document = Globals.USERS.Find(filter).First();
            return document != null;
        }

        public static string get_token(ObjectId _id)
        {
            var filter = Builders<User>.Filter.Eq(x => x._id, _id);
            User document = Globals.USERS.Find(filter).First();
            return document.token;
        }
    }
}
