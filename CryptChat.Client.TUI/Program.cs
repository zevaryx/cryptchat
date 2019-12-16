using CryptChat.Core.Models;
using CryptChat.Core.Security;
using CryptChat.Server;
using Grpc.Core;
using Grpc.Net.Client;
using System;
using System.Collections.Generic;
using Terminal.Gui;

namespace CryptChat.Client.TUI
{
    class Program
    {
        static List<Message> messages = new List<Message>();
        static Dictionary<string, string> keylist = new Dictionary<string, string>();
        private static User user;
        static bool loggedIn = false;
        static GrpcChannel channel;
        static Server.Server.ServerClient client;

        static void Main(string[] args)
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new Server.Server.ServerClient(channel);

            Application.Init();
            var top = Application.Top;

            var win = new Window("CryptChat")
            {
                X = 0,
                Y = 1,
                Width = Dim.Fill(),
                Height = Dim.Fill()
            };
            top.Add(win);
            var menu = new MenuBar(new MenuBarItem[] {
                new MenuBarItem ("_File", new MenuItem [] {
                    new MenuItem ("_Quit", "", () => { Application.RequestStop(); top.Running = false; })
                })
            });
            top.Add(menu);

            var login = new Label("Login: ") { X = 3, Y = 2 };
            var password = new Label("Password: ")
            {
                X = Pos.Left(login),
                Y = Pos.Top(login) + 1
            };
            var loginText = new TextField("")
            {
                X = Pos.Right(password),
                Y = Pos.Top(login),
                Width = 40
            };
            var passText = new TextField("")
            {
                Secret = true,
                X = Pos.Left(loginText),
                Y = Pos.Top(password),
                Width = Dim.Width(loginText)
            };

            var loginButton = new Button(3, 14, "Login");
            loginButton.Clicked += () => { Login(loginText.Text.ToString(), passText.Text.ToString()); } ;

            // Add some controls, 
            win.Add(
                // The ones with my favorite layout system
                login, password, loginText, passText,
                loginButton,
                    new Button(10, 14, "Register"),
                    new Label(3, 18, "Press F9 or ESC plus 9 to activate the menubar"));

            Application.Run();
        }

        static void Login(string username, SecureString password)
        {
            var salt = client.GetSalt(new SaltRequest { Username = username });
            string hash = Password.HashPassword(password, salt.Salt);
            LoginResponse success;
            try
            {
                success = client.Login(new LoginRequest { Username = username, Hash = hash });
                user = new User()
                {
                    Username = success.Username,
                    Id = success.Id,
                    Token = success.Token,
                    PublicKey = success.Publickey
                };
                user.LoadPrivateKey(password);
                password.Dispose();
                loggedIn = true;
            }
            catch (RpcException e)
            {
                Console.WriteLine($"\n\n{e.Status.StatusCode}: {e.Status.Detail}");
            }
        }
    }
}
