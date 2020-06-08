using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;

namespace Server_Models
{
    public class AClientConnection
    {
        public string ClientId { get; set; }                        // Identefiet who te user is.
        public string ClientDisplayName { get; set; }               // A displayname used to help the player to know who is who on a list.

        public bool HasGameEnded { get; set; }
        public bool InGameStatus { get; set; }                      // true = in-Game | false = Free to play
        public bool IsPlayer1Turn { get; set; }                     // used for knowing whos tyrn is is

        public GameSymbolTypes[] GameboardFildsArray { get; set; } = new GameSymbolTypes[9];   // gameboard

        public Socket ClientSocket { get; set; }                    // My connection 

        public AClientConnection Oponent { get; set; }              // Opponent reference
    }
}
