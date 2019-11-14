using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace CryptChatServer
{
    // https://codinginfinite.com/multi-threaded-tcp-server-core-example-csharp/
    class TCPServer
    {
        private TcpListener server = null;

        public TCPServer(string ip, ushort port)
        {
            IPAddress bind_addr = IPAddress.Parse(ip);
            server = new TcpListener(bind_addr, port);
            server.Start();
            StartListener();
        }

        public void StartListener()
        {
            try
            {
                while (true)
                {
                    TcpClient client = server.AcceptTcpClient();
                    Thread t = new Thread(new ParameterizedThreadStart(HandleDevice));
                    t.Start(client);
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine($"SocketException: {e}");
                server.Stop();
            }
        }

        public void HandleDevice(object obj)
        {
            TcpClient client = (TcpClient)obj;
            var stream = client.GetStream();
        }
    }
}
