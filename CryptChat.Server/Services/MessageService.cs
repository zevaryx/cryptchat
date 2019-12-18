using CryptChat.Server.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptChat.Server.Services
{
    public class MessageService
    {
        private readonly IMongoCollection<Message> _messages;

        public MessageService(IMongoDatabaseSettings dbsettings, IMessagesCollectionSettings settings)
        {
            var client = new MongoClient(dbsettings.ConnectionString);
            var database = client.GetDatabase(dbsettings.DatabaseName);

            _messages = database.GetCollection<Message>(settings.MessagesCollectionName);
        }

        public List<Message> Get() =>
            _messages.Find<Message>(message => true).ToList();

        public Message Get(string id) =>
            _messages.Find<Message>(message => message.Id == id).FirstOrDefault();

        public List<Message> GetUserMessages(string id) =>
            _messages.Find<Message>(message => message.Keys.ContainsKey(id)).ToList();

        public List<Message> GetChatMessages(string id) =>
            _messages.Find<Message>(message => message.Chat == id).ToList();

        public Message Create(Message message)
        {
            _messages.InsertOne(message);
            return message;
        }

        public void Update(string id, Message messageIn) =>
            _messages.ReplaceOne(message => message.Id == id, messageIn);

        public void Remove(Message messageIn) =>
            _messages.DeleteOne(message => message.Id == messageIn.Id);

        public void Remove(string id) =>
            _messages.DeleteOne(message => message.Id == id);
    }
}
