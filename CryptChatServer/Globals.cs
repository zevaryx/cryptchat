using MongoDB.Driver;
using System.Collections.Concurrent;
using System.Collections.Generic;

namespace CryptChatServer
{
    public static class Globals
    {
        public static Config CONFIG { get; set; }
        public static MongoClient MONGO_CLIENT { get; set; }
        public static IMongoDatabase MONGO_DATABASE { get; set; }
        public static IMongoCollection<Types.User> USERS { get; set; }
        public static IMongoCollection<Types.Chat> CHATS { get; set; }
        public static IMongoCollection<Types.Message> MESSAGES { get; set; }

        public static ConcurrentDictionary<string, object> CLIENTS = new ConcurrentDictionary<string, object>();
        public static ConcurrentDictionary<string, string> SIDS = new ConcurrentDictionary<string, string>();
    }
}
