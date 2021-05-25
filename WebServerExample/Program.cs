using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace Server
{
    internal class Program
    {
        private static readonly int _port = 50501;

        private static void Main(string[] args)
        {
            Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endpoint = new(IPAddress.Any, _port);

            socket.Bind(endpoint);
            socket.Listen(4);

            var responseSocket = socket.Accept();

            Span<byte> buffer = new(new byte[1024]);
            StringBuilder sb = new(responseSocket.Available);

            do
            {
                responseSocket.Receive(buffer);
                sb.Append(Encoding.UTF8.GetString(buffer));
            } while (responseSocket.Available > 0);

            Console.WriteLine(sb);
        }
    }
}
