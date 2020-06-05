using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using TTT_Models;
using TTT_Repository;


namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for Game_SingelPlayer.xaml
    /// </summary>
    public partial class Game_SingelPlayer : Window
    {
        GameLogic gameLogic = new GameLogic();          // Reusable logic methods for the game Tic-Tac-Toe
        GameModel game = new GameModel();               // A model that contains everything a game needs.

        #region Default constructor
        public Game_SingelPlayer()
        {
            InitializeComponent();

            New_Game();
        }
        #endregion


        /// <summary>
        /// Setting up a new Tic-Tac-Toe game
        /// </summary>
        private void New_Game()
        {
            game = new GameModel();                                                             // new instance of my GameModel

            gameLogic.PrepareTheGameboardSymbols(game.gameboardFildsArray, 9, true);            // Creates a new instance of GameSymbolTypes[9] and Explicitly sets all GameSymbolTypes to 'Free'.

            gameLogic.SetAllButtonsContent(Gameboard.Children.Cast<Button>().ToList());         // Clears all 'Content' and changes background color on all buttons inside 'Gameboard'.

            game.hasGameEnded = false;                                                          // resets the gamestatus
        }


        #region Application buttons

        /// <summary>
        /// Sets the value of the button there was chicked
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void Button_OnGameFild_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Gets the sender button
                Button button = (Button)sender;

                // Find the button position in the array
                var colum = Grid.GetColumn(button);
                var row = Grid.GetRow(button);

                var indexOfButton = colum + (row * 3);

                // Checks if the button is 'Free'
                bool buttonCheck = gameLogic.CheckGameboardFildStatus(game.gameboardFildsArray, indexOfButton);
                
                if (buttonCheck == false)            // false means the button is NOT 'Free'
                {
                    return;
                }

                // Checks what players turn it is and sets a symbol. derafter the players turn changes.
                gameLogic.SetAGameboardFildValue(game.gameboardFildsArray, indexOfButton, game.isPlayer1Turn);

                // Sets the context of the pressed button depending on what players turn it is.
                game.isPlayer1Turn = gameLogic.SetButtonSymbol(button, game.isPlayer1Turn);

                // Checks if the game has ended or not
                game.hasGameEnded = gameLogic.CheckifGameHasEnded(game.gameboardFildsArray, Gameboard.Children.Cast<Button>().ToList(), game.hasGameEnded);

                // Switches between what player name should displayed be on the UI
                if (game.isPlayer1Turn == true)
                    turnOfPlayerX.Content = "Player 1";         // writes to a label on the UI. 

                else
                    turnOfPlayerX.Content = "Player 2";         // writes to a label on the UI. 
            }
            catch (Exception)
            {
                
            }
        }


        /// <summary>
        /// Starts a new Singelplayer game of Tic-Tac-Toe
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void PlayANewGame_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (game.hasGameEnded == true)
                {
                    New_Game();
                }
                else
                {
                    gameLogic.GenericMessageBoxPopup("You Lose");       // Costum messagebox method
                    New_Game();
                }
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// Closes the Application Down, using a costum button
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void Close_Application_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                App.Current.Shutdown();
            }
            catch (Exception)
            {

            }
        }

        #endregion


        /// <summary>
        /// Makes the whole application able to be draged or moved, 
        /// This is done by holding down the left mouse button, anyware on the application. 
        /// </summary>
        /// <param name="sender">Application Window</param>
        /// <param name="e"></param>
        private void SingelplayerWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // This method was possible thanks to 'Rachel' from StackOwerflow 'https://stackoverflow.com/questions/7417739/make-wpf-window-draggable-no-matter-what-element-is-clicked'

            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

    }
}
