using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace CryptChatServer
{
    public class Program
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        static void Main(string[] args)
        {
            Logger.Debug("Getting config...");
            try
            {
                Globals.CONFIG = FileIO.GetConfigFromFile();
            }
            catch (Exception e)
            {
                Logger.Fatal($"Loading config failed. Reason: {e.Message}");
                Environment.Exit(1);
            }
            InitMongo();
            foreach (var x in CryptChatProtos.Requests.Message.MessageReflection.Descriptor.MessageTypes)
            {
                Console.WriteLine(x.Name);
            }
        }

        public static void InitMongo()
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
