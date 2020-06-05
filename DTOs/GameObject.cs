using System;
using System.Collections.Generic;
using System.Text;
using TTT_Models;

namespace DTOs
{
    [Serializable]
    public class GameObject
    {
        public string DisplayName { get; set; }                         // In-game Name
        public bool hasGameEnded { get; set; } = false;                 // Game Status
        public bool isPlayer1Turn { get; set; } = true;                 // Player Turn

        public int X_Score { get; set; }                                // amount of wins for X 
        public int O_Score { get; set; }                                // amount of wins for O

        public GameSymbolTypes[] GameboardFildsArray { get; set; }      // GameBoeard

    }
}
