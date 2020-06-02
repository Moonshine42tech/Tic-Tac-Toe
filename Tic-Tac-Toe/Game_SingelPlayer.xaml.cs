using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;


namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for Game_SingelPlayer.xaml
    /// </summary>
    public partial class Game_SingelPlayer : Window
    {
        public Game_SingelPlayer()
        {
            InitializeComponent();


        }

        private void Button_OnGameFild_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Close_Application_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }

        private void New_Game_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SingelplayerWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }
    }
}
