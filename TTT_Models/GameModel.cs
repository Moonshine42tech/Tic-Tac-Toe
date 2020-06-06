using System;
using System.Collections.Generic;
using System.Text;

namespace TTT_Models
{
    public class GameModel
    {
        public bool HasGameEnded { get; set; } = false;                 // GameStatus boolian - false = game has NOT ended | true = game has ended
        public bool IsPlayer1Turn { get; set; } = true;                   // Used to evaluate what players turn it is. (true meant player1 always starts the game).

        public GameSymbolTypes[] GameboardFildsArray { get; set; } = new GameSymbolTypes[9];    // An llist of Enum values. thise values is the three different states a fild on the gameboard can have

    }
}
