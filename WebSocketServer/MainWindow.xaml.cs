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
using System.Threading;

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
            StopListeningOnIpAndPort.IsEnabled = false;
        }
        

        #region START / STOP SERVER

        /// <summary>
        /// Stops the server socket
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        /// <exception cref="NullReferenceException">Sets ListBox.ItemsSource to ""</exception>
        private void StopListeningOnIpAndPort_Click(object sender, RoutedEventArgs e)
        {
            StopListeningOnIpAndPort.IsEnabled = false;                     // Alowes you to stop the server
            StartListeningOnIpAndPort.IsEnabled = true;                     // you can't click start if the server is running already
            ServerStatus.Content = "Not Running";                           // Label on the UI

            try
            {
                //Displays a list of all clients
                if (serverLogic.AllClients != null)
                {
                    // Makes sure you can't see the list of clients if the Server is not connectet to an IPaddress and Port
                    foreach (var client in serverLogic.AllClients)
                    {
                        ConnectionList_ListBox.ItemsSource = "";
                    }
                }
                else
                {
                    // dont show anything on the listBox if the client list id null or empty
                }


                // Make A stop server method
                serverLogic.StopServerConnection();
            }
            catch (NullReferenceException)
            {
                ConnectionList_ListBox.ItemsSource = "";
            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// Starts the server.
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        private void StartListeningOnIpAndPort_Click(object sender, RoutedEventArgs e)
        {
            StopListeningOnIpAndPort.IsEnabled = true;                      // Alowes you to stop the server
            StartListeningOnIpAndPort.IsEnabled = false;                    // you can't click start if the server is running already
            ServerStatus.Content = "Server is Running";                     // Label on the UI

            try
            {
                //Displays a list of all clients
                if (serverLogic.AllClients != null)
                {
                    // Adds all DisplayNames in the 'AllClients' list to the the UI listbox 'ConnectionList_ListBox'
                    foreach (var client in serverLogic.AllClients)
                    {
                        ConnectionList_ListBox.Items.Add(client.ClientDisplayName);
                    }
                }
                else
                {
                    // dont show anything on the listBox if the client list id null or empty
                }

                if (!string.IsNullOrEmpty(ListenOnPortNumber.Text) || !string.IsNullOrEmpty(ListenOnIPAddress.Text))
                {
                    string ip = ListenOnIPAddress.Text;
                    int port = Convert.ToInt32(ListenOnPortNumber.Text);

                    // Calles a method on a new thread
                    Thread newClientThread = new Thread(() => serverLogic.StartLiseningForConnections(ip, port));        
                    newClientThread.Start();
                }
                else
                {
                    // Do nothing
                }
            }
            catch (NullReferenceException)
            {
                ConnectionList_ListBox.ItemsSource = "";
            }
            catch (Exception x)
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
