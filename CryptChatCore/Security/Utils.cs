using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

using Sodium;

namespace CryptChat.Core.Security
{
    /// <summary>
    /// Security utilities
    /// </summary>
    public static class Utils
    {

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
    public sealed class SecureString : IDisposable
    {
        private bool disposed = false;
        private bool wasBytes = false;
        private byte[] _data;
        private byte[] _nonce = SecretBox.GenerateNonce();
        private byte[] _key = SecretBox.GenerateKey();
        public SecureString(string data) =>
            _data = SecretBox.Create(data, _nonce, _key);

        public static implicit operator SecureString(string data)
        {
            if (data == null)
                return null;
            return new SecureString(data);
        }

        public static implicit operator SecureString(byte[] data)
        {
            if (data == null || data.Length == 0)
                return null;
            SecureString n = new SecureString(Convert.ToBase64String(data));
            n.wasBytes = true;
            return n;
        }

        public static implicit operator string(SecureString secure)
        {
            if (secure == null)
                return null;
            return secure.ToString();
        }

        public static implicit operator byte[](SecureString secure)
        {
            if (secure.wasBytes)
                return Convert.FromBase64String(secure.ToString());
            return Encoding.UTF8.GetBytes(secure);
        }

        public override string ToString()
        {
            return Encoding.UTF8.GetString(SecretBox.Open(_data, _nonce, _key));
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    // We would dispose of managed resources here
                }
                Array.Clear(_data, 0, _data.Length);
                Array.Clear(_key, 0, _key.Length);
                Array.Clear(_nonce, 0, _nonce.Length);
                disposed = true;
            }
        }

        ~SecureString() => Dispose(false);
    }
    public sealed class Base32
    {

        // the valid chars for the encoding
        private static string ValidChars = "QAZ2WSX3" + "EDC4RFV5" + "TGB6YHN7" + "UJM8K9LP";

        /// <summary>
        /// Converts an array of bytes to a Base32-k string.
        /// </summary>
        public static string ToBase32String(byte[] bytes)
        {
            StringBuilder sb = new StringBuilder();         // holds the base32 chars
            byte index;
            int hi = 5;
            int currentByte = 0;

            while (currentByte < bytes.Length)
            {
                // do we need to use the next byte?
                if (hi > 8)
                {
                    // get the last piece from the current byte, shift it to the right
                    // and increment the byte counter
                    index = (byte)(bytes[currentByte++] >> (hi - 5));
                    if (currentByte != bytes.Length)
                    {
                        // if we are not at the end, get the first piece from
                        // the next byte, clear it and shift it to the left
                        index = (byte)(((byte)(bytes[currentByte] << (16 - hi)) >> 3) | index);
                    }

                    hi -= 3;
                }
                else if (hi == 8)
                {
                    index = (byte)(bytes[currentByte++] >> 3);
                    hi -= 3;
                }
                else
                {

                    // simply get the stuff from the current byte
                    index = (byte)((byte)(bytes[currentByte] << (8 - hi)) >> 3);
                    hi += 5;
                }

                sb.Append(ValidChars[index]);
            }

            return sb.ToString();
        }


        /// <summary>
        /// Converts a Base32-k string into an array of bytes.
        /// </summary>
        /// <exception cref="System.ArgumentException">
        /// Input string <paramref name="s">s</paramref> contains invalid Base32-k characters.
        /// </exception>
        public static byte[] FromBase32String(string str)
        {
            int numBytes = str.Length * 5 / 8;
            byte[] bytes = new Byte[numBytes];

            // all UPPERCASE chars
            str = str.ToUpper();

            int bit_buffer;
            int currentCharIndex;
            int bits_in_buffer;

            if (str.Length < 3)
            {
                bytes[0] = (byte)(ValidChars.IndexOf(str[0]) | ValidChars.IndexOf(str[1]) << 5);
                return bytes;
            }

            bit_buffer = (ValidChars.IndexOf(str[0]) | ValidChars.IndexOf(str[1]) << 5);
            bits_in_buffer = 10;
            currentCharIndex = 2;
            for (int i = 0; i < bytes.Length; i++)
            {
                bytes[i] = (byte)bit_buffer;
                bit_buffer >>= 8;
                bits_in_buffer -= 8;
                while (bits_in_buffer < 8 && currentCharIndex < str.Length)
                {
                    bit_buffer |= ValidChars.IndexOf(str[currentCharIndex++]) << bits_in_buffer;
                    bits_in_buffer += 5;
                }
            }

            return bytes;
        }
    }
}
