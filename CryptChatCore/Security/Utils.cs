using System.Text.RegularExpressions;

namespace CryptChatCore.Security
{
    /// <summary>
    /// Security utilities
    /// </summary>
    class Utils
    {
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
