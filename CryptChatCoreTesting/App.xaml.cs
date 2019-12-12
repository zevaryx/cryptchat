using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Grpc.Net.Client;
using CryptChatCore.Protos;

namespace CryptChatCoreTesting
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static async Task Connect(string address)
        {
            var channel = GrpcChannel.ForAddress(address);
            var client = new Auth.AuthClient(channel);
            var reply = await client.GetSaltAsync(new SaltRequest { Username = "eragon5779" });
            Console.WriteLine(reply.Salt);
        }
    }
}
