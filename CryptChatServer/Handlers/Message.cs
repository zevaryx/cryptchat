using System;
using System.Collections.Generic;

using MongoDB.Driver;
using Google.Protobuf;

using Request = CryptChatProtos.Requests.Request;
using Response = CryptChatProtos.Responses.Response;
using ProtoMaps = CryptChatProtos.Maps;
using CryptChatProtos.Responses.Message;
using CryptChatProtos.Requests.Message;

using CryptChatServer.Utils;


namespace CryptChatServer.Handlers
{
    class Message
    {
        public static Response ProcessRequest(Request request)
        {
            string request_type = ProtoMaps.GetRequestType(request.Type);
            Response result = request_type switch
            {
                "DeleteMessageRequest" => ProcessDeleteRequest(DeleteMessageRequest.Parser.ParseFrom(request.Data)),
                "SendMessageRequest"   => ProcessSendRequest(SendMessageRequest.Parser.ParseFrom(request.Data)),
                "MessageRequest"       => ProcessMessageRequest(MessageRequest.Parser.ParseFrom(request.Data)),
                "EditMessageRequest"   => ProcessEditRequest(EditMessageRequest.Parser.ParseFrom(request.Data)),
                _                      => new Response() { 
                                                Data    = ByteString.Empty, 
                                                Status  = 1, 
                                                Message = $"Request of type {request_type} is not supported", 
                                                Type    = -1 
                                            }
            };
            return result;
        }

        public static Response ProcessDeleteRequest(DeleteMessageRequest request)
        {
            var token_owner = Tokens.GetTokenOwner(request.Token);
            if (token_owner is null)
            {
                return new Response()
                {
                    Data    = ByteString.Empty,
                    Status  = 1,
                    Message = $"Error deleting message",
                    Type    = -1
                };
            }

            var message_filter = Builders<Types.Message>
                                    .Filter
                                    .Eq(m => m._id.ToString(), request.Id);

            Types.Message message = Globals.MESSAGES
                                        .Find(message_filter)
                                        .First();
            if (message is null || token_owner._id.ToString() != message.sender)
            {
                return new Response()
                {
                    Data    = ByteString.Empty,
                    Status  = 1,
                    Message = $"Error deleting message",
                    Type    = -1
                };
            }

            var message_check = Globals.MESSAGES.FindOneAndDelete(message_filter) is null;

            var response = new DeleteMessageResponse()
            {
                Deleted = message_check
            };

            return new Response()
            {
                Status  = 0, 
                Message = string.Empty, 
                Type    = ProtoMaps.GetResponseCode("DeleteMessageResponse"), 
                Data    = response.ToByteString()
            };
        }
        
        public static Response ProcessSendRequest(SendMessageRequest request)
        {
            var token_owner = Tokens.GetTokenOwner(request.Token);
            if (token_owner is null)
            {
                return new Response()
                {
                    Data    = ByteString.Empty,
                    Status  = 1,
                    Message = $"Error sending message",
                    Type    = -1
                };
            }

            // Get recipient
            var user_filter = Builders<Types.User>
                                .Filter
                                .Eq(u => u.username, request.Recipient);

            Types.User recipient = Globals.USERS
                                    .Find(user_filter)
                                    .First();

            if (recipient is null)
            {
                return new Response()
                {
                    Data    = ByteString.Empty,
                    Status  = 1,
                    Message = $"Invalid recipient",
                    Type    = -1
                };
            }

            string chat_id = request.Chat;
            Types.Chat chat;
            // Build chat if it doesn't exist
            if (string.IsNullOrEmpty(chat_id))
            {
                // Get recipient ID (will eventually also query groups for Group ID
                chat = new Types.Chat()
                {
                    _id = Generators.GenerateId(),
                    members = new List<string>(2) 
                    {
                        token_owner._id.ToString(),
                        recipient._id.ToString()
                    }, 
                    msg_count = 1, 
                    queue = new Dictionary<string, Types.Queue>(2) 
                    {
                        { recipient._id.ToString(), new Types.Queue() },
                        { token_owner._id.ToString(), new Types.Queue() }
                    }
                };
                Globals.CHATS.InsertOne(chat);
            }
            // Update it if it does
            else
            {
                var chat_filter = Builders<Types.Chat>
                                    .Filter
                                    .Eq(c => c._id.ToString(), chat_id);
                var chat_update = Builders<Types.Chat>
                                    .Update
                                    .Inc(c => c.msg_count, 1);
                chat = Globals.CHATS.FindOneAndUpdate(chat_filter, chat_update);
                
                if (!chat.members.Contains(token_owner._id.ToString()))
                {
                    return new Response()
                    {
                        Data = ByteString.Empty,
                        Status = 1,
                        Message = $"Error sending message (sender not part of chat)",
                        Type = -1
                    };
                }
            }

            Dictionary<string, string> key = new Dictionary<string, string>();
            foreach (KeyValuePair<string, string> kv in request.Keys)
            {
                key.Add(kv.Key, kv.Value);
            }

            var message = new Types.Message()
            {
                _id       = Generators.GenerateId(),
                chat      = chat._id,
                edited    = false,
                edited_at = 0,
                file      = request.File,
                key       = key,
                message   = request.Message,
                sender    = token_owner._id.ToString(),
                signature = request.Signature,
                timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds()
            };

            Globals.MESSAGES.InsertOne(message);
            var sender_key = string.Empty;
            message.key.TryGetValue(token_owner._id.ToString(), out sender_key);
            var response = new MessageResponse()
            {
                Message   = message.message,
                Chat      = message.chat.ToString(), 
                Edited    = message.edited,
                EditedAt  = message.edited_at,
                File      = message.file, 
                Id        = message._id.ToString(),
                Key       = sender_key, 
                Sender    = token_owner.username.ToString(), 
                Signature = message.signature, 
                Timestamp = message.timestamp
            };
            return new Response()
            {
                Status  = 0,
                Type    = ProtoMaps.GetResponseCode("MessageRequest"),
                Data    = response.ToByteString(), 
                Message = string.Empty
            };

        }

