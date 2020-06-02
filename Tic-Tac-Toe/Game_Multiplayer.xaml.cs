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
    }
}
