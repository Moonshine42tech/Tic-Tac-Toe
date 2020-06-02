using System;
using System.Collections.Generic;
using System.Text;
using TTT_Models;

namespace TTT_Repository
{
    public class Game
    {
        //public bool CheckIfTheGameHasEnded()
        //{
        //    if (true)
        //    {

        //    }
        //}

        /// <summary>
        /// Changes the turn between the players using a bool 'player1Turn'
        /// </summary>
        /// <param name="player1Turn">true = Player1 | false = Player2</param>
        /// <returns> the next players turn in form of a bool</returns>
        public bool TurnChange(bool player1Turn)
        {
            try
            {
                if (player1Turn == true) 
                {
                    player1Turn = false;
                    return player1Turn;
                }
                else 
                {
                    player1Turn = true;
                    return player1Turn;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }



        /// <summary>
        /// Checks if a specific fild on the gameboard is free or not
        /// </summary>
        /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        /// <param name="gameboardFildIndex">The selected/clicked idex of the array</param>
        /// <returns></returns>
        public bool CheckGameboardFildStatus(GameSymbolTypes[] gameboardFildsArray, int gameboardFildIndex)
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
        /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        /// <param name="gameboardFildIndex">The selected/clicked idex of the array</param>
        /// <param name="player1Turn">whhos turn is it</param>
        public void SetAGameboardFildValue(GameSymbolTypes[] gameboardFildsArray, int gameboardFildIndex, bool player1Turn)
        {
            if (player1Turn == true)
            {
                // PlayerModel 1 is always X
                gameboardFildsArray[gameboardFildIndex] = GameSymbolTypes.Cross;
            }
            else
            {
                // PlayerModel 2 is always O
                gameboardFildsArray[gameboardFildIndex] = GameSymbolTypes.Cirkle;
            }
        }

        /// <summary>
        /// Explisitly sets alle filds to free. 
        /// Used for clearing the gameboard between or after game(s).
        /// </summary>
        /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        public void ClearAllFildsOnGameboard(GameSymbolTypes[] gameboardFildsArray)
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
        /// /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        /// <param name="newBoeardOrClearBoard"> true = new gameboard | false = clear gameboard)</param>
        public void PrepareTheGameboard(GameSymbolTypes[] gameboardFildsArray, bool newBoeardOrClearBoard)
        {
            try
            {
                if (newBoeardOrClearBoard == true)
                {
                    gameboardFildsArray = new GameSymbolTypes[9];                       // creates a new black array with free filds
                    ClearAllFildsOnGameboard(gameboardFildsArray);                      // Explisitly resets the array to start a new game
                }
                else
                {
                    ClearAllFildsOnGameboard(gameboardFildsArray);                      // Explisitly resets the array to start a new game
                }
            }
            catch (Exception)
            {

            }
        }
    }
}