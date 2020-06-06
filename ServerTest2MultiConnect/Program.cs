using System;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;


// Video guide to learn about web sokets: https://www.youtube.com/watch?v=FYLMxrN5c6g
// Video on Web socket server : https://www.youtube.com/watch?v=ycVgXe6v1VQ
// this program can so far only take one connection.

namespace ServerTest2MultiConnect
{
    class Program
    {
        private readonly string ip = "127.0.0.1";                                               // localhost
        private readonly int port = 8888;

        static void Main(string[] args)
        {
            Program p = new Program();

            // listening to new clients
            Socket socketListenner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(p.ip), p.port);                   // Ipadress and port number for the endpoint

            socketListenner.Bind(ipEnd);                                                        // Binds the endpint to the socket

            int clientId = 0;

            while (true)
            {
                socketListenner.Listen(0);                                                          // Lisen to the socket socketListenner
                Socket ClientSocket = socketListenner.Accept();                                     // Accept new incomming connections

                Thread clientThread = new Thread(() => ClientConnection(ClientSocket, clientId));   // Calles a method on a new thread
                clientThread.Start();

                clientId++;
            }
        }

        private static void ClientConnection(Socket clientSocket, int clientId)
        {
            // Contains the socket connection when a connection to a client has been made
            byte[] buffer = new byte[clientSocket.SendBufferSize];                              // sets the sise of the byte array to the size of the socketClient's buffersize

            int readbyte;
            do
            {
                // resive data
                readbyte = clientSocket.Receive(buffer);                                        // the whole buffer (ca. 65.000 bytes) 

                // Do stuff with data
                byte[] resivedData = new byte[readbyte];                                        // A new array with a lengt equal to the buffer right above
                Array.Copy(buffer, resivedData, readbyte);                                      // copy only the amount of readbyte from buffer[] to resiveData[].

                //string clinentIdString = "<" + clientId.ToString() + "> "; 
                //Console.WriteLine(clinentIdString + Encoding.ASCII.GetString(resivedData));

               

                //// piggyback data back to client
                //clientSocket.Send(Encoding.ASCII.GetBytes("your message was: " + Encoding.ASCII.GetString(resivedData)));

            } while (readbyte > 0);

            Thread.CurrentThread.Abort();
            if (Thread.CurrentThread.IsAlive == true)
            {
                Console.WriteLine("Somthing went wrong: Connection did not close propperly");
            }
            Console.WriteLine("Client is disconected");
            Console.Read();
        }

        /// <summary>
        /// Helper Method - Deserializes a byte[] array into a GameObject DTO
        /// </summary>
        /// <param name="inData">Serilized byte[] array</param>
        /// <returns>A GameObject</returns>
        private static DTOs.GameObject ByteTOGameObject(byte[] inData)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            ms.Write(inData, 0, inData.Length);             // write to the memorystream
            ms.Seek(0, SeekOrigin.Begin);                   // sets memorystream curser ack  to start (0)

            object o = (object)bf.Deserialize(ms);
            return (DTOs.GameObject)o;
        }
    }
}
