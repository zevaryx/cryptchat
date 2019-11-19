using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using Google.Protobuf;
using MongoDB.Bson;

using CryptChatProtos.Responses;
using CryptChatProtos.Responses.Message;
using CryptChatProtos.Responses.Chat;
using CryptChatProtos.Responses.Auth;
using CryptChatProtos.Responses.Account;

using ProtoMaps = CryptChatProtos.Maps;
using CryptChatProtos.Requests;

namespace CryptChatServer
{
    // https://codinginfinite.com/multi-threaded-tcp-server-core-example-csharp/
    class TCPServer
    {
        private static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        private TcpListener server = null;

        public TCPServer(string ip, ushort port, bool autostart)
        {
            IPAddress bind_addr = IPAddress.Parse(ip);
            server = new TcpListener(bind_addr, port);
            server.Start();
            if (autostart)
                StartListener();
        }

        public void StartListener()
        {
            try
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    string cid = Utils.Generators.GenerateCID();
                    Logger.Debug($"Client joined with CID of {cid}");
                    bool added = Globals.CLIENTS.TryAdd(cid, client);
                    if (added)
                    {
                        Thread t = new Thread(_ => HandleDevice(client, cid));
                        t.Start();
                    }
                    else
                    {
                        Logger.Error($"Failed to add new client with CID {cid}");
                    }
                }
            }
            catch (SocketException e)
            {
                Logger.Fatal($"SocketException: {e}");
                server.Stop();
            }
        }

        public void HandleDevice(object obj, string cid)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
            List<byte> data = new List<byte>();
            byte[] buffer = new byte[512];
            int i;
            try
            {
                while (client.Connected)
                {
                    while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        data.AddRange(buffer[0..i]);
                    }
                    Request request = Request.Parser.ParseFrom(data.ToArray());
                    Response response = ProtoMaps.GetRangeResult(request.Type) switch
                    {
                        "Message" =>  Handlers.Message.ProcessRequest(request),
                        "Chat" => Handlers.Chat.ProcessRequest(request),
                        "Auth" => Handlers.Auth.ProcessRequest(request),
                        "Account" => Handlers.Account.ProcessRequest(request),
                        _ => Handlers.Defaults.ErrorResponse("Unknown request")
                    };
                    
                    byte[] output = response.ToByteArray();
                    stream.Write(output, 0, output.Length);
                }
            }
            catch (Exception e)
            {
                Logger.Error($"Client {cid} died. Reason: {e.Message}");
            }
            if (client.Connected)
                client.Close();
        }
    }
}
