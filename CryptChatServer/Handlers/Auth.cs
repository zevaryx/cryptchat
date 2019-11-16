using System;
using System.Collections.Generic;
using System.Security.Cryptography;

using MongoDB.Bson;
using MongoDB.Driver;
using Google.Protobuf;

using Request = CryptChatProtos.Requests.Request;
using Response = CryptChatProtos.Responses.Response;
using ProtoMaps = CryptChatProtos.Maps;
using CryptChatProtos.Responses.Auth;
using CryptChatProtos.Requests.Auth;

using CryptChatServer.Utils;

namespace CryptChatServer.Handlers
{
    class Auth
    {
        public static Response ProcessRequest(Request request)
        {
            string request_type = ProtoMaps.GetRequestType(request.Type);
            Response result = request_type switch
            {
                "SaltRequest"     => ProcessSaltRequest(SaltRequest.Parser.ParseFrom(request.Data)),
                "LoginRequest"    => ProcessLoginRequest(LoginRequest.Parser.ParseFrom(request.Data)),
                "UserRequest"     => ProcessUserRequest(UserRequest.Parser.ParseFrom(request.Data)),
                "RegisterRequest" => ProcessRegisterRequest(RegisterRequest.Parser.ParseFrom(request.Data)),
                _                 => new Response()
                {
                    Data    = ByteString.Empty,
                    Status  = 1,
                    Message = $"Request of type {request_type} is not supported",
                    Type    = -1
                }
            };
            return result;
        }

        private static Response ProcessSaltRequest(SaltRequest request)
        {
            var user_filter = Builders<Types.User>
                              .Filter
                              .Eq(u => u.username, request.Username);

            var salt = Globals.USERS.Find(user_filter).First().salt;

            if (salt is null)
            {
                using (var crypto = RandomNumberGenerator.Create())
                {
                    byte[] salt_byte = new byte[24];
                    crypto.GetNonZeroBytes(salt_byte);
                    salt = Convert.ToBase64String(salt_byte);
                }
            }

            var response = new SaltResponse()
            {
                Salt = salt
            };

            return new Response()
            {
                Status  = 0,
                Data    = response.ToByteString(),
                Type    = ProtoMaps.GetResponseCode("SaltResponse"),
                Message = string.Empty
            };

        }

        private static Response ProcessLoginRequest(LoginRequest request)
        {
            var user_filter = Builders<Types.User>
                              .Filter
                              .Eq(u => u.username, request.Username);

            var user = Globals.USERS.Find(user_filter).First();

            if (user is null || !request.Hash.Equals(user.hash))
            {
                return new Response()
                {
                    Type    = -1,
                    Data    = ByteString.Empty,
                    Message = $"Login failed",
                    Status  = 1
                };
            }

            var response = new LoginResponse()
            {
                Id = user._id.ToString(),
                Token = user.token,
                Pubkey = user.pubkey,
                Username = user.username
            };

            return new Response()
            {
                Type = ProtoMaps.GetResponseCode("LoginResponse"),
            };

        }

        private static Response ProcessRegisterRequest(RegisterRequest request)
        {
            ObjectId _id = Generators.GenerateId();
            string token = Generators.GenerateToken();

            var user_filter = Builders<Types.User>
                              .Filter
                              .Eq(u => u.username, request.Username);

            var user = Globals.USERS.Find(user_filter).First();
            if (user !is null)
            {
                return new Response()
                {
                    Data    = ByteString.Empty,
                    Type    = -1,
                    Message = $"Registration failed",
                    Status  = 1
                };
            }

            user = new Types.User()
            {
                _id      = _id,
                token    = token,
                hash     = request.Hash,
                salt     = request.Salt,
                username = request.Username,
                pubkey   = request.Pubkey
            };

            Globals.USERS.InsertOne(user);

            var response = new RegisterResponse()
            {
                Id = _id.ToString(),
                Token = token
            };

            return new Response()
            {
                Status  = 0,
                Type    = ProtoMaps.GetResponseCode("RegisterResponse"),
                Data    = response.ToByteString(),
                Message = string.Empty
            };
        }

        private static Response ProcessUserRequest(UserRequest request)
        {
            var user_filter = Builders<Types.User>
                              .Filter
                              .Eq(u => u.username, request.Username);

            var user = Globals.USERS.Find(user_filter).First();

            if (user is null)
            {
                return new Response()
                {
                    Type = -1,
                    Data = ByteString.Empty,
                    Status = 1,
                    Message = $"User retrieval failed"
                };
            }

            var response = new UserResponse()
            {
                Pubkey = user.pubkey,
                Username = user.username
            };

            return new Response()
            {
                Type = ProtoMaps.GetResponseCode("UserRequest"),
                Message = string.Empty,
                Status = 0,
                Data = response.ToByteString()
            };
        }
    }
}
