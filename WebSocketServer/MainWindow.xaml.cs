using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Net.Sockets;
using System.Net;
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
        private static Socket masterSocket;

        public MainWindow()
        {
            InitializeComponent();
            StopListeningOnIpAndPort.IsEnabled = false;
        }

        #region START / STOP SERVER

        /// <summary>
        /// Stops the server socket
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        private void StopListeningOnIpAndPort_Click(object sender, RoutedEventArgs e)
        {
            // Makes sure you can't see the list of clients if the Server is not connectet to an IPaddress and Port
            foreach (var client in serverLogic.AllClients)
            {
                ConnectionList_ListBox.ItemsSource = "";
            }

            // Make A stop server method
            serverLogic.StopServerConnection(masterSocket);
        }


        /// <summary>
        /// Starts the server.
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        private void StartListeningOnIpAndPort_Click(object sender, RoutedEventArgs e)
        {
            StopListeningOnIpAndPort.IsEnabled = true;

            try
            {
                // Adds all DisplayNames in the 'AllClients' list to the the UI listbox 'ConnectionList_ListBox'
                foreach (var client in serverLogic.AllClients)
                {
                    ConnectionList_ListBox.ItemsSource = client.ClientDisplayName;
                }

                // Makes a web socket that can be lisend on
                Socket liseningSocket = serverLogic.StartLiseningForConnections(ListenOnIPAddress.Text, Convert.ToInt32(ListenOnPortNumber.Text));

                masterSocket = liseningSocket;
            }
            catch (Exception)
            {

            }
        }

        #endregion


        #region SERVER SETTINGS

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

        #endregion 

    }
}
