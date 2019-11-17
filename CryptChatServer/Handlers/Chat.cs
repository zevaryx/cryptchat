using System;
using System.Collections.Generic;
using System.Text;

using Google.Protobuf;
using Google.Protobuf.Collections;
using MongoDB.Driver;

using CryptChatProtos.Requests;
using CryptChatProtos.Responses;
using CryptChatProtos.Requests.Chat;
using CryptChatProtos.Responses.Chat;
using ProtoMaps = CryptChatProtos.Maps;
using CryptChatServer.Utils;

namespace CryptChatServer.Handlers
{
    class Chat
    {
        public static Response ProcessRequest(Request request)
        {
            string request_type = ProtoMaps.GetRequestType(request.Type);
            Response result = request_type switch
            {
                "DeleteMessageRequest" => ProcessQueueRequest(QueueRequest.Parser.ParseFrom(request.Data)),
                "SendMessageRequest"   => ProcessChatRequest(ChatRequest.Parser.ParseFrom(request.Data)),
                "MessageRequest"       => ProcessChatListRequest(ChatListRequest.Parser.ParseFrom(request.Data)),
                "EditMessageRequest"   => ProcessNewRequest(NewRequest.Parser.ParseFrom(request.Data)),
                _                      => new Response()
                                       {
                                           Data    = ByteString.Empty,
                                           Status  = 1,
                                           Message = $"Request of type {request_type} is not supported",
                                           Type    = -1
                                       }
            };
            return result;
        }

        public static Response ProcessQueueRequest(QueueRequest request)
        {
            var error_response = new Response()
            {
                Data = ByteString.Empty,
                Status = 1,
                Message = "Failed to get queue",
                Type = -1
            };

            var token_owner = Tokens.GetTokenOwner(request.Token);
            if (token_owner is null)
            {
                return error_response;
            }

            var chat_filter = Builders<Types.Chat>
                              .Filter
                              .Eq(c => c._id.ToString(), request.Id);

            var chat = Globals.CHATS.Find(chat_filter).First();

            if (chat is null || !chat.members.Contains(token_owner._id.ToString()))
            {
                return error_response;
            }

            Types.Queue queue;
            bool got = chat.queue.TryGetValue(token_owner._id.ToString(), out queue);

            if (!got)
            {
                queue = new Types.Queue()
                {
                    deleted = new List<string>(),
                    edited  = new List<string>(),
                    resync  = new List<string>()
                };
            }

            var response = new QueueResponse();
            queue.deleted.ForEach(d => response.Deleted.Add(d));
            queue.edited.ForEach(e => response.Edited.Add(e));
            queue.resync.ForEach(r => response.Resync.Add(r));

            return new Response()
            {
                Type = ProtoMaps.GetResponseCode("QueueResponse"),
                Data = response.ToByteString(),
                Status = 0,
                Message = string.Empty
            };
        }

        public static Response ProcessChatRequest(ChatRequest request)
        {
            var error_response = new Response()
            {
                Data = ByteString.Empty,
                Status = 1,
                Type = -1,
                Message = "Failed to get chat"
            };
            var token_owner = Tokens.GetTokenOwner(request.Token);

            var chat_filter = Builders<Types.Chat>
                              .Filter
                              .Eq(c => c._id.ToString(), request.Id);

            var chat = Globals.CHATS.Find(chat_filter).First();
            if (chat is null || !chat.members.Contains(token_owner._id.ToString()))
            {
                return error_response;
            }

            var response = new ChatResponse()
            {
                Id = chat._id.ToString(),
                MsgCount = chat.msg_count
            };

            chat.members.ForEach(m => response.Members.Add(m));

            return new Response()
            {
                Status = 0,
                Data = response.ToByteString(),
                Type = ProtoMaps.GetResponseCode("ChatResponse"),
                Message = string.Empty
            };
        }

        public static Response ProcessChatListRequest(ChatListRequest request)
        {
            var error_response = new Response()
            {
                Data = ByteString.Empty,
                Status = 1,
                Type = -1,
                Message = "Failed to get chat list"
            };
            var token_owner = Tokens.GetTokenOwner(request.Token);

            if (token_owner is null)
            {
                return error_response;
            }

            var chat_filter = Builders<Types.Chat>
                              .Filter
                              .AnyEq(c => c.members, token_owner._id.ToString());
            var chats = Globals.CHATS.Find(chat_filter).ToList();
            if (chats is null)
            {
                return error_response;
            }

            var response = new ChatListResponse();
            foreach (var chat in chats)
            {
                var r = new ChatResponse()
                {
                    Id = chat._id.ToString(),
                    MsgCount = chat.msg_count
                };
                chat.members.ForEach(m => r.Members.Add(m));
                response.Chats.Add(r);
            }

            return new Response()
            {
                Status = 0,
                Type = ProtoMaps.GetResponseCode("ChatListResponse"),
                Data = response.ToByteString(),
                Message = string.Empty
            };
        }

        public static Response ProcessNewRequest(NewRequest request)
        {

        }
    }
}
