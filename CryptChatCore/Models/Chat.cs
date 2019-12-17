using System;
using System.Collections.Generic;
using System.Text;

namespace CryptChat.Core.Models
{
    class Chat
    {
        public string Id { get; set; }
        public string[] Members { get; set; }
        public int MessageCount { get; set; }
        public Dictionary<string, ChatQueue> Queue { get; set; }
    }
}
