using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Server_Models;
using System.Linq;

namespace Server_Repository
{
    public class ServerLogic : IServerLogic
    {
        public List<AConnection> AllClients { get; set; }                                           // A list of all the the connected clients
        private static Random random = new Random();                                                // Used to generate a new random 'ClientId'

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
        /// <param name="socketListenner">A pre bound System.Net.Sockets web socket</param>
        public void NewClientThread(Socket socketListenner)
        {
            try
            {
                // Accepts incommeng client
                socketListenner.Listen(0);                                                              // Lisen to the incomming socket
                Socket ClientSocket = socketListenner.Accept();                                         // Accept new incomming connections


                #region  Reads Display name, send by the client
                
                byte[] buffer = new byte[ClientSocket.SendBufferSize];                              // sets the sise of the byte array to the size of the socketClient's buffersize
                
                // resive data
                int readbyte = ClientSocket.Receive(buffer);                                        // the whole buffer (ca. 65.000 bytes) 

                // Do stuff with data
                byte[] resivedData = new byte[readbyte];                                            // A new array with a lengt equal to the buffer right above
                Array.Copy(buffer, resivedData, readbyte);                                          // copy only the amount of readbyte from buffer[] to resiveData[].

                #endregion

                if (!string.IsNullOrEmpty(readbyte.ToString()) == true)
                {
                    // Makes a new client thread
                    Thread newClientThread = new Thread(() => ClientConnection(ClientSocket));    // Calles a method on a new thread
                    newClientThread.Start();


                    #region Adding the new client to the list 'AllClients'

                    string clientNumber = RandomString(64);                                             // Generates a random string with 64 characters in it

                    // Creating a new instance of 'AConnection'
                    AConnection newClient = new AConnection() { ClientId = clientNumber, ClientThread = newClientThread, InGameStatus = false, ClientDisplayName = Encoding.ASCII.GetString(resivedData) };

                    AllClients.Add(newClient);                                                          // Adds 'newClient' to the list of all Clients 'AllClients'

                    #endregion
                }
                else {}
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Send the client back, a sorted list of all available Clients in form of a big string. 
        /// </summary>
        /// <param name="clientSocket">A pre bound System.Net.Sockets web socket</param>
        public void ClientConnection(Socket clientSocket)
        {
            try
            {
                #region Sort what Clients is not currently in game

                List<AConnection> AllSortedClients = new List<AConnection>();
                foreach (var Client in AllClients)
                {
                    if (Client.InGameStatus == false)
                    {
                        AllSortedClients.Add(Client);
                    }
                    else {}
                }

                #endregion

                #region piggyback data: Sends all sorted Client connections back to the Client as one big string

                string allClientConnections = "";
                foreach (var Client in AllSortedClients)
                {
                    string aClientAndConnection;
                    string clientId = Client.ClientId.ToString();
                    string ClientConnection = Client.ClientThread.ToString();
                    string InGameStatus = Client.InGameStatus.ToString();

                    // Connects the clientId, InGameStatus and ClientConnection into one string (~ = end of one 'aClientAndConnection') 
                    aClientAndConnection = clientId + "#" + InGameStatus + "#" + ClientConnection + "~";

                    allClientConnections = allClientConnections + aClientAndConnection;         // Adds 'aClientAndConnection' to the string 'allClientConnections'
                }

                // piggyback data back to client after the client have connected
                clientSocket.Send(Encoding.ASCII.GetBytes(allClientConnections.ToString()));    // Sendsa list of AllClients in form of one big string 'allClientConnections'

                #endregion
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Generates a random alphanumeric string
        /// </summary>
        /// <param name="length">length of the random alphanumeric string</param>
        /// <returns>A string</returns>
        public static string RandomString(int length)
        {
            // Credit: Liam & dtb
            // Link: https://stackoverflow.com/a/1344242

            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
