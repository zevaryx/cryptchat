using System;
using System.Collections.Generic;
using System.Text;
using CryptChat.Core.Models;
using CryptChat.Server;
using Grpc.Net.Client;

namespace CryptChat.Core.RPC
{
    public sealed class RPCHandler
    {
        public User User { get; private set; }
        private static readonly Lazy<RPCHandler> lazy = new Lazy<RPCHandler>(() => new RPCHandler());
        public Server.Server.ServerClient Client { get; private set; }
        private GrpcChannel _channel;
        private RPCHandler()
        {
            _channel = GrpcChannel.ForAddress("https://localhost:5001");
            Client = new Server.Server.ServerClient(_channel);
        }

        public void ChangeChannel(string address)
        {
            _channel = GrpcChannel.ForAddress(address);
            Client = new Server.Server.ServerClient(_channel);
        }

        public void SetUser(User user)
        {
            if (user != null)
                User = user;
        }

        public void ClearUser()
        {
            User = null;
        }

        public static RPCHandler Instance { get => lazy.Value; }

    }
}
