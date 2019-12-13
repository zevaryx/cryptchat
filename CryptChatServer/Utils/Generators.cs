using MongoDB.Bson;
using System;

namespace CryptChat.Core.Utils
{
    class Generators
    {
        public static string GenerateToken()
        {
            return new Ulid().ToString();
        }

        public static string GenerateCID()
        {
            return GenerateToken();
        }

        public static ObjectId GenerateId()
        {
            return new ObjectId();
        }
    }
}
