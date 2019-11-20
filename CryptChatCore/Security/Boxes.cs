using System;
using System.Collections.Generic;
using System.Text;

using Sodium;

namespace CryptChatCore.Security.Boxes
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

        public static string Decrypt(string crypt, string secretKey, string publicKey, string nonce)
        {
            byte[] secret_bytes;
            byte[] public_bytes;
            byte[] nonce_bytes;
            byte[] crypt_bytes;

            if (Utils.IsBase64(nonce))
                nonce_bytes = Encoding.ASCII.GetBytes(nonce);
            else
                throw new ArgumentException($"{nameof(nonce)} must be a Base64-encoded string");

            if (Utils.IsBase64(crypt))
                crypt_bytes = Encoding.ASCII.GetBytes(crypt);
            else
                throw new ArgumentException($"{nameof(crypt)} must be a Base64-encoded string");

            if (!Utils.IsBase64(secretKey))
                secret_bytes = Encoding.ASCII.GetBytes(secretKey);
            else
                secret_bytes = Convert.FromBase64String(secretKey);

            if (!Utils.IsBase64(publicKey))
                public_bytes = Encoding.ASCII.GetBytes(publicKey);
            else
                public_bytes = Convert.FromBase64String(publicKey);

            var plaintext = PublicKeyBox.Open(crypt_bytes, nonce_bytes, secret_bytes, public_bytes);

            return Encoding.UTF8.GetString(plaintext);
        }
    }

    class Symmetric
    {
        public static string GenerateKey()
        {
            var key = SecretBox.GenerateKey();
            return Convert.ToBase64String(key);
        }

        public static byte[] GenerateNonce()
        {
            return SecretBox.GenerateNonce();
        }

        public static Dictionary<string, string> Encrypt(string message, string secretKey)
        {
            byte[] nonce = GenerateNonce();
            byte[] secret_bytes;
            if (!Utils.IsBase64(secretKey))
                secret_bytes = Encoding.ASCII.GetBytes(secretKey);
            else
                secret_bytes = Convert.FromBase64String(secretKey);

            var crypt = SecretBox.Create(message, nonce, secret_bytes);
            return new Dictionary<string, string>()
            {
                { "nonce", Convert.ToBase64String(nonce) },
                { "crypt", Convert.ToBase64String(crypt) }
            };
        }

        public static string Decrypt(string crypt, string secretKey, string nonce)
        {
            byte[] secret_bytes;
            byte[] nonce_bytes;
            byte[] crypt_bytes;

            if (Utils.IsBase64(nonce))
                nonce_bytes = Encoding.ASCII.GetBytes(nonce);
            else
                throw new ArgumentException($"{nameof(nonce)} must be a Base64-encoded string");

            if (Utils.IsBase64(crypt))
                crypt_bytes = Encoding.ASCII.GetBytes(crypt);
            else
                throw new ArgumentException($"{nameof(crypt)} must be a Base64-encoded string");

            if (!Utils.IsBase64(secretKey))
                secret_bytes = Encoding.ASCII.GetBytes(secretKey);
            else
                secret_bytes = Convert.FromBase64String(secretKey);
            
            var plaintext = SecretBox.Open(crypt_bytes, nonce_bytes, secret_bytes);

            return Encoding.UTF8.GetString(plaintext);
        }
    }
}
