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

namespace Tic_Tac_Toe
{
    /// <summary>
    /// Interaction logic for Game_Multiplayer.xaml
    /// </summary>
    public partial class Game_Multiplayer : Window
    {
        MultiplayerGameModel mpGame = new MultiplayerGameModel();
        GameLogic gameLogic = new GameLogic();

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
            ConnectToServer_Button.IsEnabled = false;       // so the user don't connect to the server again while in game
            PlayerDisplayName_textbox.IsEnabled = false;    // Disabels the users DisplayName input


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
                // Send a connection request to the server
                gameLogic.ConnectToServer(mpGame.ClientSocket, mpGame.IpAddress, mpGame.PortNumber);

                // Serilize all necessary data to byte[] 
                byte[] serilizedDataString_ToServer = gameLogic.ConvertDataToByteArray(0, mpGame.DisplayName = PlayerDisplayName_textbox.Text, mpGame.HasGameEnded, mpGame.IsPlayer1Turn, mpGame.GameboardFildsArray); 


                // Send Data to the server
                gameLogic.SendDataToServer(mpGame.ClientSocket, serilizedDataString_ToServer);


                // Enabels the Play button
                Play_Button.IsEnabled = true;


                // Lisen to the server
                Thread LisenForMessageFromServer = new Thread(LisenToServerMessage);
                LisenForMessageFromServer.Start();
            }
            catch (Exception)
            {

            }

        }


        /// <summary>
        /// Sets the port number for later use
        /// </summary>
        /// <param name="sender">Textbox</param>
        /// <param name="e"></param>
        private void ServerPortNumber_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Converts ServerPortNumber_TextBox.Text to a int
            mpGame.PortNumber = Convert.ToInt32(ServerPortNumber_TextBox.Text);
        }


        /// <summary>
        /// Sets the Ip address for later use
        /// </summary>
        /// <param name="sender">Textbox</param>
        /// <param name="e"></param>
        private void ServerIpAddress_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            mpGame.IpAddress = ServerIpAddress_TextBox.Text;
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

                string clientDataString = gameLogic.ConvertByteArrayToData(resivedData).ToString();                      // Deserializees the data the client has send. | returns one big data string
                clientDataString.Split('~');
                switch (clientDataString[0])
                {
                    #region case 0: update gameboard data
                    case '0':   // update gameboard

                        // variabels neded: 
                        //int serverMethosCallNumber, string DisplayName, bool hasGameEnded, bool isPlayer1Turn, GameSymbolTypes[] gameboard

                        lock (objLock)
                        {
                            // Update Client Data
                            mpGame.DisplayName = clientDataString[1].ToString();
                            mpGame.HasGameEnded = Convert.ToBoolean(clientDataString[2]);
                            mpGame.IsPlayer1Turn = Convert.ToBoolean(clientDataString[3]);
                            
                            // Concerts the gameboard in char format to an array of 'GameSymbolTypes'
                            IEnumerable<GameSymbolTypes> gameboardSymbilsArray = clientDataString[4].ToString().Select(a => (GameSymbolTypes)Enum.Parse(typeof(GameSymbolTypes), a.ToString()));
                            mpGame.GameboardFildsArray = (GameSymbolTypes[])gameboardSymbilsArray;

                            // Invokes??? 
                        }
                        break;
                    #endregion

                    #region case 1: Oponent left message & action
                    case '1':   // Oponent left
                        // variabels neded: 
                        //int serverMethosCallNumber, hasGameEnded

                        mpGame.HasGameEnded = Convert.ToBoolean(clientDataString[1]);

                        // Nitify the user.
                        gameLogic.GenericMessageBoxPopup("The oponent left... You Win");

                        // Sleep thred for x amount of time, so the user can read the messagebox. 
                        Thread.Sleep(5000);         // 5 sec.

                        // Kill the connection the the server.
                        killLoop = false;
                        mpGame.ClientSocket.Disconnect(false);
                        mpGame.ClientSocket.Dispose();

                        // Kill the application
                        App.Current.Shutdown();

                        break;
                    #endregion

                    #region case 2: 
                    case '2':

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
        /// Displays a list of oponents
        /// </summary>
        /// <param name="sender">ListBox</param>
        /// <param name="e"></param>
        private void FreeOnlinePlayersList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // Set oponent equal to selection

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

    }
}
