﻿using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

using CryptChat.Core.Security;

namespace CryptChat.Core.Models
{
    public class Message
    {
        public string ID { get; set; }
        public string Chat { get; set; }
        public string EncryptedMessage { get; set; }
        public string EncryptedKey { get; set; }
        public string Nonce { get; set; }
        public double Timestamp { get; set; }
        public string Sender { get; set; }
        public bool Edited { get; set; }
        public double Edited_At { get; set; }
        public string EncryptedFile { get; set; }
        public SecureString Plaintext { get; private set; }


        public void Decrypt(SecureString privatekey, string publickey)
        {
            if (!Security.Utils.IsBase64(privatekey))
            {
                throw new ArgumentException($"{nameof(privatekey)} must be Base64-encoded string");
            }
            if (!Security.Utils.IsBase64(publickey))
            {
                throw new ArgumentException($"{nameof(publickey)} must be Base64-encoded string");
            }
            using SecureString key = Security.Boxes.Asymmetric.Decrypt(EncryptedKey.Split(':')[1], privatekey, publickey, EncryptedKey.Split(':')[0]);
            Plaintext = Security.Boxes.Symmetric.Decrypt(EncryptedMessage, key, Nonce);                
        }

        public override string ToString()
        {
            DateTime tstamp = DateTimeOffset.FromUnixTimeSeconds((long)Timestamp).DateTime.ToLocalTime();
            var sb = new StringBuilder();
            sb.Append($"   Sender: {Sender}");
            sb.Append($"\nTimestamp: {tstamp}");
            if (!string.IsNullOrEmpty(Plaintext))
                sb.Append($"\n  Message: {Plaintext}");
            return sb.ToString();
        }
    }
}
