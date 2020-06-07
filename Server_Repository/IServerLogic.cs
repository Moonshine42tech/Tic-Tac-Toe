using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Net;
using System.Text;
using Server_Models;

namespace Server_Repository
{
    public interface IServerLogic
    {

        #region START / STOP SERVER

        /// <summary>
        /// Makes a socket to lisen on a given ip addres and port number.
        /// </summary>
        /// <param name="ipAddress">Ip Address</param>
        /// <param name="portNumber">Port Number</param>
        /// <returns>A socket bound to an Ip address and port number</returns>
        /// <exception cref="NullReferenceException">Returns a default connection to localhost on port 8888</exception>
        Socket StartLiseningForConnections(string ipAddress, int portNumber);


        /// <summary>
        /// Stops the Server socket lisener
        /// </summary>
        /// <param name="master">Socket</param>
        void StopServerConnection(Socket master);

        #endregion


        #region Client Logic

        /// <summary>
        /// Makes a new thread for every new client connection
        /// </summary>
        /// <param name="socketListenner">A pre bound System.Net.Sockets web socket</param>
        void NewClientThread(Socket socketListenner);



        /// <summary>
        /// Uses the first index in the data from the client to Call methods
        /// </summary>
        /// <param name="clientSocket">A System.Net.Sockets Socket</param>
        /// <param name="resivedData">Data send from the client</param>
        void ClientMethodCalls(Socket clientSocket, byte[] resivedData);


        /// <summary>
        /// Returns a list of all available clients
        /// </summary>
        /// <param name="clientSocket">A System.Net.Sockets Socket</param>
        void ReturnClientList(Socket clientSocket);

        #endregion


        #region Multoplayer only logic 

        /// <summary>
        /// Serializes Game data into a byte[] array
        /// </summary>
        /// <param name="DisplayName">String from UI</param>
        /// <param name="hasGameEnded">TTT_Models.GameModel</param>
        /// <param name="isPlayer1Turn">TTT_Models.GameModel</param>
        /// <param name="gameboard">TTT_Models.GameModel</param>
        /// <returns>byte[] Array</returns>
        byte[] ConvertDataToByteArray(string DisplayName, bool hasGameEnded, bool isPlayer1Turn, GameSymbolTypes[] gameboard);

        /// <summary>
        /// Serializes Game data into a byte[] array
        /// </summary>
        /// <param name="aBoolValue">bool</param>
        /// <returns>byte[] Array</returns>
        byte[] ConvertDataToByteArray(bool aBoolValue);


        /// <summary>
        /// Deserializees a serialized string
        /// </summary>
        /// <param name="serializedDataString">A Serialize string in form of a byte[] array</param>
        /// <returns>A deserialized string</returns>
        string ConvertByteArrayToData(byte[] serializedDataString);

        #endregion
    }
}
