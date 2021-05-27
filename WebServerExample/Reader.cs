using System;
using System.Net.Sockets;
using System.Text;

namespace WebServerExample.Server
{
    public class Reader
    {
        private const int _bufferSize = 1024 * 4;

        public void PrintData(Socket responseSocket)
        {
            Span<byte> buffer = new(new byte[_bufferSize]);
            Span<char> chars = new(new char[_bufferSize]);

            Encoding enc = Encoding.UTF8;

            do
            {
                responseSocket.Receive(buffer);
                enc.GetDecoder().GetChars(buffer, chars, true);

                Console.WriteLine(chars.ToString());
            } while (responseSocket.Available > 0);
        }
    }
}
