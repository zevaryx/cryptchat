using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using CryptChat.Client.Services;
using CryptChat.Client.Views;
using Grpc.Core;
using Grpc.Net.Client;

namespace CryptChat.Client
{
    public partial class App : Application
    {

        public static GrpcChannel channel;
        public static Server.Server.ServerClient client;

        public App()
        {
            InitializeComponent();

            channel = GrpcChannel.ForAddress("https://cryptchat.dragonfirecomputing.com:5001");
            client = new Server.Server.ServerClient(channel);

            DependencyService.Register<MockDataStore>();
            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
