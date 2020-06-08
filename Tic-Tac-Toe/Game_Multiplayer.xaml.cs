using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using TTT_Repository;
using TTT_Models;
using System.Net.Sockets;
using System.Net;
using System.Linq;
using System.Threading;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for Game_Multiplayer.xaml
    /// </summary>
    public partial class Game_Multiplayer : Window
    {
        MultiplayerGameModel mpGame = new MultiplayerGameModel();
        GameLogic gameLogic = new GameLogic();
        AvailableOpponent selectedAvailableOpponent = new AvailableOpponent();

        private readonly object objLock = new object();             // Used to by a lock statment

        public Game_Multiplayer()
        {
            InitializeComponent();
            Gameboard.IsEnabled = false;                // Disabels the game fild on startup.
            Play_Button.IsEnabled = false;              // Disabels the play button on startup -< enabels after connection to server has been made
            New_Game();
        }

        #region game

        /// <summary>
        /// Setting up a new Tic-Tac-Toe game board
        /// </summary>
        private void New_Game()
        {
            PlayerDisplayName_textbox.IsEnabled = true;                                         // Enabels the users DisplayName input

            mpGame = new MultiplayerGameModel();                                                // new instance of my GameModel

            gameLogic.PrepareTheGameboardSymbols(mpGame.GameboardFildsArray, 9, true);          // Creates a new instance of GameSymbolTypes[9] and Explicitly sets all GameSymbolTypes to 'Free'.

            gameLogic.SetAllButtonsContent(Gameboard.Children.Cast<Button>().ToList());         // Clears all 'Content' and changes background color on all buttons inside 'Gameboard'.

            mpGame.HasGameEnded = false;                                                        // resets the gamestatus
        }


        /// <summary>
        /// Sets the value of the button there was chicked
        /// </summary>
        /// <param name="sender">button</param>
        /// <param name="e"></param>
        private void Button_OnGameFild_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                // Gets the sender button that was clicked
                Button button = (Button)sender;

                // Find the clicked buttons position in the array
                var colum = Grid.GetColumn(button);
                var row = Grid.GetRow(button);

                var indexOfButton = colum + (row * 3);

                // Checks if the button is 'Free'
                bool buttonCheck = gameLogic.CheckGameboardFildStatus(mpGame.GameboardFildsArray, indexOfButton);

                if (buttonCheck == false)            // false means the button is NOT 'Free'
                {
                    return;
                }

                // Checks what players turn it is and sets a symbol. derafter the players turn changes.
                gameLogic.SetAGameboardFildValue(mpGame.GameboardFildsArray, indexOfButton, mpGame.IsPlayer1Turn);

                // Sets the context of the pressed button depending on what players turn it is.
                mpGame.IsPlayer1Turn = gameLogic.SetButtonSymbol(button, mpGame.IsPlayer1Turn);

                // Checks if the game has ended or not
                mpGame.HasGameEnded = gameLogic.CheckifGameHasEnded(mpGame.GameboardFildsArray, Gameboard.Children.Cast<Button>().ToList(), mpGame.HasGameEnded);


                if (mpGame.HasGameEnded == true)        // If the game has ended then notifi the server
                {

                    #region Send updatet gameboard to server

                    // Serilizes data
                    byte[] serilizedDataString1 = gameLogic.ConvertDataToByteArray(1, mpGame.HasGameEnded, mpGame.IsPlayer1Turn, mpGame.GameboardFildsArray);

                    // Send byte[] to the server
                    gameLogic.SendDataToServer(mpGame.ClientSocket, serilizedDataString1);

                    #endregion

                }
                else
                {
                    // Switches between what player name should displayed be on the UI
                    if (mpGame.IsPlayer1Turn == true)
                        TurnOfPlayerX.Content = "Player 1";         // writes to a label on the UI. 

                    else
                        TurnOfPlayerX.Content = "Player 2";         // writes to a label on the UI. 
                }


                #region Send updatet gameboard to server

                // Serilizes data
                byte[] serilizedDataString2 = gameLogic.ConvertDataToByteArray(2, mpGame.HasGameEnded, mpGame.IsPlayer1Turn, mpGame.GameboardFildsArray);

                // Send byte[] to the server
                gameLogic.SendDataToServer(mpGame.ClientSocket, serilizedDataString2);

                #endregion

            }
            catch (Exception)
            {

            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        private void Play_Button_Click(object sender, RoutedEventArgs e)
        {
            #region Disabels some game settings. when going into a game

            ConnectToServer_Button.IsEnabled = false;       // so the user don't connect to the server again while in game
            PlayerDisplayName_textbox.IsEnabled = false;    // Disabels the users DisplayName input
            ServerIpAddress_TextBox.IsEnabled = false;      // Ip address field
            ServerPortNumber_TextBox.IsEnabled = false;     // port number field

            #endregion 

            // Somthing More

            // Send request to server with OponentId
        }

        #endregion


        #region Connect to server

        /// <summary>
        ///  Sending a connection request to the server using a WebSocket
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        private void ConnectToServer_Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                mpGame.PortNumber = Convert.ToInt32(ServerPortNumber_TextBox.Text);         // Sets the port number 
                mpGame.IpAddress = ServerIpAddress_TextBox.Text;                            // Sets the Ip address

                // Send a connection request to the server
                gameLogic.ConnectToServer(mpGame.ClientSocket, mpGame.IpAddress, mpGame.PortNumber);


                // Serilize all necessary data to byte[] 
                // '1' = index of a server method;
                byte[] serilizedDataString_ToServer = gameLogic.ConvertDataToByteArray(1, mpGame.DisplayName = PlayerDisplayName_textbox.Text, mpGame.HasGameEnded, mpGame.IsPlayer1Turn, mpGame.GameboardFildsArray);


                //// Send Data to the server
                gameLogic.SendDataToServer(mpGame.ClientSocket, serilizedDataString_ToServer);


                // Enabels the Play button
                Play_Button.IsEnabled = true;


                // Lisen to the server
                Thread LisenForMessageFromServer = new Thread(LisenToServerMessage);        // Makes a lisener on a new thread
                LisenForMessageFromServer.Start();                                          // starts the new thread
            }
            catch (Exception)
            {

            }
        }

        #endregion


        #region Lisen to Server 

        /// <summary>
        /// Lisens for messages from the server
        /// </summary>
        public void LisenToServerMessage()
        {
            Socket socketLisener = mpGame.ClientSocket;

            bool killLoop = true;
            while (killLoop == true)
            {
                killLoop = true;

                #region  Reads data, send by the server

                byte[] buffer = new byte[socketLisener.SendBufferSize];                              // sets the sise of the byte array to the size of the socketClient's buffersize

                // resive data
                int readbyte = socketLisener.Receive(buffer);                                        // the whole buffer (ca. 65.000 bytes) 

                // Do stuff with data
                byte[] resivedData = new byte[readbyte];                                            // A new array with a lengt equal to the buffer right above
                Array.Copy(buffer, resivedData, readbyte);                                          // copy only the amount of readbyte from buffer[] to resiveData[].

                #endregion

                string clientDataString = gameLogic.ConvertByteArrayToData(resivedData).ToString();                 // Deserializees the data the client has send. | returns one big data string
                string[] resultData = clientDataString.Split('~');                                                  // Splits up the 'clientDataString'
                resultData = resultData.Where(x => !string.IsNullOrEmpty(x)).ToArray();                             // Removes all empty entrys

                switch (resultData[0])
                {
                    #region case 0: Resive Opponent list
                    case "0": // Resive Opponent list

                        if (resultData.Length > 1)                                                                  // checks if there is data with the request
                        {
                            List<AvailableOpponent> apList = new List<AvailableOpponent>();

                            string[] clientListString = Convert.ToString(resultData[1]).Split('#');                 // Splits up the string of clients into a string[] array of clients
                            clientListString = clientListString.Where(x => !string.IsNullOrEmpty(x)).ToArray();     // Removes all empty entrys

                            foreach (var client in clientListString)
                            {
                                string[] clientInfo = client.Split(';');                                            // Split up the string of client info
                                clientInfo = clientInfo.Where(x => !string.IsNullOrEmpty(x)).ToArray();             // Removes all empty entrys

                                // Creates a new opponent
                                AvailableOpponent newOp = new AvailableOpponent();
                                newOp.ClientServerId = clientInfo[0];
                                newOp.DisplayName = clientInfo[1];

                                apList.Add(newOp);                                                                  // Adds the new opponent to the list of AvailableOpponent 'ap'
                            }


                            // sets the list of oponents equal to the UI listBox
                            //Creator: markmnl
                            //Link: https://stackoverflow.com/a/4191440
                            foreach (var ap in apList)
                            {
                                Dispatcher.BeginInvoke(new Action(delegate ()
                                {
                                    FreeOnlinePlayersList.Items.Add(ap);
                                }));
                            }


                            //// Creator: nrod
                            //// Link: https://stackoverflow.com/a/60206398
                            //Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                            //    this.FreeOnlinePlayersList.ItemsSource = null)
                            //);

                            //Application.Current.Dispatcher.BeginInvoke(new Action(() =>
                            //    this.FreeOnlinePlayersList.ItemsSource = apList)
                            //);

                        }
                        break;
                    #endregion

                    #region case 1: update gameboard data
                    case "1":   // update gameboard

                        // variabels neded: 
                        //int serverMethosCallNumber, string DisplayName, bool hasGameEnded, bool isPlayer1Turn, GameSymbolTypes[] gameboard

                        if (resultData.Length > 1)                                                                  // checks if there is data with the request
                        {
                            lock (objLock)
                            {
                                // Update Client Data
                                mpGame.DisplayName = resultData[1].ToString();
                                mpGame.HasGameEnded = Convert.ToBoolean(resultData[2]);
                                mpGame.IsPlayer1Turn = Convert.ToBoolean(resultData[3]);

                                // Splits up all the game field values
                                string[] gameboardValues = resultData[4].ToString().Split('#');

                                // Asigns all gameboardValues to a GameSymbolTypes[] inside 'newClient'.
                                for (int i = 0; i < gameboardValues.Length; i++)
                                {
                                    mpGame.GameboardFildsArray[i] = (GameSymbolTypes)Enum.Parse(typeof(GameSymbolTypes), gameboardValues[i]);
                                }
                            }
                        }

                        break;
                    #endregion

                    #region case 2: Oponent left the game - message & action
                    case "2":   // Oponent left
                        // variabels neded: 
                        //int serverMethosCallNumber, hasGameEnded

                        if (resultData.Length > 1)
                        {
                            mpGame.HasGameEnded = Convert.ToBoolean(clientDataString[1]);

                            // Nitify the user.
                            bool awnser = true;
                            int counter = 0;
                            do
                            {
                                awnser = gameLogic.GenericMessageBoxOk("The oponent left... You Win");

                                #region backup break

                                if (counter >= 10)
                                {
                                    awnser = false;
                                }
                                counter++;

                                #endregion

                            } while (awnser != true);

                            // Kill the connection the the server.
                            killLoop = false;                               // breaks out of outer while loop
                            mpGame.ClientSocket.Disconnect(false);
                            mpGame.ClientSocket.Dispose();

                            // Kill the application
                            App.Current.Shutdown();
                        }
                        break;
                    #endregion

                    #region case 3: 
                    case "3":

                        // do somthing 

                        break;
                    #endregion

                    default:
                        break;
                }
            }
        }

        #endregion


        /// <summary>
        /// Displays a list of oponents and changes a UI label based on selection
        /// </summary>
        /// <param name="sender">ListBox</param>
        /// <param name="e"></param>
        private void FreeOnlinePlayersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (FreeOnlinePlayersList.SelectedItem != null)                     // Checks if the selectet item is null or not
                {
                    // Seperate DisplayName from selected item
                    string[] opponentSplit = FreeOnlinePlayersList.SelectedItem.ToString().Split(':');

                    SelectedOponentName_Label.Content = opponentSplit[0];           // Displays Selected opponent name on a UI Label

                    // the selected item info equal to an instance of an 'AvailableOpponent'
                    selectedAvailableOpponent.DisplayName = opponentSplit[0];
                    selectedAvailableOpponent.ClientServerId = opponentSplit[1];
                }
                else
                {
                    SelectedOponentName_Label.Content = "N/A";                      // if Selected opponent value is null set label to "N/A"
                }
            }
            catch (NullReferenceException)
            {
                SelectedOponentName_Label.Content = "N/A";                          // if Selected opponent reference is null set label to "N/A"
            }
            catch (Exception)
            {

            }
                             

        }


        /// <summary>
        /// Closes the game
        /// </summary>
        /// <param name="sender">Button</param>
        /// <param name="e"></param>
        private void Close_Application_Click(object sender, RoutedEventArgs e)
        {
            // Tils the server a player has closed the application
            gameLogic.SendCloseApplicationNotisToServer(mpGame.ClientSocket);

            App.Current.Shutdown();
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


        /// <summary>
        /// Makes the entire window drageble so it can be moved around
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiplayerWindow_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }


        /// <summary>
        /// Sets a list of opponents equal to Listbox items
        /// </summary>
        /// <param name="apList">List<AvailableOpponent></param>
        /// <exception cref="NullReferenceException">Sets the List to "", and returns an errorbox the the UI</exception>
        public void UpdateUiListOfOpponents(List<AvailableOpponent> apList)
        {
            try
            {
                foreach (var ap in apList)
                {
                    FreeOnlinePlayersList.Items.Add(ap.DisplayName + ":" + ap.ClientServerId);
                }
                
            }
            catch (NullReferenceException)
            {
                // Sets the Listboc itemList to empty;
                FreeOnlinePlayersList.ItemsSource = "";

                // Sends an error MessageBox to the UI;
                gameLogic.GenericMessageBoxOk("The Available Opponent list is empty");
            }
            catch (Exception)
            {

            }
        }
    }
}
