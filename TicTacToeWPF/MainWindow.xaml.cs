using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TTT_Repository;


namespace TicTacToeWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        RunGame newGame = new RunGame();

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
                var sigelplayer = Radiobutton_Singleplayer.IsChecked;
                var oneGame = Radiobutton_SingleGame.IsChecked;
                int amountOfGames;

                #endregion

                // If a game mode is selected then continue
                if (sigelplayer == true || sigelplayer == false)
                {
                    #region Sets the value of the amount of games selected
                    // Sets amount of games 
                    if (oneGame == true)
                    {
                        amountOfGames = 1;
                    }
                    else
                    {
                        // oneGame == false
                        amountOfGames = 3;
                    }
                    #endregion

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
                        // call singelplayer game

                        //newGame.PrepareTheGameboard(true);

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

        private void button_ReadTheRuelsHere_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Hello World");
        }

        private void Button_GiveUp_Click(object sender, RoutedEventArgs e)
        {
            // Send a bool (false) to repository to end the current game
        }

        private void Button_OnGameFild_Click(object sender, RoutedEventArgs e)
        {
            int gameboardFild = Convert.ToInt32(((Button)sender).Tag);
            // newGame.CheckGameboardFildStatus(gameboardFild);

            // Send 
            // Update Array List 

        }
    }
}
