using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace CryptChatServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Config config = FileIO.GetConfigFromFile();
            var creds = MongoCredential.CreateCredential("admin", config.MongoDB.User, config.MongoDB.Passwd);
            var settings = new MongoClientSettings { Credential = creds, Server = new MongoServerAddress(config.MongoDB.Host) };
            Globals.MONGO_CLIENT = new MongoClient(settings);
            Globals.MONGO_DATABASE = Globals.MONGO_CLIENT.GetDatabase(config.MongoDB.Database);
            Globals.USERS = Globals.MONGO_DATABASE.GetCollection<Types.User>("user");
            Globals.MESSAGES = Globals.MONGO_DATABASE.GetCollection<Types.Message>("message");
            Globals.CHATS = Globals.MONGO_DATABASE.GetCollection<Types.Chat>("chat");

            var cursor = Globals.CHATS.Find(new BsonDocument()).ToCursor();
            foreach (var document in cursor.ToEnumerable())
            {
                Console.WriteLine(document.ToJson());
            }
        }
    }
}
