using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptChatServer.Utils
{
    class Generators
    {
        public static string GenerateToken()
        {
            return new Ulid().ToString();
        }

        public static string GenerateSID()
        {
            return GenerateToken();
        }

        public static ObjectId GenerateId()
        {
            return new ObjectId();
        }
    }
}
