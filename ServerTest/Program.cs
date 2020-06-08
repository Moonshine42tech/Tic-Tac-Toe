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
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);         
            socketListenner.Bind(ipEnd);

            socketListenner.Listen(0);
            Socket ClientSocket = socketListenner.Accept();

            byte[] buffer = new byte[ClientSocket.SendBufferSize];

            int readbyte;
            do
            {
                readbyte = ClientSocket.Receive(buffer);
                byte[] rData = new byte[ClientSocket.SendBufferSize];
                Array.Copy(buffer, rData, readbyte);


                Console.WriteLine(readbyte.ToString());
            } while (readbyte > 0);




            Console.WriteLine("Client disconected");
            Console.Read();
        }
    }
}
