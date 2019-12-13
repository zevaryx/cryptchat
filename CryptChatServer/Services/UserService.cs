using CryptChat.Server.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptChat.Server.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;

        public UserService(IMongoDatabaseSettings dbsettings, IUsersCollectionSettings settings)
        {
            var client = new MongoClient(dbsettings.ConnectionString);
            var database = client.GetDatabase(dbsettings.DatabaseName);

            _users = database.GetCollection<User>(settings.UsersCollectionName);
        }

        public List<User> Get() => 
            _users.Find<User>(user => true).ToList();

        public User Get(string id) =>
            _users.Find<User>(user => user.Id == id).FirstOrDefault();

        public User GetFromToken(string token) =>
            _users.Find<User>(user => user.Token == token).FirstOrDefault();

        public User GetFromUsername(string username) =>
            _users.Find<User>(user => user.Username == username).FirstOrDefault();

        public User Create(User user)
        {
            _users.InsertOne(user);
            return user;
        }

        public void Update(string id, User userIn) =>
            _users.ReplaceOne(user => user.Id == id, userIn);

        public void Remove(User userIn) =>
            _users.DeleteOne(user => user.Id == userIn.Id);

        public void Remove(string id) =>
            _users.DeleteOne(user => user.Id == id);
    }
}
