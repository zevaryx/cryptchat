using System;
using System.Text;

using Sodium;

namespace CryptChat.Core.Security
{
    /// <summary>
    /// XChaCha management
    /// </summary>
    class XChaCha
    {
        /// <summary>
        /// Encrypt a message using provided key and nonce
        /// </summary>
        /// <param name="message">Plaintext message</param>
        /// <param name="key">Plaintext/Base64-encoded key</param>
        /// <param name="nonce">Base64-encoded nonce (generate using GenerateNonce())</param>
        /// <returns>
        /// Base64-encoded encrypted message
        /// </returns>
        public static string Encrypt(string message, string key, string nonce)
        {
            // Validate parameters
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
            if (string.IsNullOrEmpty(nonce))
            {
                throw new ArgumentNullException(nameof(nonce));
            }
            // Check nonce is Base64
            if (!Utils.IsBase64(nonce))
            {
                throw new ArgumentException($"{nameof(nonce)} must be base64-encoded string");
            }
            byte[] nonce_bytes = Convert.FromBase64String(nonce);
            // Convert key to bytes
            byte[] key_bytes;
            if (Utils.IsBase64(key))
            {
                // Key is Base64, convert to raw bytes
                key_bytes = Convert.FromBase64String(key);
            }
            else
            {
                // Key is plaintext string, fallback to raw ASCII bytes
                key_bytes = Encoding.ASCII.GetBytes(key);
            }
            // Encrypt the message
            byte[] encrypted = StreamEncryption.EncryptXChaCha20(message, nonce_bytes, key_bytes);
            // Return the raw bytes as Base64
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Decrypt a message using provided key and nonce
        /// </summary>
        /// <param name="encrypted">Ciphertext message</param>
        /// <param name="key">Plaintext/Base64-encoded key</param>
        /// <param name="nonce">Base64-encoded nonce (generate using GenerateNonce())</param>
        /// <returns>
        /// Plaintext UTF-8 message
        /// </returns>
        public static string Decrypt(string encrypted, string key, string nonce)
        {
            // Validate parameters
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrEmpty(encrypted))
            {
                throw new ArgumentNullException(nameof(encrypted));
            }
            if (string.IsNullOrEmpty(nonce))
            {
                throw new ArgumentNullException(nameof(nonce));
            }
            // Check nonce is Base64
            if (!Utils.IsBase64(nonce))
            {
                throw new ArgumentException($"{nameof(nonce)} must be base64-encoded string");
            }
            byte[] nonce_bytes = Convert.FromBase64String(nonce);
            // Convert key to bytes
            byte[] key_bytes;
            if (Utils.IsBase64(key))
            {
                // Key is Base64, convert to raw bytes
                key_bytes = Convert.FromBase64String(key);
            }
            else
            {
                // Key is plaintext string, fallback to raw ASCII bytes
                key_bytes = Encoding.ASCII.GetBytes(key);
            }
            // Check if encrypted is Base64
            if (!Utils.IsBase64(encrypted))
            {
                throw new ArgumentException($"{nameof(encrypted)} must be base64-encoded string");
            }
            byte[] encrypted_bytes = Convert.FromBase64String(encrypted);
            // Decrypt the message
            var raw_msg = StreamEncryption.DecryptXChaCha20(encrypted_bytes, nonce_bytes, key_bytes);
            return Encoding.UTF8.GetString(raw_msg);
        }

        /// <summary>
        /// Generate a secure nonce
        /// </summary>
        /// <returns>
        /// Base64-encoded nonce
        /// </returns>
        public static string GenerateNonce()
        {
            return Convert.ToBase64String(StreamEncryption.GenerateNonceXChaCha20());
        }
    }
    /// <summary>
    /// ChaCha management
    /// </summary>
    class ChaCha
    {
        /// <summary>
        /// Encrypt a message using provided key and nonce
        /// </summary>
        /// <param name="message">Plaintext message</param>
        /// <param name="key">Plaintext/Base64-encoded key</param>
        /// <param name="nonce">Base64-encoded nonce (generate using GenerateNonce())</param>
        /// <returns>
        /// Base64-encoded encrypted message
        /// </returns>
        public static string Encrypt(string message, string key, string nonce)
        {
            // Validate parameters
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }
            if (string.IsNullOrEmpty(nonce))
            {
                throw new ArgumentNullException(nameof(nonce));
            }
            // Check nonce is Base64
            if (!Utils.IsBase64(nonce))
            {
                throw new ArgumentException($"{nameof(nonce)} must be base64-encoded string");
            }
            byte[] nonce_bytes = Convert.FromBase64String(nonce);
            // Convert key to bytes
            byte[] key_bytes;
            if (Utils.IsBase64(key))
            {
                // Key is Base64, convert to raw bytes
                key_bytes = Convert.FromBase64String(key);
            }
            else
            {
                // Key is plaintext string, fallback to raw ASCII bytes
                key_bytes = Encoding.ASCII.GetBytes(key);
            }
            // Encrypt the message
            byte[] encrypted = StreamEncryption.EncryptChaCha20(message, nonce_bytes, key_bytes);
            // Return the raw bytes as Base64
            return Convert.ToBase64String(encrypted);
        }

        /// <summary>
        /// Decrypt a message using provided key and nonce
        /// </summary>
        /// <param name="encrypted">Ciphertext message</param>
        /// <param name="key">Plaintext/Base64-encoded key</param>
        /// <param name="nonce">Base64-encoded nonce (generate using GenerateNonce())</param>
        /// <returns>
        /// Plaintext UTF-8 message
        /// </returns>
        public static string Decrypt(string encrypted, string key, string nonce)
        {
            // Validate parameters
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }
            if (string.IsNullOrEmpty(encrypted))
            {
                throw new ArgumentNullException(nameof(encrypted));
            }
            if (string.IsNullOrEmpty(nonce))
            {
                throw new ArgumentNullException(nameof(nonce));
            }
            // Check nonce is Base64
            if (!Utils.IsBase64(nonce))
            {
                throw new ArgumentException($"{nameof(nonce)} must be base64-encoded string");
            }
            byte[] nonce_bytes = Convert.FromBase64String(nonce);
            // Convert key to bytes
            byte[] key_bytes;
            if (Utils.IsBase64(key))
            {
                // Key is Base64, convert to raw bytes
                key_bytes = Convert.FromBase64String(key);
            }
            else
            {
                // Key is plaintext string, fallback to raw ASCII bytes
                key_bytes = Encoding.ASCII.GetBytes(key);
            }
            // Check if encrypted is Base64
            if (!Utils.IsBase64(encrypted))
            {
                throw new ArgumentException($"{nameof(encrypted)} must be base64-encoded string");
            }
            byte[] encrypted_bytes = Convert.FromBase64String(encrypted);
            // Decrypt the message
            var raw_msg = StreamEncryption.DecryptChaCha20(encrypted_bytes, nonce_bytes, key_bytes);
            return Encoding.UTF8.GetString(raw_msg);
        }

        /// <summary>
        /// Generate a secure nonce
        /// </summary>
        /// <returns>
        /// Base64-encoded nonce
        /// </returns>
        public static string GenerateNonce()
        {
            return Convert.ToBase64String(StreamEncryption.GenerateNonceChaCha20());
        }
    }
}
