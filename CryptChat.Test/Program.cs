using System;
using Grpc.Net.Client;

using CryptChat.Core;
using CryptChat.Server;
using System.Threading.Tasks;
using Grpc.Core;

namespace CryptChat.Test
{
    class Program
    {
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
        static async Task Main(string[] args)
        {
            Core.Security.Utils.LoadMemoryKey();
            channel = GrpcChannel.ForAddress("https://localhost:5001");
            client = new Server.Server.ServerClient(channel);
            currentMenu = LoginMenu;
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
                    string hash = Core.Security.Password.HashPassword(password, salt.Salt);
                    LoginResponse success;
                    try
                    {
                        success = client.Login(new LoginRequest { Username = username, Hash = hash });
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
                }
            }

        }

        static void MainLoop()
        {
            while (running)
            {

            }
        }
    }
}
