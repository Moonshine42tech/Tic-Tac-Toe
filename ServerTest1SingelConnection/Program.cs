using System;
using System.Net.Sockets;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Text;


// Video guide to learn about web sokets: https://www.youtube.com/watch?v=FYLMxrN5c6g

// this program can so far only take one connection.


namespace ServerTest1SingelConnection
{
    class Program
    {
        private readonly string ip = "127.0.0.1";                                               // localhost
        private readonly int port = 8888;

        static void Main(string[] args)
        {
            Program p = new Program();

            // listening  to new clients
            Socket socketListenner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(p.ip), p.port);                   // Ipadress and port number for the endpoint
            
            socketListenner.Bind(ipEnd);                                                        // Binds the endpint to the socket
            socketListenner.Listen(0);                                                          // 


            // Contains the socket connection when a connection to a client has been made
            Socket SocketClient = socketListenner.Accept();
            byte[] buffer = new byte[SocketClient.SendBufferSize];                              // sets the sise of the byte array to the size of the socketClient's buffersize

            int readbyte;
            do
            {
                // resive data
                readbyte = SocketClient.Receive(buffer);             // the whole buffer (ca. 65.000 bytes) 

                // Do stuff with data
                byte[] resivedData = new byte[readbyte];              // A new array with a lengt equal to the buffer right above
                Array.Copy(buffer, resivedData, readbyte);            // copy only the amount of readbyte from buffer[] to resiveData[].

                Console.WriteLine(Encoding.ASCII.GetString(resivedData));

                // piggyback data back to client
                SocketClient.Send(Encoding.ASCII.GetBytes("your message was: " + Encoding.ASCII.GetString(resivedData)));

                Console.WriteLine();
            } while (readbyte > 0);

            Console.WriteLine("Client is disconected");
            Console.Read();


        }
    }
}
