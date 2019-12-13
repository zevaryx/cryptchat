using System;
using Grpc.Net.Client;

using CryptChat.Core;
using CryptChatServer;
using System.Threading.Tasks;

namespace CryptChat.Test
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Core.Security.Utils.LoadMemoryKey();
            Console.WriteLine(Convert.ToBase64String(Core.Security.Utils.MemoryKey.ToArray()));
            var channel = GrpcChannel.ForAddress("https://localhost:5001");
            var client = new CryptChatServer.Server.ServerClient(channel);
            var reply = await client.GetSaltAsync(new SaltRequest { Username = "eragon5779" });
            Console.WriteLine($"Salt for eragon5779: {reply.Salt}");
        }
    }
}
