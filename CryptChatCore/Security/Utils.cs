using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using Sodium;

namespace CryptChatCore.Security
{
    /// <summary>
    /// Security utilities
    /// </summary>
    public static class Utils
    {
        public static List<byte> MemoryKey { 
            get; 
            private set; 
        }
        private static byte[] nonce = SecretBox.GenerateNonce();

        public static void LoadMemoryKey(string path = null)
        {
            if (string.IsNullOrEmpty(path))
            {
                path = (Environment.OSVersion.Platform == PlatformID.Unix) ?
                        Environment.GetEnvironmentVariable("HOME") :
                        Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
                path += "/.cryptchat/.secret";
            }
            using var entropy = new MemoryStream();
            if (!File.Exists(path))
            {
                if (!Directory.Exists(path.Substring(0, path.LastIndexOf('/'))))
                {
                    Directory.CreateDirectory(path.Substring(0, path.LastIndexOf('/')));
                }
                entropy.Write(SodiumCore.GetRandomBytes(16384));
                File.WriteAllBytes(path, entropy.ToArray());
            }
            else
            {
                entropy.Write(File.ReadAllBytes(path));
            }
            MemoryKey = new List<byte>(GenericHash.Hash(entropy.ToArray(), null, 32)); 
        }

        public static string Lock(byte[] plaintext, string password = null)
        {
            if (string.IsNullOrEmpty(password))
            {
                if (MemoryKey.Count == 0)
                {
                    LoadMemoryKey();
                }
                var cipher = SecretBox.Create(plaintext, nonce, MemoryKey.ToArray());
                return Convert.ToBase64String(cipher);
            }
            else
            {
                byte[] pass_bytes = (IsBase64(password)) ? 
                    Convert.FromBase64String(password) : 
                    Encoding.UTF8.GetBytes(password);
                var cipher = SecretBox.Create(plaintext, GenericHash.Hash(pass_bytes, null, 24), pass_bytes);
                return Convert.ToBase64String(cipher);
            }
        }
        
        public static string Unlock(string ciphertext, string password = null)
        {

            if (!IsBase64(ciphertext))
            {
                throw new ArgumentException($"{nameof(ciphertext)} must be a Base64-encoded string");
            }
            if (string.IsNullOrEmpty(password))
            {
                if (MemoryKey.Count == 0)
                {
                    LoadMemoryKey();
                }
                var plaintext = SecretBox.Open(ciphertext, nonce, MemoryKey.ToArray());
                return Encoding.UTF8.GetString(plaintext);
            }
            else
            {
                byte[] pass_bytes = (IsBase64(password)) ?
                    Convert.FromBase64String(password) :
                    Encoding.UTF8.GetBytes(password);
                var plaintext = SecretBox.Open(ciphertext, GenericHash.Hash(pass_bytes, null, 24), pass_bytes);
                return Encoding.UTF8.GetString(plaintext);
            }
        }

        /// <summary>
        /// Check if the passed in string is in Base64
        /// </summary>
        /// <param name="to_check">String to check</param>
        /// <returns>
        /// If string is Base64
        /// </returns>
        public static bool IsBase64(string toCheck)
        {
            if (string.IsNullOrEmpty(toCheck))
            {
                return false;
            }
            toCheck = toCheck.Trim();
            return (toCheck.Length % 4 == 0) && Regex.IsMatch(toCheck, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

    }
}
