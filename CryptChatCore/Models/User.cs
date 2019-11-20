using System;
using System.Collections.Generic;
using System.Text;

namespace CryptChatCore.Models
{
    class User
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string PublicKey { get; set; }

        public User(string username, string token, string publickey)
        {
            this.Username = username;
            this.Token = token;
            this.PublicKey = publickey;
        }
        
    }
}
