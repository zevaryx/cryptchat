using System;
using System.Security.Cryptography;

using MongoDB.Bson;
using MongoDB.Driver;
using Google.Protobuf;

using Request = CryptChatProtos.Requests.Request;
using Response = CryptChatProtos.Responses.Response;
using ProtoMaps = CryptChatProtos.Maps;
using CryptChatProtos.Responses.Account;
using CryptChatProtos.Requests.Account;

using CryptChatServer.Utils;

namespace CryptChatServer.Handlers
{
    class Account
    {
        public static Response ProcessRequest(Request request)
        {
            string request_type = ProtoMaps.GetRequestType(request.Type);
            Response result = request_type switch
            {
                "ChangePasswordRequest" => ProcessChangePasswordRequest(ChangePasswordRequest.Parser.ParseFrom(request.Data)),
                _ => Defaults.ErrorResponse($"Request type of {request_type} is not supported")
            };
            return result;
        }

        public static Response ProcessChangePasswordRequest(ChangePasswordRequest request)
        {
            var token_owner = Tokens.GetTokenOwner(request.Token);
            if (token_owner is null)
            {
                return Defaults.ErrorResponse("Error changing password");
            }

            var user_filter = Builders<Types.User>.Filter.Eq(u => u.token, request.Token);
            user_filter &= Builders<Types.User>.Filter.Eq(u => u.hash, request.OldHash);
            user_filter &= Builders<Types.User>.Filter.Eq(u => u.salt, request.Salt);

            var user = Globals.USERS.Find(user_filter).First();
            if (user is null)
            {
                return Defaults.ErrorResponse("Error changing password");
            }

            var new_token = Generators.GenerateToken();

            var user_update = Builders<Types.User>.Update
                              .Set(u => u.hash, request.NewHash)
                              .Set(u => u.pubkey, request.Pubkey)
                              .Set(u => u.token, new_token);


            Globals.USERS.FindOneAndUpdate(user_filter, user_update);

            var response = new ChangePasswordResponse()
            {
                Token = new_token
            };

            return new Response()
            {
                Data = response.ToByteString(),
                Message = string.Empty,
                Status = 0,
                Type = ProtoMaps.GetResponseCode("ChangePasswordResponse")
            };
        }
    }
}
