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
using Server_Models;
using Server_Repository;

namespace WebSocketServer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LisenOn lisenOn = new LisenOn();
        ServerLogic serverLogic = new ServerLogic();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void StopListeningOnIpAndPort_Click(object sender, RoutedEventArgs e)
        {

        }


        private void StartListeningOnIpAndPort_Click(object sender, RoutedEventArgs e)
        {

        }


        /// <summary>
        /// Sets the given port number to lisenOn.PortNumber
        /// </summary>
        /// <param name="sender">Textbox fild value</param>
        /// <param name="e"></param>
        private void ListenOnPortNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Sets the given port number to lisenOn.PortNumber
            lisenOn.PortNumber = Convert.ToInt32(ListenOnPortNumber.Text);
        }


        /// <summary>
        /// Sets the given Ip address to lisenOn.IpAddress
        /// </summary>
        /// <param name="sender">Textbox fild value</param>
        /// <param name="e"></param>
        private void ListenOnIPAddress_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Sets the given Ip address to lisenOn.IpAddress
            lisenOn.IpAddress = ListenOnIPAddress.Text;
        }
    }
}
