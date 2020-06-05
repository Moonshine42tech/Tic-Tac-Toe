using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text;

namespace Server_Repository
{
    public interface IServerLogic
    {
        /// <summary>
        /// Makes a socket to lisen on a given ip addres and port number.
        /// </summary>
        /// <param name="ipAddress">Ip Address</param>
        /// <param name="portNumber">Port Number</param>
        /// <returns>A socket bound to an Ip address and port number</returns>
        /// <exception cref="NullReferenceException">Returns a default connection to localhost on port 8888</exception>
        Socket StartLiseningForConnections(string ipAddress, int portNumber);

        /// <summary>
        /// Makes a new thread for every new client connection
        /// </summary>
        /// <param name="socketListenner">A pro bound System.Net.Sockets web socket</param>
        void NewClientThread(Socket socketListenner);

        /// <summary>
        /// Send the client back, a sorted list of all available Clients in form of a big string. 
        /// </summary>
        /// <param name="clientSocket">A pre bound System.Net.Sockets web socket</param>
        void ClientReturnList(Socket clientSocket);
    }
}
