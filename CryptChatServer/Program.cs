using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CryptChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Globals.CONFIG = FileIO.GetConfigFromFile();
            init_mongo();
        }

        static void init_mongo()
        {
            var creds = MongoCredential.CreateCredential("admin", Globals.CONFIG.MongoDB.User, Globals.CONFIG.MongoDB.Passwd);
            var settings = new MongoClientSettings { Credential = creds, Server = new MongoServerAddress(Globals.CONFIG.MongoDB.Host) };
            Globals.MONGO_CLIENT = new MongoClient(settings);
            Globals.MONGO_DATABASE = Globals.MONGO_CLIENT.GetDatabase(Globals.CONFIG.MongoDB.Database);
            Globals.USERS = Globals.MONGO_DATABASE.GetCollection<Types.User>("user");
            Globals.MESSAGES = Globals.MONGO_DATABASE.GetCollection<Types.Message>("message");
            Globals.CHATS = Globals.MONGO_DATABASE.GetCollection<Types.Chat>("chat");
        }
    }
}
