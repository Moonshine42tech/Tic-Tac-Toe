using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TTT_Models;
using TTT_Repository;


namespace TicTacToeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        

        #region Default constructor
        /// <summary>
        /// Default constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            Scoreboard_DisplayNames.IsEnabled = false;
            Gameboard.IsEnabled = false;
            Button_GiveUp.IsEnabled = false; 
        }
        #endregion

        private void Button_NewGameOrGameRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region Shorten names from UI for a more easy to read code
                // Shortend down for easy to read code futher down.
                bool? sigelplayer = Radiobutton_Singleplayer.IsChecked;
                
                #endregion

                #region Sets the value of the amount of games selected
                int amountOfGames;

                if (Radiobutton_SingleGame.IsChecked == true)
                {
                    amountOfGames = 1;            // singel game
                }
                else
{
                    amountOfGames = 3;            // bedst out of three
                }

                #endregion

                // If a game mode is selected then continue
                if (sigelplayer == true || sigelplayer == false)
                {
                    #region Enable / Disaple Filds

                    // Enabels the game fild
                    Scoreboard_DisplayNames.IsEnabled = true;
                    Gameboard.IsEnabled = true;
                    Button_GiveUp.IsEnabled = true;

                    // Disabels the settings filds
                    Settings_Space.IsEnabled = false;

                    #endregion

                    if (sigelplayer == true)
                    {
                        #region Set displayNames

                        Player1_DisplayName.Content = DisplayName_User.Text;
                        Player2_DisplayName.Content = DisplayName_User.Text;

                        #endregion

                        // call singelplayer game
                        ScoreBoard scoreboard = new ScoreBoard();
                        NewSingelPlayerGame(scoreboard, amountOfGames);
                    }
                    else
                    {
                        // Call multiplayer game
                    }
                }
                else
                {

                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// The ruels of the game goes here.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button_ReadTheRuelsHere_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World"); 
        }

        /// <summary>
        /// The player pressing this button will give up, and loses the game
        /// </summary>
        /// <param name="sender">a button element</param>
        /// <param name="e"></param>
        private void Button_GiveUp_Click(object sender, RoutedEventArgs e)
        {
            // Send a bool (false) to repository to end the current game
        }

        private void Button_OnGameFild_Click(object sender, RoutedEventArgs e)
        {

            var button = ((Button)sender);                  // the pressed button

            // finds button position
            var colum = Grid.GetColumn(button);
            var row = Grid.GetRow(button);

            var buttonIndex = colum + (row *  3);           // calculates the pressed buttons grid position

            if (true)
            {

            }

        }


        public void NewSingelPlayerGame(ScoreBoard scoreboard, int amountOfGames)
        {
            TTT_Models.GameModel gameModel = new TTT_Models.GameModel();
            TTT_Repository.GameLogic game = new TTT_Repository.GameLogic();

            game.PrepareTheGameboard(gameModel.gameboardFildsArray, true);      // set up a empty gameboard 



        }
    }
}
