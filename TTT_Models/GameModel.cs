using System;
using System.Collections.Generic;
using System.Text;

namespace TTT_Models
{
    public class GameModel
    {
        public PlayerModel Player1 { get; set; }
        public PlayerModel Player2 { get; set; }

        public int AmountOfGames { get; set; }         
        
        public bool Player1Turn { get; set; } = true;       // Used to evaluate what players turn it is. (true meant player1 always starts the game).
        public bool isGameEnded { get; set; } = false;      // GameStatus boolian

        public GameSymbolTypes[] gameboardFildsArray { get; set; }

    }
}
