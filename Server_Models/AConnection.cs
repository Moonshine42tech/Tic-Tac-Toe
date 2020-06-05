using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Server_Models
{
    public class AConnection
    {
        public string ClientId { get; set; }            // Identefiet who te user is.
        public Thread ClientThread { get; set; }        // Contains the Thread the user is connected to

        public bool InGameStatus { get; set; }          // true = in-Game | false = Free to play

        public string ClientDisplayName { get; set; }   // A displayname used to help the player to know who is who on a list.
    }
}
