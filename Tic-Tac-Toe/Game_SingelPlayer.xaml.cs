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
            game = new GameModel();                                                   // new instance of my GameModel

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
            var button = (Button)sender;


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
            App.Current.Shutdown();
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
