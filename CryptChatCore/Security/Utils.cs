using System;
using System.Collections.Generic;
using System.IO;
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
                entropy.Write(SodiumCore.GetRandomBytes(16384));
                File.WriteAllBytes(path, entropy.ToArray());
            }
            else
            {
                entropy.Write(File.ReadAllBytes(path));
            }
            MemoryKey = new List<byte>(GenericHash.Hash(entropy.ToArray(), null, 32)); 
        }

        /// <summary>
        /// Check if the passed in string is in Base64
        /// </summary>
        /// <param name="to_check">String to check</param>
        /// <returns>
        /// If string is Base64
        /// </returns>
        public static bool IsBase64(string to_check)
        {
            to_check = to_check.Trim();
            return (to_check.Length % 4 == 0) && Regex.IsMatch(to_check, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

    }
}
