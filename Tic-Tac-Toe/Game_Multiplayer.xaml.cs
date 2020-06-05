using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;


namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for Game_Multiplayer.xaml
    /// </summary>
    public partial class Game_Multiplayer : Window
    {
        public Game_Multiplayer()
        {
            InitializeComponent();
        }

        private void Button_OnGameFild_Click(object sender, RoutedEventArgs e)
        {

        }

        private void HostetOnlineGames_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Close_Application_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void MultiplayerWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void ServerPortNumber_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void ServerIpAddress_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        /// <summary>
        /// Gives a pop-up window with a guide
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        private void HowToConnectAndPlay_Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("" +
                "1. type in the ip address and port number of the server \n   you want to play on.\n\n" + 
                "2. Make a Display name.\n\n" +
                "3. Press the 'Connect' button.\n\n" +
                "4. Select an oponent from the list of available opponents.\n\n" +
                "5. Press the 'Play' button to start the game.\n\n" +
                "6. When you want to leve the game, simply press the button 'Close'.");

        }
    }
}
