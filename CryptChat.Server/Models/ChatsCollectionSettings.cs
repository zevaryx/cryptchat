using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptChat.Server.Models
{
    public class ChatsCollectionSettings : IChatsCollectionSettings
    {
        public string ChatsCollectionName { get; set; }
    }

    public interface IChatsCollectionSettings
    {
        string ChatsCollectionName { get; set; }
    }
}
