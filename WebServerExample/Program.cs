using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using WebServerExample.Server;

namespace Server
{
    internal class Program
    {
        private static readonly int _port = 50501;

        private static void Main(string[] args)
        {
            Socket socket = new(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint endpoint = new(IPAddress.Any, _port);
            Reader reader = new();

            socket.Bind(endpoint);
            socket.Listen(4);

            while (true)
            {
                var responseSocket = socket.Accept();

                reader.PrintData(responseSocket);

                SendHttp(responseSocket);

                responseSocket.Close();
            }
        }

        private static void SendHttp(Socket response)
        {
            const string headers = "HTTP/2.0 200 OK\r\nContent-Type: text/html\r\n\r\n";
            const string content = "Hello world!";

            response.Send(Encode(headers + content));
        }



        private static ReadOnlySpan<byte> Encode(string message) => Encoding.UTF8.GetBytes(message);
    }
}
