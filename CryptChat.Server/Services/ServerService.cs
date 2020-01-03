using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;

using CryptChat.Server;
using CryptChat.Server.Models;
using CCServer = CryptChat.Server.Server;
using MongoDB.Bson;
using System.Text;

namespace CryptChat.Server.Services
{
    public class ServerService : CCServer.ServerBase
    {
        private readonly UserService _userService;
        private readonly ChatService _chatService;
        private readonly MessageService _messageService;
        private readonly ILogger<ServerService> _logger;

        private double? ToEpoch(DateTimeOffset dateTime) => dateTime.ToUnixTimeSeconds();

        public ServerService(ILogger<ServerService> logger, UserService userService, ChatService chatService, MessageService messageService)
        {
            _logger = logger;
            _userService = userService;
            _chatService = chatService;
            _messageService = messageService;

        }

        #region NotificationService

        #endregion

        #region MessageService
        public override Task<MessageResponse> GetMessage(MessageRequest request, ServerCallContext context)
        {
            Message message = _messageService.Get(request.Id);
            User user = _userService.GetFromToken(request.Token);
            Chat chat = null;
            if (message != null && user != null)
                chat = _chatService.Get(message.Id);

            if (user == null)
                throw new RpcException(new Status(StatusCode.Unauthenticated, "Could not validate token"));

            if (chat == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Unable to find chat"));

            if (!chat.Members.Contains(user.Id))
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Unable to access chat"));

            if (message == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Unable to find message"));

            if (!message.Keys.Keys.Contains(user.Id))
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Unable to access message"));

            var sender = _userService.Get(message.Sender);

            return Task.FromResult(new MessageResponse
            {
                Id = message.Id, 
                Chat = message.Chat, 
                Edited = message.Edited, 
                EditedAt = message.Edited_At, 
                File = message.File, 
                Key = message.Keys.First(x => x.Key == user.Id).Value, 
                Message = message.message, 
                Nonce = message.Nonce, 
                Sender = sender.Username, 
                Timestamp = message.Timestamp
            });
        }

        public override async Task GetAllMessages(SyncRequest request, IServerStreamWriter<MessageResponse> responseStream, ServerCallContext context)
        {
            User user = _userService.GetFromToken(request.Token);

            if (user == null)
                throw new RpcException(new Status(StatusCode.Unauthenticated, "Could not validate token"));

            List<Message> messages = _messageService.GetUserMessages(user.Id);
            foreach (Message message in messages)
            {
                var sender = _userService.Get(message.Sender);
                var response = new MessageResponse
                {
                    Id = message.Id,
                    Chat = message.Chat,
                    Edited = message.Edited,
                    EditedAt = message.Edited_At,
                    File = message.File,
                    Key = message.Keys.First(x => x.Key == user.Id).Value,
                    Message = message.message,
                    Nonce = message.Nonce,
                    Sender = sender.Username,
                    Timestamp = message.Timestamp
                };

                await responseStream.WriteAsync(response);
            }
        }

        public override Task<SendMessageResponse> SendMessage(SendMessageRequest request, ServerCallContext context)
        {
            User sender = _userService.GetFromToken(request.Token);
            User recipient = _userService.GetFromUsername(request.Recipient);

            if (recipient == null)
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));

            if (sender == null)
                throw new RpcException(new Status(StatusCode.Unauthenticated, "Could not validate token"));

            Chat chat;

            if (string.IsNullOrEmpty(request.Chat))
                chat = _chatService.GetFromUsers(new string[] { sender.Id, recipient.Id });
            else
                chat = _chatService.Get(request.Chat);

            bool created = false;

            if (chat == null)
            {
                chat = new Chat()
                {
                    Id = new BsonObjectId(ObjectId.GenerateNewId()).ToString(),
                    Members = new string[] { sender.Id, recipient.Id },
                    MessageCount = 1,
                    Queue = new Dictionary<string, ChatQueue>()
                };
                _chatService.Create(chat);
                created = true;
            }

            var tstamp = ToEpoch(DateTimeOffset.UtcNow) ?? 0.0;

            Message message = new Message()
            {
                message = request.Message,
                Keys = new Dictionary<string, string>(),
                Nonce = request.Nonce,
                Chat = chat.Id,
                Id = new BsonObjectId(ObjectId.GenerateNewId()).ToString(),
                Edited = false,
                Edited_At = -1.0,
                File = request.File,
                Sender = sender.Id,
                Timestamp = tstamp
            };
            request.Keys.ToList().ForEach(x => message.Keys.Add((x.Key == sender.Username ? sender.Id : recipient.Id), x.Value));

            _messageService.Create(message);

            if (!created)
            {
                chat.MessageCount++;
                _chatService.Update(chat.Id, chat);
            }

            return Task.FromResult(new SendMessageResponse
            {
                Id = message.Id, 
                Chat = chat.Id, 
                Timestamp = message.Timestamp
            });
        }

