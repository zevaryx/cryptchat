using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptChat.Server;
using Grpc.Core;

using CCServer = CryptChat.Server.Server;

namespace CryptChat.Server.Services
{
    public class ServerService : CCServer.ServerBase
    {
        private readonly ILogger<ServerService> _logger;
        public ServerService(ILogger<ServerService> logger)
        {
            _logger = logger;
        }
        #region MessageService
        public override Task<MessageResponse> GetMessage(MessageRequest request, ServerCallContext context)
        {
            return Task.FromResult(new MessageResponse
            {

            });
        }

        public override Task<SendMessageResponse> SendMessage(SendMessageRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SendMessageResponse
            {

            });
        }

        public override Task<EditMessageResponse> EditMessage(EditMessageRequest request, ServerCallContext context)
        {
            return Task.FromResult(new EditMessageResponse
            {

            });
        }

        public override Task<DeleteMessageResponse> DeleteMessage(DeleteMessageRequest request, ServerCallContext context)
        {
            return Task.FromResult(new DeleteMessageResponse
            {

            });
        }
        #endregion

        #region ChatService
        public override Task<ChatResponse> GetChat(ChatRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ChatResponse
            {

            });
        }

        public override Task<ChatListResponse> GetChatList(ChatListRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ChatListResponse
            {

            });
        }

        public override Task<ChatListResponse> GetNew(NewRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ChatListResponse
            {

            });
        }

        public override Task<QueueResponse> GetQueue(QueueRequest request, ServerCallContext context)
        {
            return Task.FromResult(new QueueResponse
            {

            });
        }
        #endregion

        #region AuthService
        public override Task<SaltResponse> GetSalt(SaltRequest request, ServerCallContext context)
        {
            return Task.FromResult(new SaltResponse
            {
                Salt = "Test Salt"
            });
        }

        public override Task<LoginResponse> Login(LoginRequest request, ServerCallContext context)
        {
            return Task.FromResult(new LoginResponse
            {

            });
        }

        public override Task<UserResponse> GetUser(UserRequest request, ServerCallContext context)
        {
            return Task.FromResult(new UserResponse
            {

            });
        }

        public override Task<RegisterResponse> Register(RegisterRequest request, ServerCallContext context)
        {
            return Task.FromResult(new RegisterResponse
            {

            });
        }
        #endregion

        #region AccountService
        public override Task<ChangePasswordResponse> ChangePassword(ChangePasswordRequest request, ServerCallContext context)
        {
            return Task.FromResult(new ChangePasswordResponse
            {

            });
        }
        #endregion
    }
}
