using System;
using System.Collections.Generic;
using System.Text;
using TTT_Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Documents;

namespace TTT_Repository
{
    public class GameLogic
    {

        /// <summary>
        /// Changes the turn between the players using a bool 'player1Turn'
        /// </summary>
        /// <param name="isPlayer1Turn">true = Player1 | false = Player2</param>
        /// <returns> the next players turn in form of a bool</returns>
        public bool TurnChange(bool isPlayer1Turn)
        {
            try
            {
                if (isPlayer1Turn == true) 
                {
                    isPlayer1Turn = false;
                    return isPlayer1Turn;
                }
                else 
                {
                    isPlayer1Turn = true;
                    return isPlayer1Turn;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }


        #region UI Bottons 

        /// <summary>
        /// Manipulates a given list of WPF buttons
        /// </summary>
        /// <param name="buttons">A list of buttons using the 'PresentationFramework.dll' and 'PresentationCore.dll' Assemblies from WPF</param>
        public void SetAllButtonsContent(List<System.Windows.Controls.Button> buttons)
        {
            try
            {
                foreach (var button in buttons)
                {
                    // Changes the content of alle buttons
                    button.Content = "";

                    // Changes color on both forground and background
                    button.Background = Brushes.White;
                    button.Foreground = Brushes.Blue;
                }
            }
            catch (NullReferenceException e)
            {

            }
            catch (Exception e)
            {

            }

        }


        /// <summary>
        /// Sets the symbol value, dependig on what players turn it is.
        /// </summary>
        /// <param name="button">A clicked button from the UI</param>
        /// <param name="isPlayer1Turn">a players turn</param>
        public void SetButtonSymbol(System.Windows.Controls.Button button, bool isPlayer1Turn)
        {
            try
            {
                button.Content = isPlayer1Turn ? "X" : "O";
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion


        #region Checking Symbols

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
        public void SetAGameboardFildValue(GameSymbolTypes[] gameboardFildsArray, int gameboardFildIndex, bool isPlayer1Turn)
        {
            if (isPlayer1Turn == true)
            {
                // Player 1 is always X
                gameboardFildsArray[gameboardFildIndex] = GameSymbolTypes.Cross;

                TurnChange(isPlayer1Turn);    // Changes the players turn
            }
            else
            {
                // Player 2 is always O
                gameboardFildsArray[gameboardFildIndex] = GameSymbolTypes.Cirkle;

                TurnChange(isPlayer1Turn);    // Changes the players turn
            }
        }

        #endregion


        #region Prepare symbols

        /// <summary>
        /// Explisitly sets alle filds to free. 
        /// Used for clearing the gameboard between or after game(s).
        /// </summary>
        /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        public void SetAllSymbolTypesToFree(GameSymbolTypes[] gameboardFildsArray)
        {
            try
            {
                // Explisitly sets alle filds to free
                for (int i = 0; i < gameboardFildsArray.Length; i++)
                {
                    gameboardFildsArray[i] = GameSymbolTypes.Free;
                }
            }
            catch (NullReferenceException e)
            {

            }
            catch (Exception e)
            {

            }

        }

        /// <summary>
        /// Chouse if you will make a new gameboard or just clear the gameboard
        /// </summary>
        /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        /// <param name="amountOfFilds">Amount of buttons/filds on the gameboard</param>
        /// <param name="newBoeardOrClearBoard"> true = new gameboard | false = clear gameboard)</param>
        public void PrepareTheGameboardSymbols(GameSymbolTypes[] gameboardFildsArray, int amountOfFilds, bool newBoeardOrClearBoard)
        {
            try
            {
                if (newBoeardOrClearBoard == true)
                {
                    gameboardFildsArray = new GameSymbolTypes[amountOfFilds];           // Creates a new object of GameSymbolTypes with default value 'free'
                    SetAllSymbolTypesToFree(gameboardFildsArray);                       // Explisitly set all the fild sympols to 'Free'

                }
                else
                {
                    SetAllSymbolTypesToFree(gameboardFildsArray);                       // Explisitly set all the sympols to 'Free'

                }
            }
            catch (NullReferenceException e)
            {

            }
            catch (Exception e)
            {

            }
        }

        #endregion


        #region MessageBox(s)

        /// <summary>
        /// Makes a costum error message popup on the windows desktop
        /// </summary>
        /// <param name="msg">Costum error message</param>
        public void ErrorMessageBoxPopup(string msg)
        {
            MessageBox.Show("Error: " + msg);
        }

        /// <summary>
        /// Makes a costum generic MessageBox
        /// </summary>
        /// <param name="msg">costum string message</param>
        public void GenericMessageBoxPopup(string msg)
        {
            MessageBox.Show(msg);
        }

        #endregion
    }
}