        public override Task<EditMessageResponse> EditMessage(EditMessageRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, "Editing messages is not yet implemented"));
            return Task.FromResult(new EditMessageResponse
            {

            });
        }

        public override Task<DeleteMessageResponse> DeleteMessage(DeleteMessageRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, "Deleting messages is not yet implemented"));
            return Task.FromResult(new DeleteMessageResponse
            {

            });
        }
        #endregion

        #region ChatService
        private ChatListResponse CreateResponse(List<Chat> chats)
        {
            ChatListResponse response = new ChatListResponse();
            foreach (Chat chat in chats)
            {
                ChatResponse embedded = new ChatResponse()
                {
                    Id = chat.Id,
                    MessageCount = chat.MessageCount
                };
                embedded.Members.AddRange(chat.Members);
                response.Chats.Add(embedded);
            }
            return response;
        }
        public override Task<ChatResponse> GetChat(ChatRequest request, ServerCallContext context)
        {
            User user = _userService.GetFromToken(request.Token);
            Chat chat = _chatService.Get(request.Id);

            if (user == null)
                throw new RpcException(new Status(StatusCode.Unauthenticated, "Could not validate token"));

            if (chat == null)
                throw new RpcException(new Status(StatusCode.NotFound, "Could not find chat"));

            if (!chat.Members.Contains(user.Id))
                throw new RpcException(new Status(StatusCode.PermissionDenied, "Could not access chat"));
            ChatResponse response = new ChatResponse()
            {
                Id = chat.Id,
                MessageCount = chat.MessageCount
            };
            response.Members.AddRange(chat.Members);
            return Task.FromResult(response);
        }

        public override async Task GetChatList(ChatListRequest request, IServerStreamWriter<ChatResponse> responseStream, ServerCallContext context)
        {
            User user = _userService.GetFromToken(request.Token);
            if (user == null)
                throw new RpcException(new Status(StatusCode.Unauthenticated, "Could not validate token"));
            List<Chat> chats = _chatService.GetUserChats(user.Id);
            foreach (Chat c in chats)
            {
                ChatResponse cr = new ChatResponse
                {
                    Id = c.Id, 
                    MessageCount = c.MessageCount
                };
                cr.Members.AddRange(c.Members);
                await responseStream.WriteAsync(cr);
            }
        }

        public override async Task GetNew(NewRequest request, IServerStreamWriter<ChatResponse> responseStream, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, "Cannot get new messages at this time"));
            User user = _userService.GetFromToken(request.Token);
            if (user == null)
                throw new RpcException(new Status(StatusCode.Unauthenticated, "Could not validate token"));
            List<Chat> chats = _chatService.GetUserChats(user.Id);
            foreach (Chat c in chats)
            {
                ChatResponse cr = new ChatResponse
                {
                    Id = c.Id,
                    MessageCount = c.MessageCount
                };
                cr.Members.AddRange(c.Members);
                await responseStream.WriteAsync(cr);
            }
        }

        public override Task<QueueResponse> GetQueue(QueueRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, "Cannot get queue at this time"));
            return Task.FromResult(new QueueResponse
            {

            });
        }
        #endregion

        #region AuthService
        public override Task<SaltResponse> GetSalt(SaltRequest request, ServerCallContext context)
        {
            User user = _userService.GetFromUsername(request.Username);
            string salt;
            if (user == null)
            {
                // Generate a pseudo-random fake salt if user not found
                Random r = new Random();
                salt = new string(Enumerable.Repeat(
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789", 32
                    ).Select(s => s[r.Next(s.Length)]).ToArray());
                salt = Convert.ToBase64String(Encoding.ASCII.GetBytes(salt));
            }
            else
                 salt = user.Salt;
            return Task.FromResult(new SaltResponse
            {
                Salt = salt
            });
        }

        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            User user = _userService.GetFromUsername(request.Username);
            if (user == null || user.Hash != request.Hash) 
            {
                throw new RpcException(new Status(StatusCode.Unauthenticated, "Login Failed"));
            }
            return Task.FromResult(new LoginResponse
            {
                Id = user.Id, 
                Publickey = user.Publickey, 
                Token = user.Token, 
                Username = user.Username
            });
        }

        public override Task<UserResponse> GetUser(UserRequest request, ServerCallContext context)
        {
            User user = _userService.GetFromUsername(request.Username);
            if (user == null)
                throw new RpcException(new Status(StatusCode.NotFound, "User not found"));
            return Task.FromResult(new UserResponse
            {
                Username = user.Username,
                Publickey = user.Publickey
            });
        }

        public override Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            User user = _userService.GetFromUsername(request.Username);
            if (user != null)
                throw new RpcException(new Status(StatusCode.AlreadyExists, "Username already exists"));
            user = new User()
            {
                Id = ObjectId.GenerateNewId().ToString(), 
                Hash = request.Hash, 
                Publickey = request.Publickey, 
                Salt = request.Salt, 
                Token = Ulid.NewUlid().ToString(), 
                Username = request.Username
            };
            _userService.Create(user);
            return Task.FromResult(new RegisterResponse
            {
                Id = user.Id,
                Token = user.Token
            });
        }
        #endregion

        #region AccountService
        public override Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
        {
            throw new RpcException(new Status(StatusCode.Unimplemented, "Cannot change password at this time"));
            return Task.FromResult(new ChangePasswordResponse
            {

            });
        }
        #endregion
    }
}
