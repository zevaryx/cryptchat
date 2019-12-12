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
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Security.Authentication;

namespace CryptChatServerTCP
{
    abstract class Server
    {
        protected static readonly NLog.Logger Logger = NLog.LogManager.GetCurrentClassLogger();
        protected TcpListener server = null;
        public abstract void StartListener();
        public abstract void HandleDevice(object obj, string cid);
    }
    class SSLServer : Server
    {
        private X509Certificate certificate = null;

        public SSLServer(string ip, ushort port, bool autostart, string certpath)
        {
            Logger.Debug($"Creatting SSL server using certificate {certpath}");
            certificate = X509Certificate.CreateFromCertFile(certpath);
            Logger.Debug($"Binding server to {ip}:{port}");
            IPAddress bind_addr = IPAddress.Parse(ip);
            server = new TcpListener(bind_addr, port);
            Logger.Debug($"Starting server");
            server.Start();
            if (autostart)
                StartListener();
        }

        public override void StartListener()
        {
            Logger.Debug("Listener started");
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

        public override void HandleDevice(object obj, string cid)
        {
            TcpClient client = (TcpClient)obj;
            using var stream = new SslStream(client.GetStream(), false);
            List<byte> data = new List<byte>();
            byte[] buffer = new byte[512];
            int i;
            try
            {
                stream.AuthenticateAsServer(certificate, clientCertificateRequired: false, checkCertificateRevocation: true);
                while (client.Connected)
                {
                    while ((i = stream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        data.AddRange(buffer[0..i]);
                    }
                    Request request = Request.Parser.ParseFrom(data.ToArray());
                    Response response = ProtoMaps.GetRangeResult(request.Type) switch
                    {
                        "Message" => Handlers.Message.ProcessRequest(request),
                        "Chat" => Handlers.Chat.ProcessRequest(request),
                        "Auth" => Handlers.Auth.ProcessRequest(request),
                        "Account" => Handlers.Account.ProcessRequest(request),
                        _ => Handlers.Defaults.ErrorResponse("Unknown request")
                    };

                    byte[] output = response.ToByteArray();
                    stream.Write(output, 0, output.Length);
                }
            }
            catch (AuthenticationException e)
            {
                Logger.Error($"Client {cid} failed to authenticate. Reason: {e.Message}");
            }
            catch (Exception e)
            {
                Logger.Error($"Client {cid} died. Reason: {e.Message}");
            }
            if (client.Connected)
            {
                stream.Close();
                client.Close();
            }
        }
    }
    // https://codinginfinite.com/multi-threaded-tcp-server-core-example-csharp/
    class TCPServer : Server
    {
        public TCPServer(string ip, ushort port, bool autostart)
        {
            IPAddress bind_addr = IPAddress.Parse(ip);
            server = new TcpListener(bind_addr, port);
            server.Start();
            if (autostart)
                StartListener();
        }

        public override void StartListener()
        {
            try
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    string cid = CryptChatServer.Utils.Generators.GenerateCID();
                    Logger.Debug($"Client joined with CID of {cid}");
                    bool added = CryptChatServer.Globals.CLIENTS.TryAdd(cid, client);
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

        public override void HandleDevice(object obj, string cid)
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
                        "Message" =>  CryptChatServer.Handlers.Message.ProcessRequest(request),
                        "Chat" => CryptChatServer.Handlers.Chat.ProcessRequest(request),
                        "Auth" => CryptChatServer.Handlers.Auth.ProcessRequest(request),
                        "Account" => CryptChatServer.Handlers.Account.ProcessRequest(request),
                        _ => CryptChatServer.Handlers.Defaults.ErrorResponse("Unknown request")
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
