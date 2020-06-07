using System;
using System.Net;
using System.Net.Sockets;

namespace ServerTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket socketListenner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 45698);         
            socketListenner.Bind(ipEnd);

            socketListenner.Listen(0);
            socketListenner.Accept();


        }
    }
}
