using Sodium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace CryptChatCore.Models
{
    class User
    {
        public string UserID { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string PublicKey { get; set; }
        public string PrivateKey { get; private set; }

        public User(string userid, string username, string token)
        {
            this.UserID = userid;
            this.Username = username;
            this.Token = token;
        }
        
        public void LoadPrivateKey(string password, string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = (Environment.OSVersion.Platform == PlatformID.Unix) ?
                        Environment.GetEnvironmentVariable("HOME") :
                        Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
                path += "/.cryptchat/" + UserID + "/.pem";
            }
            if (!File.Exists(path))
            {
                using (KeyPair kp = Security.Boxes.Asymmetric.GenerateKeyPair())
                {
                    PrivateKey = Security.Utils.Lock(kp.PrivateKey);
                    PublicKey = Convert.ToBase64String(kp.PublicKey);
                    var cipher_priv = Security.Utils.Lock(kp.PrivateKey, password);
                    File.WriteAllBytes(path, Convert.FromBase64String(cipher_priv));
                }
            }
            else
            {
                var cipher_file = File.ReadAllBytes(path);
                PrivateKey = Security.Utils.Unlock(Convert.ToBase64String(cipher_file));
                var priv_bytes = Convert.FromBase64String(Security.Utils.Unlock(PrivateKey, password));
                using (var kpair = PublicKeyBox.GenerateKeyPair(priv_bytes))
                {
                    PublicKey = Convert.ToBase64String(kpair.PublicKey);
                }
                Array.Clear(priv_bytes, 0, priv_bytes.Length);
            }
        }
    }
}
