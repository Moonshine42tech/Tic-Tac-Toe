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
    }
}
