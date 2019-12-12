using CryptChatProtos.Responses;
using Google.Protobuf;
using System;
using System.Collections.Generic;
using System.Text;

namespace CryptChatServerTCP.Handlers
{
    public static class Defaults
    {
        public static Response ErrorResponse(string message)
        {
            return new Response()
            {
                Data = ByteString.Empty,
                Status = 1,
                Type = -1,
                Message = message
            };
        }
    }
}
