using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace TTT_Models
{
    public class MultiplayerGameModel
    {
        public string IpAddress { get; set; }                                                   // Used for lokating the server
        public int PortNumber { get; set; }                                                     // Used for lokating the server

        public string DisplayName { get; set; }
        public bool HasGameEnded { get; set; } = false;                                         // GameStatus boolian - false = game has NOT ended | true = game has ended
        public bool IsPlayer1Turn { get; set; } = true;                                         // Used to evaluate what players turn it is. (true meant player1 always starts the game).

        public ScoreBoardScore Score { get; set; }
        public GameSymbolTypes[] GameboardFildsArray { get; set; } = new GameSymbolTypes[9];     // An llist of Enum values. thise values is the three different states a fild on the gameboard can have

        public Socket ClientSocket { get; set; }                                                // Used for sending and resiving data to and from the server

        public int RequestedMethod { get; set; }                                                // Used in a switch case on the server to select what method should be used
    }
}
