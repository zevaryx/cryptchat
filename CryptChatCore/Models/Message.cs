using System;
using System.Collections.Generic;
using System.Text;

namespace CryptChat.Core.Models
{
    class Message
    {
        public string ID { get; set; }
        public string Chat { get; set; }
        public string EncryptedMessage { get; set; }
        public string EncryptedKey { get; set; }
        public string Nonce { get; set; }
        public double Timestamp { get; set; }
        public string Sender { get; set; }
        public bool Edited { get; set; }
        public double Edited_at { get; set; }
        public string EncryptedFile { get; set; }

        public Message(
            string id, string chat, string encryptedmessage, 
            string encryptedkey, string nonce, double timestamp, 
            string sender, bool edited, double edited_at, string encryptedfile
            )
        {
            this.ID = id;
            this.Chat = chat;
            this.EncryptedMessage = encryptedmessage;
            this.EncryptedKey = encryptedkey;
            this.Nonce = nonce;
            this.Timestamp = timestamp;
            this.Sender = sender;
            this.Edited = edited;
            this.Edited_at = edited_at;
            this.EncryptedFile = encryptedfile;
        }

        public string GetPlaintext(string privatekey, string publickey)
        {
            if (!Security.Utils.IsBase64(privatekey))
            {
                throw new ArgumentException($"{nameof(privatekey)} must be Base64-encoded string");
            }
            if (!Security.Utils.IsBase64(publickey))
            {
                throw new ArgumentException($"{nameof(publickey)} must be Base64-encoded string");
            }
            var key = Security.Boxes.Asymmetric.Decrypt(EncryptedKey, privatekey, publickey, Nonce);
            return Security.Boxes.Symmetric.Decrypt(EncryptedMessage, key, Nonce);
        }
    }
}
