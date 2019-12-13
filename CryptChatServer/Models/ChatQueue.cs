using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CryptChat.Server.Models
{
    public class ChatQueue
    {
        public List<string> Deleted { get; set; }
        public List<string> Edited { get; set; }
        public List<string> Resync { get; set; }
    }
}
