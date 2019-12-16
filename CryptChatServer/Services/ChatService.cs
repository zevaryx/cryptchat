using CryptChat.Server.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptChat.Server.Services
{
    public class ChatService
    {
        private readonly IMongoCollection<Chat> _chats;

        public ChatService(IMongoDatabaseSettings dbsettings, IChatsCollectionSettings settings)
        {
            var client = new MongoClient(dbsettings.ConnectionString);
            var database = client.GetDatabase(dbsettings.DatabaseName);

            _chats = database.GetCollection<Chat>(settings.ChatsCollectionName);
        }

        public List<Chat> Get() =>
            _chats.Find<Chat>(chat => true).ToList();

        public List<Chat> GetUserChats(string userID) =>
            _chats.Find<Chat>(chat => chat.Members.Contains(userID)).ToList();

        public Chat Get(string id) =>
            _chats.Find<Chat>(chat => chat.Id == id).FirstOrDefault();

        public Chat GetFromUsers(string[] userIDs) =>
            _chats.Find<Chat>(chat => chat.Members == userIDs).FirstOrDefault();

        public Chat Create(Chat chat)
        {
            _chats.InsertOne(chat);
            return chat;
        }

        public void Update(string id, Chat chatIn) =>
            _chats.ReplaceOne(chat => chat.Id == id, chatIn);

        public void Remove(Chat chatIn) =>
            _chats.DeleteOne(chat => chat.Id == chatIn.Id);

        public void Remove(string id) =>
            _chats.DeleteOne(chat => chat.Id == id);
    }
}
