using System;
using System.Collections.Generic;
using System.Text;
using TTT_Models;

namespace TTT_Repository
{
    public class RunGame
    {
        #region Private Members
        public RunGame()
        {

        }

        #endregion 

        public void RunSinglePlayerGame(int amountOfGames, string playerDisplayName)
        {
            Game g = new Game();                                                        // Creates a new instance of a game
            g.AmountOfGames = amountOfGames;
            g.Player1.DisplayName = playerDisplayName;
            g.Player2.DisplayName = playerDisplayName;

            // Creates a new array and Sets all filds on the gameboard to free
            ClearAllFildsOnGameboard(g.gameboardFilds = new GameSymbolTypes[9]);           


            
        }

        public void RunMultiPlayerGame(int amountOfGames)
        {
            
        }

        /// <summary>
        /// Explisitly sets alle filds to free
        /// </summary>
        /// <param name="symbols">enum array of the type GameSymbolTypes</param>
        public void ClearAllFildsOnGameboard(GameSymbolTypes[] symbols)
        {
            // Explisitly sets alle filds to free
            for (int i = 0; i<gameboardFilds.Length; i++)
            {
                gameboardFilds[i] = GameSymbolTypes.Free;
            }
}


    }
}