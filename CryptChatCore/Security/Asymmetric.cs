using System;
using System.Collections.Generic;
using System.Text;

using Sodium;

namespace CryptChatCore.Security
{
    class Asymmetric
    {
        public static KeyPair GenerateKeyPair(byte[] seed = null)
        {
            if (seed is null)
                return PublicKeyBox.GenerateKeyPair();
            return PublicKeyBox.GenerateKeyPair(seed);
        }

        public static byte[] GenerateNonce()
        {
            return PublicKeyBox.GenerateNonce();
        }

        public static Dictionary<string, string> Encrypt(string message, string secretKey, string publicKey)
        {
            byte[] nonce = GenerateNonce();
            byte[] secret_bytes;
            byte[] public_bytes;
            if (!Utils.IsBase64(secretKey))
                secret_bytes = Encoding.ASCII.GetBytes(secretKey);
            else
                secret_bytes = Convert.FromBase64String(secretKey);

            if (!Utils.IsBase64(publicKey))
                public_bytes = Encoding.ASCII.GetBytes(publicKey);
            else
                public_bytes = Convert.FromBase64String(publicKey);

            var crypt = PublicKeyBox.Create(message, nonce, secret_bytes, public_bytes);
            return new Dictionary<string, string>()
            {
                { "nonce", Convert.ToBase64String(nonce) },
                { "crypt", Convert.ToBase64String(crypt) }
            };
        }
    }
}
