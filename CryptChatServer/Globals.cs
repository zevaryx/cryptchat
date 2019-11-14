using MongoDB.Driver;

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
    }
}
