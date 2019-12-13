using System;
using MongoDB.Bson;
using MongoDB.Driver;

using CryptChat.Core;

namespace CryptChatServerTCP
{
    public static class Program
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
            Logger.Debug("Setting up MongoDB");
            InitMongo();
            Logger.Info($"Starting server at {Globals.CONFIG.Server.BindIP}:{Globals.CONFIG.Server.Port}");
            //StartServer();
            CryptChat.Core.Security.Utils.LoadMemoryKey();
            StartServer();
            /*
            foreach (var x in CryptChatProtos.Requests.Message.MessageReflection.Descriptor.MessageTypes)
            {
                Console.WriteLine(x.Name);
            }
            */
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

        public static void StartServer()
        {
            Logger.Debug("Building server");
            Server server;
            if (string.IsNullOrEmpty(Globals.CONFIG.Server.Certpath))
                server = new TCPServer(Globals.CONFIG.Server.BindIP, Globals.CONFIG.Server.Port, Globals.CONFIG.Server.Autostart);
            else
                try
                {
                    server = new SSLServer(Globals.CONFIG.Server.BindIP, Globals.CONFIG.Server.Port, Globals.CONFIG.Server.Autostart, Globals.CONFIG.Server.Certpath);
                }
                catch (Exception e)
                {
                    Logger.Error($"Failed to create SSL server. Falling back to TCPServer. Reason: {e.Message}");
                    server = new TCPServer(Globals.CONFIG.Server.BindIP, Globals.CONFIG.Server.Port, Globals.CONFIG.Server.Autostart);
                }
            if (!Globals.CONFIG.Server.Autostart)
                Logger.Debug("Server not set to autostart. Starting listener");
                server.StartListener();
        }
    }
}
