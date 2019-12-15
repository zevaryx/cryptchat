using System;
using Grpc.Net.Client;

using CryptChat.Core.Security;
using CryptChat.Core.Models;
using CryptChat.Server;
using System.Threading.Tasks;
using Grpc.Core;
using Sodium;
using System.Text;
using System.IO;
using System.Collections.Generic;

namespace CryptChat.Test
{
    class Program
    {

        static List<Message> messages;


        private static User user;
        static string currentPrompt = "> ";
        static string currentMenu;
        static bool running = true;
        static bool loggedIn = false;
        static GrpcChannel channel;
        static Server.Server.ServerClient client;
        static string LoginMenu = @"
[L]ogin
[R]egister
[Q]uit
";
        static string MainMenu = @"
[M]essages
[N]ew message
[Q]uit
";
        static async Task Main(string[] args)
        {
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new Server.Server.ServerClient(channel);
            currentMenu = LoginMenu;
            // Let's test memory locking
            //SecureString locked = "Test";
            //string unlocked = locked;
            //Console.WriteLine(unlocked);
            //Console.WriteLine(locked);
            //_ = Console.ReadKey();
            Login();
            MainLoop();
        }

        static string MaskedInput()
        {
            string input = "";
            do
            {
                ConsoleKeyInfo key = Console.ReadKey(true);
                if (key.Key != ConsoleKey.Backspace && key.Key != ConsoleKey.Enter)
                {
                    input += key.KeyChar;
                    Console.Write("*");
                }
                else
                {
                    if (key.Key == ConsoleKey.Backspace && input.Length > 0)
                    {
                        input = input.Substring(0, (input.Length - 1));
                        Console.Write("\b \b");
                    }
                    else if (key.Key == ConsoleKey.Enter)
                    {
                        break;
                    }
                }
            } while (true);
            return input;
        }

        static void Login()
        {
            while (!loggedIn && running)
            {
                Console.WriteLine(currentMenu);
                Console.Write(currentPrompt);
                ConsoleKeyInfo input = Console.ReadKey();
                if (input.Key == ConsoleKey.Q)
                    running = false;
                else if (input.Key == ConsoleKey.L)
                {
                    Console.Write("\nEnter Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string password = MaskedInput();
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
                            PublicKey = success.Pubkey
                        };
                        user.LoadPrivateKey(password);
                        password = string.Empty;
                    }
                    catch (RpcException e)
                    {
                        Console.WriteLine($"\n\n{e.Status.StatusCode}: {e.Status.Detail}");
                    }
                }
                else if (input.Key == ConsoleKey.R)
                {
                    Console.Write("\nEnter Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string password = MaskedInput();
                    Console.Write("\nConfirm Password: ");
                    if (MaskedInput() != password)
                    {
                        Console.WriteLine("Error: Passwords did not match. Please try again");
                    }
                    Console.WriteLine("\nHashing password...");
                    var salt = Password.GenerateSalt();
                    var hash = Password.HashPassword(password, salt);
                    user = new User()
                    {
                        Username = username, 
                    };
                    Console.WriteLine($"Generating keys... Private key saved to {Base32.ToBase32String(GenericHash.Hash(Encoding.ASCII.GetBytes(username), null, 16))} subfolder");
                    user.LoadPrivateKey(password, false);
                    bool registered = false;
                    try
                    {
                        var response = client.Register(new RegisterRequest()
                        {
                            Salt = salt,
                            Hash = hash,
                            Pubkey = user.PublicKey,
                            Username = username
                        });
                        user.Id = response.Id;
                        user.Token = response.Token;
                        registered = true;
                        user.SavePrivateKey(password);
                        loggedIn = true;
                    }
                    catch (RpcException e)
                    {
                        Console.WriteLine($"\n\n{e.Status.StatusCode}: {e.Status.Detail}");
                    }
                    if (registered)
                        Console.WriteLine($"\nSuccessfully registered as {username}");
                }
            }

        }

        static void MainLoop()
        {
            while (running)
            {
                Console.WriteLine("Press any key to exit.....");
                Console.ReadKey();
                running = false;
            }
        }
    }
}
