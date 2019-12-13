using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptChat.Server.Models
{
    public class UsersCollectionSettings : IUsersCollectionSettings
    {
        public string UsersCollectionName { get; set; }
    }

    public interface IUsersCollectionSettings
    {
        string UsersCollectionName { get; set; }
    }
}
