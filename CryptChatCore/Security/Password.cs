using System;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Sodium;

namespace CryptChat.Core.Security
{
    /// <summary>
    /// Password management
    /// </summary>
    public class Password
    {
        public static string GenerateSalt()
        {
            var salt = PasswordHash.ScryptGenerateSalt();
            return Convert.ToBase64String(salt);
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
            if (!Utils.IsBase64(salt))
            {
                throw new ArgumentException($"{nameof(salt)} must be a Base64-encoded string");
            }

            byte[] hash = PasswordHash.ScryptHashBinary(Encoding.ASCII.GetBytes(password), Convert.FromBase64String(salt), PasswordHash.Strength.Interactive, 128);
            return Convert.ToBase64String(hash);
        }
    }
}
