using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Singelplayer_Click(object sender, RoutedEventArgs e)
        {
            #region manipulate windows

            // Hides the current main window
            App.Current.MainWindow.Hide();

            // Opens a new instance of the singelplayer window
            Game_SingelPlayer singelPlayerModeWindow = new Game_SingelPlayer();
            singelPlayerModeWindow.Show();

            #endregion
        }

        private void Button_Multiplayer_Click(object sender, RoutedEventArgs e)
        {
            #region manipulate windows

            // Hides the current main window
            App.Current.MainWindow.Hide();

            // Opens a new instance of the singelplayer window
            Game_Multiplayer multiPlayerModeWindow = new Game_Multiplayer();
            multiPlayerModeWindow.Show();

            #endregion
        }
    }
}
