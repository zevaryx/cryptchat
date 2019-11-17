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
using CryptChatProtos.Responses.Message;
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
                "SendMessageRequest" => ProcessChatRequest(ChatRequest.Parser.ParseFrom(request.Data)),
                "MessageRequest" => ProcessChatListRequest(ChatListRequest.Parser.ParseFrom(request.Data)),
                "EditMessageRequest" => ProcessNewRequest(NewRequest.Parser.ParseFrom(request.Data)),
                _ => Defaults.ErrorResponse($"Request of type {request_type} is not supported")
            };
            return result;
        }

        public static Response ProcessQueueRequest(QueueRequest request)
        {

            var token_owner = Tokens.GetTokenOwner(request.Token);
            if (token_owner is null)
            {
                return Defaults.ErrorResponse("Failed to get queue");
            }

            var chat_filter = Builders<Types.Chat>
                              .Filter
                              .Eq(c => c._id.ToString(), request.Id);

            var chat = Globals.CHATS.Find(chat_filter).First();

            if (chat is null || !chat.members.Contains(token_owner._id.ToString()))
            {
                return Defaults.ErrorResponse("Failed to get queue");
            }

            bool got = chat.queue.TryGetValue(token_owner._id.ToString(), out Types.Queue queue);

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
            var token_owner = Tokens.GetTokenOwner(request.Token);

            var chat_filter = Builders<Types.Chat>
                              .Filter
                              .Eq(c => c._id.ToString(), request.Id);

            var chat = Globals.CHATS.Find(chat_filter).First();
            if (chat is null || !chat.members.Contains(token_owner._id.ToString()))
            {
                return Defaults.ErrorResponse("Failed to get chat");
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
            var token_owner = Tokens.GetTokenOwner(request.Token);

            if (token_owner is null)
            {
                return Defaults.ErrorResponse("Failed to get chat list");
            }

            var chat_filter = Builders<Types.Chat>
                              .Filter
                              .AnyEq(c => c.members, token_owner._id.ToString());
            var chats = Globals.CHATS.Find(chat_filter).ToList();
            if (chats is null)
            {
                return Defaults.ErrorResponse("Failed to get chat list");
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
            var token_owner = Tokens.GetTokenOwner(request.Token);
            if (token_owner is null)
            {
                return Defaults.ErrorResponse("Failed getting new messages");
            }

            var chat_filter = Builders<Types.Chat>.Filter.Eq(c => c._id.ToString(), request.Id);

            var chat = Globals.CHATS.Find(chat_filter).First();
            if (chat is null || !chat.members.Contains(token_owner._id.ToString()))
            {
                return Defaults.ErrorResponse("Failed getting new messages");
            }

            var message_filter = Builders<Types.Message>.Filter.Eq(m => m._id.ToString(), request.Id);
            message_filter &= Builders<Types.Message>.Filter.Gt(m => m.timestamp, request.Oldest);

            var messages = Globals.MESSAGES.Find(message_filter);

            var response = new MessageListResponse();

            foreach (Types.Message m in messages.ToList())
            {
                var user_filter = Builders<Types.User>.Filter.Eq(u => u._id.ToString(), m.sender);
                string username = token_owner.username;
                m.key.TryGetValue(token_owner._id.ToString(), out string key);
                if (m.sender != token_owner._id.ToString())
                {
                    username = Globals.USERS.Find(user_filter).First().username;
                }
                response.Messages.Add(new MessageResponse()
                {
                    Sender = username, 
                    Chat = m.chat.ToString(), 
                    Edited = m.edited, 
                    EditedAt = m.edited_at, 
                    File = m.file, 
                    Id = m._id.ToString(), 
                    Key = key, 
                    Message = m.message, 
                    Signature = m.signature, 
                    Timestamp = m.timestamp
                });
            }

            return new Response()
            {
                Data = response.ToByteString(),
                Message = string.Empty,
                Status = 0,
                Type = ProtoMaps.GetResponseCode("MessageListResponse")
            };
        }
    }
}
