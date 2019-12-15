using Sodium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

using CryptChat.Core.Security;

namespace CryptChat.Core.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public SecureString Token { get; set; }
        public string PublicKey { get; set; }
        public SecureString PrivateKey { get; private set; }
        
        public void LoadPrivateKey(SecureString password, bool save = true, string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = (Environment.OSVersion.Platform == PlatformID.Unix) ?
                        Environment.GetEnvironmentVariable("HOME") :
                        Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
                path += "/.cryptchat/" + Base32.ToBase32String(GenericHash.Hash(Encoding.ASCII.GetBytes(Username), null, 16)) + "/.pem";
            }

            if (!File.Exists(path))
            {
                Console.WriteLine("Generating keypair");
                using (KeyPair kp = Security.Boxes.Asymmetric.GenerateKeyPair())
                {
                    Console.WriteLine("Store PrivateKey as mem-locked Base64 string");
                    PrivateKey = Convert.ToBase64String(kp.PrivateKey);
                    Console.WriteLine("Store PublicKey as Base64 string");
                    PublicKey = Convert.ToBase64String(kp.PublicKey);
                    if (save)
                        SavePrivateKey(password);
                }
            }

            else
            {
                var cipher_file = File.ReadAllBytes(path);
                PrivateKey = Convert.ToBase64String(SecretBox.Open(cipher_file,
                                                    GenericHash.Hash(Encoding.ASCII.GetBytes(Id), null, 24),
                                                    GenericHash.Hash(Encoding.ASCII.GetBytes(password), null, 32)));
                using (var kpair = PublicKeyBox.GenerateKeyPair(Convert.FromBase64String(PrivateKey.ToString())))
                {
                    PublicKey = Convert.ToBase64String(kpair.PublicKey);
                }
            }
        }

        public void SavePrivateKey(SecureString password, string path = null)
        {
            Console.WriteLine("Set path if needed");
            if (string.IsNullOrEmpty(path))
            {
                path = (Environment.OSVersion.Platform == PlatformID.Unix) ?
                        Environment.GetEnvironmentVariable("HOME") :
                        Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
                path += "/.cryptchat/" + Base32.ToBase32String(GenericHash.Hash(Encoding.ASCII.GetBytes(Username), null, 16));
            }
            Console.WriteLine("Create directory");
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            path += "/.pem";
            Console.WriteLine("Lock PrivateKey with password");
            var temp = SecretBox.Create(Convert.FromBase64String(PrivateKey), 
                                        GenericHash.Hash(Encoding.ASCII.GetBytes(Id), null, 24), 
                                        GenericHash.Hash(Encoding.ASCII.GetBytes(password), null, 32));
            var temp_key = Convert.ToBase64String(temp);
            Console.WriteLine("Save password-locked PrivateKey");
            if (!string.IsNullOrEmpty(temp_key))
                File.WriteAllBytes(path, Convert.FromBase64String(temp_key));
        }
    }
}
