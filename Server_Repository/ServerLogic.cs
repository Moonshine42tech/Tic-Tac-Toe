﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using Server_Models;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace Server_Repository
{
    public class ServerLogic : IServerLogic
    {
        public List<AClientConnection> AllClients { get; set; }                                     // A list of all the the connected clients

        #region Private members
        private static Random random = new Random();                                                // Used to generate a new random 'ClientId'
        private readonly object LockObject = new object();                                          // A object used for a threadsafe lock
        #endregion


        #region START / STOP SERVER

        /// <summary>
        /// Makes a socket to lisen on a given ip addres and port number.
        /// </summary>
        /// <param name="ipAddress">Ip Address</param>
        /// <param name="portNumber">Port Number</param>
        /// <returns>A socket bound to an Ip address and port number</returns>
        /// <exception cref="NullReferenceException">Returns a default connection to localhost on port 8888</exception>
        public Socket StartLiseningForConnections(string ipAddress, int portNumber)
        {
            try
            {
                // listening to new clients
                Socket socketListenner = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
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
        /// Stops the Server socket lisener
        /// </summary>
        /// <param name="master">Socket</param>
        public void StopServerConnection(Socket master)
        {
            try
            {
                master.Disconnect(false);               // Disconnects the active socket
                master.Dispose();                       // Kills the socket
            }
            catch (Exception e)
            {
                throw e;
            }
                          
        }

        #endregion


        #region Client Logic

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


                #region  Reads data, send by the client

                byte[] buffer = new byte[ClientSocket.SendBufferSize];                              // sets the sise of the byte array to the size of the socketClient's buffersize

                // resive data
                int readbyte = ClientSocket.Receive(buffer);                                        // the whole buffer (ca. 65.000 bytes) 

                // Do stuff with data
                byte[] resivedData = new byte[readbyte];                                            // A new array with a lengt equal to the buffer right above
                Array.Copy(buffer, resivedData, readbyte);                                          // copy only the amount of readbyte from buffer[] to resiveData[].

                #endregion


                if (!string.IsNullOrEmpty(readbyte.ToString()) == true)
                {
                    lock (LockObject)
                    {
                        // Makes a new client thread
                        Thread newClientThread = new Thread(() => ClientMethodCalls(ClientSocket, resivedData));      // Calles a method on a new thread
                        newClientThread.Start();
                    }
                }
                else { }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        
        /// <summary>
        /// Uses the first index in the data from the client to Call methods
        /// </summary>
        /// <param name="clientSocket">A System.Net.Sockets Socket</param>
        /// <param name="resivedData">Data send from the client</param>
        public void ClientMethodCalls(Socket clientSocket, byte[] resivedData)
        {
            try
            {
                while (true)
                {

                    string clientDataString = ConvertByteArrayToData(resivedData);                      // Deserializees the data the client has send. | returns one big data string
                    clientDataString.Split('~');
                    switch (clientDataString[0])
                    {
                        #region case 0: Make a new client
                        case '0':   // Make a new client
                            AClientConnection newClient = new AClientConnection();

                            #region Data from the client
                            newClient.ClientDisplayName = clientDataString[1].ToString();
                            newClient.HasGameEnded = Convert.ToBoolean(clientDataString[2]);
                            newClient.IsPlayer1Turn = Convert.ToBoolean(clientDataString[3]);

                            // Concerts the gameboard in char format to an array of 'GameSymbolTypes'
                            IEnumerable<GameSymbolTypes> gameboardSymbilsArray = clientDataString[4].ToString().Select(a => (GameSymbolTypes)Enum.Parse(typeof(GameSymbolTypes), a.ToString()));
                            newClient.GameboardFildsList = (GameSymbolTypes[])gameboardSymbilsArray;
                            #endregion

                            newClient.ClientId = RandomString(64);                                      // Generates a new 'ClientId' that is a random string with 64 characters in it
                            newClient.ClientSocket = clientSocket;                                      // Asign the acceptet socked to the client
                            newClient.InGameStatus = false;                                             // Not curently in game

                            #region makes sure ClientId is uniqe
                            bool IdCheck = false;
                            while (IdCheck == true)
                            {
                                IdCheck = false;
                                foreach (var AclientId in AllClients)
                                {
                                    if (newClient.ClientId == AclientId.ClientId)
                                    {
                                        IdCheck = true;
                                    }
                                }
                                if (IdCheck == true)
                                {
                                    newClient.ClientId = RandomString(64);
                                }
                            }
                            #endregion

                            ReturnClientList(newClient.ClientSocket);                                   // Sends a list of clients back to the new client

                            AllClients.Add(newClient);                                                  // Adds the new client to the list of all clients
                            break;
                        #endregion

                        #region case 1: Connect two players 
                        case '1':       
                            // Takes two values:
                            // 1, oponentId

                            AClientConnection me = new AClientConnection();
                            AClientConnection oponent = new AClientConnection();        // my oponent

                            foreach (var client in AllClients)
                            {
                                if (client.ClientId == clientDataString[1].ToString())  // find oponent
                                {
                                    oponent = client;                                   // set oponent client = AClientConnection oponent
                                }

                                if (client.ClientSocket == clientSocket)                // find myself
                                {
                                    me = client;                                        // set my client object = AClientConnection me
                                }
                            }

                            // Connect two players. 
                            oponent.Oponent = me;       // I am his
                            me.Oponent = oponent;       // he is mine

                            // updates InGameStatus
                            oponent.InGameStatus = true;
                            me.InGameStatus = true;

                            // piggyback data: returns an updated InGameStatus for both players
                            oponent.ClientSocket.Send(Encoding.ASCII.GetBytes(ConvertDataToByteArray(oponent.InGameStatus).ToString()));
                            me.ClientSocket.Send(Encoding.ASCII.GetBytes(ConvertDataToByteArray(me.InGameStatus).ToString()));

                            break;
                        #endregion

                        #region case 2:
                        case '2': // 

                            AClientConnection myself = new AClientConnection();                         // a placholder for my own client object

                            AllClients.ForEach(o =>                                                     // Finding my own client object
                            {
                                if (o.ClientId == myself.ClientId)
                                {
                                    myself = o;
                                }
                            });

                            // Sets
                            myself.Oponent.HasGameEnded = true;
                            myself.Oponent.ClientSocket.Send(Encoding.ASCII.GetBytes(ConvertDataToByteArray(myself.Oponent.HasGameEnded).ToString()));
                            break;
                        #endregion

                        #region case 3:
                        case '3':  


                            break;
                        #endregion

                        default:    // kill connection
                            clientSocket.Disconnect(false);
                            clientSocket.Dispose();
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Returns a list of all available clients
        /// </summary>
        /// <param name="clientSocket">A System.Net.Sockets Socket</param>
        public void ReturnClientList(Socket clientSocket)
        {
            #region Sort what Clients is not currently in game

            List<AClientConnection> AllSortedClients = new List<AClientConnection>();       // placeholder for sorted data

            foreach (var Client in AllClients)                                              // Sort data by InGameStatus
            {
                if (Client.InGameStatus == false)
                {
                    AllSortedClients.Add(Client);
                }
                else { }
            }

            #endregion

            #region piggyback data: Sends all sorted Client list back to the Client as one big string

            string allClientConnections = "";
            foreach (var Client in AllSortedClients)
            {
                string aClientAndConnection;
                string clientId = Client.ClientId.ToString();
                string ClientDisplayName = Client.ClientDisplayName;

                // Connects the clientId and ClientDisplayName into one string (~ = end of every 'aClientAndConnection') 
                aClientAndConnection = clientId + "#" + ClientDisplayName + "~";

                allClientConnections = (allClientConnections += aClientAndConnection);         // Append 'aClientAndConnection' to the string 'allClientConnections'
            }

            // piggyback data back to client after the client have connected
            clientSocket.Send(Encoding.ASCII.GetBytes(allClientConnections.ToString()));    // Sendsa list of AllClients in form of one big string 'allClientConnections'

            #endregion
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

        #endregion


        #region Multoplayer only logic 

        /// <summary>
        /// Serializes Game data into a byte[] array
        /// </summary>
        /// <param name="DisplayName">String from UI</param>
        /// <param name="hasGameEnded">TTT_Models.GameModel</param>
        /// <param name="isPlayer1Turn">TTT_Models.GameModel</param>
        /// <param name="X_Score">TTT_Models.ScoreBoard</param>
        /// <param name="O_Score">TTT_Models.ScoreBoard</param>
        /// <param name="gameboard">TTT_Models.GameModel</param>
        /// <returns>byte[] Array</returns>
        public byte[] ConvertDataToByteArray(string DisplayName, bool hasGameEnded, bool isPlayer1Turn, int X_Score, int O_Score, GameSymbolTypes[] gameboard)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            // Make one big datastring
            string dataString = (DisplayName + "~" + hasGameEnded + "~" + isPlayer1Turn + "~" + gameboard);

            // Serialize the dataString and returns is in form of a array 
            bf.Serialize(ms, dataString);
            return ms.ToArray();
        }

        /// <summary>
        /// Serializes Game data into a byte[] array
        /// </summary>
        /// <param name="aBoolValue">bool</param>
        /// <returns>byte[] Array</returns>
        public byte[] ConvertDataToByteArray(bool aBoolValue)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            // Make one big datastring
            string dataString = aBoolValue.ToString();

            // Serialize the dataString and returns is in form of a array 
            bf.Serialize(ms, dataString);
            return ms.ToArray();
        }


        /// <summary>
        /// Deserializees a serialized string
        /// </summary>
        /// <param name="serializedDataString">A Serialize string in form of a byte[] array</param>
        /// <returns>A deserialized string</returns>
        public string ConvertByteArrayToData(byte[] serializedDataString)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            // Deserialize the dataString and returns is to a string
            ms.Write(serializedDataString, 0, serializedDataString.Length);         // Insert 'serializedDataString' into the memoryStream
            ms.Seek(0, SeekOrigin.Begin);                                           // sets memoryStream start point to '0'
            object obj = (object)bf.Deserialize(ms);                                // Deserialize the memoryStream

            return obj.ToString();                                                  // returns the Deserialized memoryStream as a string
        }

        #endregion
    }
}
