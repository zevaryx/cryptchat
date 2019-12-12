#pragma warning disable IDE1006 // Naming Styles

using System.Collections.Generic;

namespace CryptChatServerTCP.Types
{
    public class Queue
    {
        public List<string> deleted { get; set; }
        public List<string> edited { get; set; }
        public List<string> resync { get; set; }

        public Queue() { }
    }
}