        public static Response ProcessMessageRequest(MessageRequest request)
        {
            //  Check token validity
            var token_owner = Tokens.GetTokenOwner(request.Token);
            if (token_owner is null)
            {
                return new Response() { 
                    Data    = ByteString.Empty,
                    Status  = 1,
                    Message = $"Error retrieving message", 
                    Type    = -1 
                };
            }

            // Get message
            var message_filter = Builders<Types.Message>
                                    .Filter
                                    .Eq(m => m._id.ToString(), request.Id);

            var document = Globals.MESSAGES
                                    .Find(message_filter)
                                    .First();

            if (document is null)
            {
                return new Response() { 
                    Data    = ByteString.Empty, 
                    Status  = 1,
                    Message = $"Error retrieving message", 
                    Type    = -1 
                };
            }

            // Get chat
            var chat_filter = Builders<Types.Chat>
                                .Filter
                                .Eq(c => c._id, document.chat);

            var chat = Globals.CHATS
                        .Find(chat_filter)
                        .First();

            if (chat is null || !chat.members.Contains(token_owner._id.ToString()))
            {
                return new Response() { 
                    Data    = ByteString.Empty, 
                    Status  = 1, 
                    Message = $"Error retrieving message", 
                    Type    = -1 
                };
            }

            // Get sender username
            var user_filter = Builders<Types.User>
                                .Filter
                                .Eq(u => u._id.ToString(), document.sender);

            string sender = Globals.USERS
                                .Find(user_filter)
                                .First()
                                .username;
            
            // Build and return response
            var response = new MessageResponse()
            {
                Id        = document._id.ToString(),
                Chat      = document.chat.ToString(),
                Edited    = document.edited,
                EditedAt  = document.edited_at,
                File      = document.file,
                Key       = document.key.GetValueOrDefault(token_owner._id.ToString()),
                Message   = document.message,
                Sender    = sender,
                Signature = document.signature,
                Timestamp = document.timestamp
            };

            return new Response() { 
                Data    = response.ToByteString(), 
                Message = string.Empty, 
                Status  = 0, 
                Type    = ProtoMaps.GetResponseCode("MessageResponse") 
            };
        }

        public static Response ProcessEditRequest(EditMessageRequest request)
        {
            var token_owner = Tokens.GetTokenOwner(request.Token);

            if  (token_owner is null)
            {
                return new Response()
                {
                    Data    = ByteString.Empty,
                    Status  = 1,
                    Message = $"Error editing message",
                    Type    = -1
                };
            }

            var message_filter = Builders<Types.Message>
                                 .Filter
                                 .Eq(m => m._id.ToString(), request.Id);

            var message = Globals.MESSAGES
                            .Find(message_filter).First();

            if (message is null || message.sender != token_owner._id.ToString())
            {
                return new Response()
                {
                    Data    = ByteString.Empty,
                    Status  = 1,
                    Message = $"Error editing message",
                    Type    = -1
                };
            }

            var timestamp = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            message.edited = true;
            message.edited_at = timestamp;
            message.message = request.Message;
            message.signature = request.Signature;

            var message_update = Builders<Types.Message>
                                 .Update
                                 .Set(m => m.edited, true)
                                 .Set(m => m.edited_at, timestamp)
                                 .Set(m => m.message, request.Message)
                                 .Set(m => m.signature, request.Signature);

            Globals.MESSAGES.FindOneAndUpdate(message_filter, message_update);

            var response = new EditMessageResponse()
            {
                Edited   = true,
                EditedAt = timestamp,
                Id       = message._id.ToString()
            };

            return new Response()
            {
                Message = string.Empty,
                Status  = 0,
                Type    = ProtoMaps.GetResponseCode("EditMessageResponse"),
                Data    = response.ToByteString()
            };

        }
    }
}
