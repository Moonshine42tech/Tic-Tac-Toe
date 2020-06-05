using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Server_Models;

namespace Server_Repository
{
    public class ServerLogic : IServerLogic
    {
        public List<AConnection> AllClients { get; set; }                                        // A list of all the the connected clients

        /// <summary>
        /// Tells a given web socket to lisen on a specific ip addres and port.
        /// </summary>
        /// <param name="socketListenner">a System.Net.Sockets socket </param>
        /// <param name="ipAddress">Ip Address</param>
        /// <param name="portNumber">Port Number</param>
        /// <returns>A socket bound to an Ip address and port number</returns>
        /// <exception cref="NullReferenceException">Returns a default connection to localhost on port 8888</exception>
        public Socket StartLiseningForConnections(Socket socketListenner, string ipAddress, int portNumber)
        {
            try
            {
                // listening to new clients
                socketListenner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ipAddress), portNumber);          // Ipadress and port number for the endpoint

                socketListenner.Bind(ipEnd);                                                        // Binds the endpint to the socket

                return socketListenner;                                                             // Returns the new bound socket
            }
            catch (NullReferenceException e)
            {
                Socket NewsocketListenner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 8888);              // Ipadress and port number for the endpoint

                NewsocketListenner.Bind(ipEnd);                                                     // Binds the endpint to the socket

                return NewsocketListenner;                                                          // Returns a socket bound to the localhost on port 8888
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        /// <summary>
        /// Makes a new thread for every new client connection
        /// </summary>
        /// <param name="socketListenner">A pro bound System.Net.Sockets web socket</param>
        public void NewClientThread(Socket socketListenner)
        {
            try
            {
                socketListenner.Listen(0);                                                          // Lisen to the incomming socket
                Socket ClientSocket = socketListenner.Accept();                                     // Accept new incomming connections

                Thread clientThread = new Thread(() => ClientConnection(ClientSocket));             // Calles a method on a new thread
                clientThread.Start();

                AllThreads.Add(clientThread);
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientSocket"></param>
        public void ClientConnection(Socket clientSocket)
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


                if (resivedData.ToString() == "~#^42")
                {
                    break;
                }

                Console.WriteLine(Encoding.ASCII.GetString(resivedData));

                // piggyback data back to client
                // clientSocket.Send(Encoding.ASCII.GetBytes("your message was: " + Encoding.ASCII.GetString(resivedData)));

            } while (readbyte > 0);


            Console.WriteLine("Client is disconected");

            #region Thread Cleanup and Closes current thread

            foreach (var aThread in AllClients) 
            {
                if (aThread.ClientThread == Thread.CurrentThread)        // Removes the client and the current Thread from the list 'AllClients'
                {
                    AllClients.Remove(aThread);
                } 
                else {}
            }

            Thread.CurrentThread.Abort();                   // Close current thread

            if (Thread.CurrentThread.IsAlive == true)       // Check if current thread is still alive
            {
                Console.WriteLine("Somthing went wrong: Connection did not close propperly");
            }

            #endregion

            Console.Read();
        }
    }
}
