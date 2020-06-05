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
        /// Tells a given web socket to lisen on a specific ip addres and port.
        /// </summary>
        /// <param name="socketListenner">a System.Net.Sockets socket </param>
        /// <param name="ipAddress">Ip Address</param>
        /// <param name="portNumber">Port Number</param>
        /// <returns>A socket bound to an Ip address and port number</returns>
        Socket StartLiseningForConnections(Socket socketListenner, string ipAddress, int portNumber);

        /// <summary>
        /// Makes a new thread for every new client connection
        /// </summary>
        /// <param name="socketListenner">A pro bound System.Net.Sockets web socket</param>
        void NewClientThread(Socket socketListenner);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="clientSocket"></param>
        void ClientConnection(Socket clientSocket);
    }
}
