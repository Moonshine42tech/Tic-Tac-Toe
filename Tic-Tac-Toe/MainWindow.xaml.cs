using System.Windows;
using System;


namespace Tic_Tac_Toe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Starts up the Singelplayer application window
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void Button_Singelplayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region manipulate windows

                // Opens a new instance of the singelplayer window
                Game_SingelPlayer singelPlayerModeWindow = new Game_SingelPlayer();
                singelPlayerModeWindow.Show();

                // Hides the current main window
                App.Current.MainWindow.Close();
                #endregion
            }
            catch (Exception)
            {
            }
        }

        /// <summary>
        /// Starts up the Multiplayer application window
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void Button_Multiplayer_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                #region manipulate windows

                // Opens a new instance of the singelplayer window
                Game_Multiplayer multiPlayerModeWindow = new Game_Multiplayer();
                multiPlayerModeWindow.Show();

                // Hides the current main window
                App.Current.MainWindow.Close();

                #endregion
            }
            catch (Exception)
            {

            }
        }
    }
}
