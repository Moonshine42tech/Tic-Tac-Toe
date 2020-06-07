using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);
            master.Connect(ipEnd);


            Console.WriteLine("Give me a string! ... nom");
            string myString = Convert.ToString(Console.ReadLine());

            master.Send(Encoding.ASCII.GetBytes(myString));

            Console.Read();
        }
    }
}
