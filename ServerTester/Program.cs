using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;


// Video guide to learn about web sokets: https://www.youtube.com/watch?v=KxdOOk6d_I0


namespace ServerTester
{
    class Program
    {
        static void Main(string[] args)
        {
            // Creating a TCP socet lisener
            IPAddress ip = Dns.GetHostEntry("localhost").AddressList[0];
            TcpListener server = new TcpListener(ip, 8080);                     // an TcpListener needs an IP address and a Port number
            TcpClient client = default(TcpClient);

            try
            {
                server.Start(); // tryes to start the TcpListener called server.
                Console.WriteLine("server started...");

                Console.Read();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                Console.Read();
            }

            while (true)
            {

            }
        }
    }
}
