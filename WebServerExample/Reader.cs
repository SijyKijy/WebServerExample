using System;
using System.Net.Sockets;
using System.Text;

namespace WebServerExample.Server
{
    public class Reader
    {
        private const int _bufferSize = 1024 * 4;

        private readonly byte[] _buffer;
        private readonly char[] _charBuffer;

        public Reader()
        {
            _buffer = new byte[_bufferSize];
            _charBuffer = new char[_bufferSize];
        }

        public void PrintData(Socket responseSocket)
        {
            Span<byte> buffer = new(_buffer);
            Span<char> chars = new(_charBuffer);

            Encoding enc = Encoding.UTF8;
            do
            {
                responseSocket.Receive(buffer);
                enc.GetDecoder().GetChars(buffer, chars, true);
                Console.Write(_charBuffer);
            } while (responseSocket.Available > 0);
        }
    }
}
