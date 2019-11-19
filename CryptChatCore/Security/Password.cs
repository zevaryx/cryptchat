using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Sodium;

namespace CryptChatCore.Security
{
    /// <summary>
    /// Password management
    /// </summary>
    class Password
    {
        public static string GenerateSalt()
        {
            var salt = PasswordHash.ScryptGenerateSalt();
            return Encoding.ASCII.GetString(salt);
        }

        public static string HashPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password))
            {
                throw new ArgumentNullException(nameof(password));
            }
            if (string.IsNullOrEmpty(salt))
            {
                throw new ArgumentNullException(nameof(salt));
            }

            byte[] hash = PasswordHash.ScryptHashBinary(password, salt, PasswordHash.Strength.Interactive, 128);
            return Convert.ToBase64String(hash);
        }
    }
}
