using System;
using System.Collections.Generic;
using System.Text;

namespace TTT_Models
{
    public class Game
    {
        public Player Player1 { get; set; }
        public Player Player2 { get; set; }

        public int AmountOfGames { get; set; }
        //public bool SingleOrMultiPlayer { get; set; }       // What type of game is selected           
        
        public bool Player1Turn { get; set; } = true;       // Used to evaluate what players turn it is.
        public bool isGameEnded { get; set; }               // GameStatus boolian
        public GameSymbolTypes[] gameboardFildsArray { get; set; }


        /// <summary>
        /// Checks if a specific fild on the gameboard is free or not
        /// </summary>
        /// <param name="gameboardFildIndex">The selected/clicked idex of the array</param>
        /// <returns></returns>
        public bool CheckGameboardFildStatus(int gameboardFildIndex)
        {
            if (gameboardFildsArray[gameboardFildIndex] != GameSymbolTypes.Free)
            {
                return false;           // if the gameboard file is NOT 'free' return false
            }
            else
            {
                return true;            // if the gameboard file is 'free' return true
            }
        }

        /// <summary>
        /// Simply sets a value on the gameboard
        /// </summary>
        /// <param name="gameboardFildIndex">The selected/clicked idex of the array</param>
        /// <param name="player1Turn">whhos turn is it</param>
        public void SetAGameboardFildValue(int gameboardFildIndex, bool player1Turn)
        {
            if (player1Turn == true)
            {
                // Player 1 is always X
                gameboardFildsArray[gameboardFildIndex] = GameSymbolTypes.Cross;
            }
            else
            {
                // Player 2 is always O
                gameboardFildsArray[gameboardFildIndex] = GameSymbolTypes.Cirkle;
            }
        }

        /// <summary>
        /// Explisitly sets alle filds to free. 
        /// Used for clearing the gameboard between or after game(s).
        /// </summary>
        public void ClearAllFildsOnGameboard()
        {
            // Explisitly sets alle filds to free
            for (int i = 0; i < gameboardFildsArray.Length; i++)
            {
                gameboardFildsArray[i] = GameSymbolTypes.Free;
            }
        }

        /// <summary>
        /// Chouse if you will make a new gameboard or just clear the gameboard
        /// </summary>
        /// <param name="newBoeardOrClearBoard"> true = new gameboard | false = clear gameboard)</param>
        public void PrepareTheGameboard(bool newBoeardOrClearBoard)
        {
            try
            {
                if (newBoeardOrClearBoard == true)
                {
                    gameboardFildsArray = new GameSymbolTypes[9];                   // creates a new black array with free filds
                    ClearAllFildsOnGameboard();                                     // Explisitly resets the array to start a new game
                }
                else
                {
                    ClearAllFildsOnGameboard();                                     // Explisitly resets the array to start a new game
                }
            }
            catch (Exception)
            {

            }
        }

        //public bool CheckIfTheGameHasEnded()
        //{
        //    if (true)
        //    {

        //    }
        //}

        /// <summary>
        /// Changes the turn between the players using a bool 'Player1Turn'
        /// </summary>
        public void TurnChange()
        {
            try
            {
                if (Player1Turn == true)
                {
                    Player1Turn = false;
                }
                else
                {
                    Player1Turn = true;
                }
            }
            catch (Exception e)
            {

            }
        }
    }
}
