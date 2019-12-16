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

        static List<Message> messages = new List<Message>();
        static Dictionary<string, string> keylist = new Dictionary<string, string>();

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
[C]hats
[L]ogout
[Q]uit
";
        static string MsgMenu = @"

[S]ync Messages
[N]ew Message
[V]iew Messages
[B]ack
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
                Console.WriteLine(LoginMenu);
                Console.Write(currentPrompt);
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.Q)
                    running = false;
                else if (input.Key == ConsoleKey.L)
                {
                    Console.Write("\nEnter Username: ");
                    string username = Console.ReadLine();
                    Console.Write("Enter Password: ");
                    string password = MaskedInput();
                    Console.WriteLine();
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
                        password = string.Empty;
                        Console.WriteLine($"\nLogged in as {user.Username}");
                        currentPrompt = user.Username + " > ";
                        loggedIn = true;
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
                            Publickey = user.PublicKey,
                            Username = username
                        });
                        user.Id = response.Id;
                        user.Token = response.Token;
                        registered = true;
                        user.SavePrivateKey(password);
                        loggedIn = true;
                        currentPrompt = user.Username + " > ";
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

        static void NewMessage()
        {
            try
            {
                Console.Write("\nRecipient: ");
                var other_username = Console.ReadLine();
                var recipient = client.GetUser(new UserRequest()
                {
                    Username = other_username
                });
                Console.Write("Message: ");
                var message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                {
                    Console.WriteLine("Empty message are not allowed.");
                    return;
                }
                using SecureString key = Core.Security.Boxes.Symmetric.GenerateKey();
                var rec_key_enc = Core.Security.Boxes.Asymmetric.Encrypt(key, user.PrivateKey, recipient.Publickey);
                var send_key_enc = Core.Security.Boxes.Asymmetric.Encrypt(key, user.PrivateKey, user.PublicKey);
                var msg_enc = Core.Security.Boxes.Symmetric.Encrypt(message, key);
                SendMessageRequest new_message = new SendMessageRequest()
                {

                    Token = user.Token,
                    Message = msg_enc["crypt"],
                    Nonce = msg_enc["nonce"],
                    Recipient = other_username,
                    Sender = user.Username,
                };
                new_message.Keys.Add(other_username, $"{rec_key_enc["nonce"]}:{rec_key_enc["crypt"]}");
                new_message.Keys.Add(user.Username, $"{send_key_enc["nonce"]}:{send_key_enc["crypt"]}");

                //Console.WriteLine(new_message);

                var message_request = client.SendMessage(new_message);

                var msg = new Message()
                {
                    ID = message_request.Id,
                    Chat = message_request.Chat,
                    EncryptedKey = $"{send_key_enc["nonce"]}:{send_key_enc["crypt"]}",
                    EncryptedMessage = msg_enc["crypt"],
                    Nonce = msg_enc["nonce"],
                    Sender = user.Username,
                    Timestamp = message_request.Timestamp
                };

                msg.Decrypt(user.PrivateKey, user.PublicKey);

                messages.Add(msg);

                Console.WriteLine("Message sent");
            }
            catch (RpcException e)
            {
                Console.WriteLine($"\n\n{e.Status.StatusCode}: {e.Status.Detail}");
            }
        }

        static void SyncMessages()
        {
            Console.WriteLine("\nGetting messages...");
            var msgs = client.GetAllMessages(new SyncRequest() { Token = user.Token });
            foreach (var message in msgs.Messages)
            {
                if (!messages.Exists(m => m.ID == message.Id))
                    messages.Add(new Message()
                    {
                        ID = message.Id, 
                        Chat = message.Chat, 
                        Edited = message.Edited, 
                        Edited_At = message.EditedAt, 
                        EncryptedFile = message.File, 
                        EncryptedKey = message.Key, 
                        EncryptedMessage = message.Message, 
                        Nonce = message.Nonce, 
                        Sender = message.Sender, 
                        Timestamp = message.Timestamp
                    });
                if (!keylist.ContainsKey(message.Sender))
                {
                    var sender = client.GetUser(new UserRequest() { Username = message.Sender });
                    keylist.Add(message.Sender, sender.Publickey);
                }
            }
            Console.WriteLine("Decrypting...");
            messages.ForEach(m => m.Decrypt(user.PrivateKey, keylist[m.Sender]));
            Console.WriteLine("Messages synced");
        }

        static void MessageMenu()
        {
            bool inMenu = true;
            while (running && inMenu)
            {
                Console.WriteLine(MsgMenu);
                Console.Write(currentPrompt);
                var input = Console.ReadKey();
                if (input.Key == ConsoleKey.N)
                    NewMessage();
                if (input.Key == ConsoleKey.S)
                    SyncMessages();
                if (input.Key == ConsoleKey.Q)
                    running = false;
                if (input.Key == ConsoleKey.B)
                    inMenu = false;
            }
        }

        static void MainLoop()
        {
            while (running)
            {
                if (!loggedIn)
                    Login();
                else
                {
                    Console.WriteLine(MainMenu);
                    Console.Write(currentPrompt);
                    var input = Console.ReadKey();
                    if (input.Key == ConsoleKey.Q)
                        running = false;
                    if (input.Key == ConsoleKey.L)
                    {
                        user = null;
                        loggedIn = false;
                        currentPrompt = "> ";
                    }
                    if (input.Key == ConsoleKey.M)
                        MessageMenu();
                }
            }
        }
    }
}
