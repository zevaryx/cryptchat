using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptChat.Server.Models
{
    public class MessagesCollectionSettings : IMessagesCollectionSettings
    {
        public string MessagesCollectionName { get; set; }
    }

    public interface IMessagesCollectionSettings
    {
        string MessagesCollectionName { get; set; }
    }
}
