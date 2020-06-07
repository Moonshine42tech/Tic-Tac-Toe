using System;
using System.Collections.Generic;
using System.Text;
using TTT_Models;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows;
using System.Windows.Documents;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Net.Sockets;
using System.Net;

namespace TTT_Repository
{
    public class GameLogic
    {
        #region Game status check

        /// <summary>
        /// This method is validating if a player has won or if the game is a tie
        /// </summary>
        /// <param name="gameboardFildsArray">Enum array of game symbols</param>
        /// <param name="buttons">list of System.Windows.Controls.Button buttons</param>
        /// <param name="hasGameEnded">Game status</param>
        /// <returns></returns>
        public bool CheckifGameHasEnded(GameSymbolTypes[] gameboardFildsArray, List<System.Windows.Controls.Button> buttons, bool hasGameEnded)
        {
            try
            {
                #region Check for horisontal wins 
                // Check for horisontal wins 
                if (gameboardFildsArray[0] != GameSymbolTypes.Free && (gameboardFildsArray[0] & gameboardFildsArray[1] & gameboardFildsArray[2]) == gameboardFildsArray[0])
                {
                    // Changes the color on the bittons that got three in a row (winning buttons).
                    List<Button> winningButtons = new List<Button>() { buttons[0], buttons[1], buttons[2] };
                    SetButtonColor(winningButtons, true);

                    // Disabels all remaining buttons on the fild.
                    List<Button> disabelButtons = new List<Button>() { buttons[3], buttons[4], buttons[5], buttons[6], buttons[7], buttons[8] };
                    DisabelsButtons(disabelButtons);

                    // returns game state bool value
                    return hasGameEnded = true;
                }
                if (gameboardFildsArray[3] != GameSymbolTypes.Free && (gameboardFildsArray[3] & gameboardFildsArray[4] & gameboardFildsArray[5]) == gameboardFildsArray[3])
                {
                    // Changes the color on the bittons that got three in a row (winning buttons).
                    List<Button> winningButtons = new List<Button>() { buttons[3], buttons[4], buttons[5] };
                    SetButtonColor(winningButtons, true);

                    // Disabels all remaining buttons on the fild.
                    List<Button> disabelButtons = new List<Button>() { buttons[0], buttons[1], buttons[2], buttons[6], buttons[7], buttons[8] };
                    DisabelsButtons(disabelButtons);

                    // returns game state bool value
                    return hasGameEnded = true;
                }
                if (gameboardFildsArray[6] != GameSymbolTypes.Free && (gameboardFildsArray[6] & gameboardFildsArray[7] & gameboardFildsArray[8]) == gameboardFildsArray[6])
                {
                    // Changes the color on the bittons that got three in a row (winning buttons).
                    List<Button> winningButtons = new List<Button>() { buttons[6], buttons[7], buttons[8] };
                    SetButtonColor(winningButtons, true);

                    // Disabels all remaining buttons on the fild.
                    List<Button> disabelButtons = new List<Button>() { buttons[0], buttons[1], buttons[2], buttons[3], buttons[4], buttons[5] };
                    DisabelsButtons(disabelButtons);

                    // returns game state bool value
                    return hasGameEnded = true;
                }
                #endregion

                #region check for vertical wins 
                // check for vertical wins 
                if (gameboardFildsArray[0] != GameSymbolTypes.Free && (gameboardFildsArray[0] & gameboardFildsArray[3] & gameboardFildsArray[6]) == gameboardFildsArray[0])
                {
                    // Changes the color on the bittons that got three in a row (winning buttons).
                    List<Button> winningButtons = new List<Button>() { buttons[0], buttons[3], buttons[6] };
                    SetButtonColor(winningButtons, true);

                    // Disabels all remaining buttons on the fild.
                    List<Button> disabelButtons = new List<Button>() { buttons[1], buttons[2], buttons[4], buttons[5], buttons[7], buttons[8] };
                    DisabelsButtons(disabelButtons);

                    // returns game state bool value
                    return hasGameEnded = true;
                }
                if (gameboardFildsArray[1] != GameSymbolTypes.Free && (gameboardFildsArray[1] & gameboardFildsArray[4] & gameboardFildsArray[7]) == gameboardFildsArray[1])
                {
                    // Changes the color on the bittons that got three in a row (winning buttons).
                    List<Button> winningButtons = new List<Button>() { buttons[1], buttons[4], buttons[7] };
                    SetButtonColor(winningButtons, true);

                    // Disabels all remaining buttons on the fild.
                    List<Button> disabelButtons = new List<Button>() { buttons[0], buttons[2], buttons[3], buttons[5], buttons[6], buttons[8] };
                    DisabelsButtons(disabelButtons);

                    // returns game state bool value
                    return hasGameEnded = true;
                }
                if (gameboardFildsArray[2] != GameSymbolTypes.Free && (gameboardFildsArray[2] & gameboardFildsArray[5] & gameboardFildsArray[8]) == gameboardFildsArray[2])
                {
                    // Changes the color on the bittons that got three in a row (winning buttons).
                    List<Button> winningButtons = new List<Button>() { buttons[2], buttons[5], buttons[8] };
                    SetButtonColor(winningButtons, true);

                    // Disabels all remaining buttons on the fild.
                    List<Button> disabelButtons = new List<Button>() { buttons[0], buttons[1], buttons[3], buttons[4], buttons[6], buttons[7] };
                    DisabelsButtons(disabelButtons);

                    // returns game state bool value
                    return hasGameEnded = true;
                }
                #endregion

                #region Check for oblique wins
                // Check for oblique wins
                if (gameboardFildsArray[0] != GameSymbolTypes.Free && (gameboardFildsArray[0] & gameboardFildsArray[4] & gameboardFildsArray[8]) == gameboardFildsArray[0])
                {
                    // Changes the color on the bittons that got three in a row (winning buttons).
                    List<Button> winningButtons = new List<Button>() { buttons[0], buttons[4], buttons[8] };
                    SetButtonColor(winningButtons, true);

                    // Disabels all remaining buttons on the fild.
                    List<Button> disabelButtons = new List<Button>() { buttons[1], buttons[2], buttons[3], buttons[5], buttons[6], buttons[7] };
                    DisabelsButtons(disabelButtons);

                    // returns game state bool value
                    return hasGameEnded = true;
                }
                if (gameboardFildsArray[2] != GameSymbolTypes.Free && (gameboardFildsArray[2] & gameboardFildsArray[4] & gameboardFildsArray[6]) == gameboardFildsArray[2])
                {
                    // Changes the color on the bittons that got three in a row (winning buttons).
                    List<Button> winningButtons = new List<Button>() { buttons[2], buttons[4], buttons[6] };
                    SetButtonColor(winningButtons, true);

                    // Disabels all remaining buttons on the fild.
                    List<Button> disabelButtons = new List<Button>() { buttons[0], buttons[1], buttons[3], buttons[5], buttons[7], buttons[8] };
                    DisabelsButtons(disabelButtons);

                    // returns game state bool value
                    return hasGameEnded = true;
                }
                #endregion

                #region Check for no more game filds (Tie game)
                // Check for no more game filds (Tie game)
                if (!gameboardFildsArray.Any(result => result == GameSymbolTypes.Free))         // if there is no more free game filds
                {
                    // Changes the color on the bittons that got three in a row (winning buttons).
                    List<Button> winningButtons = new List<Button>() { buttons[0], buttons[1], buttons[2], buttons[3], buttons[4], buttons[5], buttons[6], buttons[7], buttons[8] };
                    SetButtonColor(winningButtons, false);

                    // returns game state bool value
                    return hasGameEnded = true;
                }
                #endregion

                return false; // temp value
            }
            catch (Exception)
            {

                throw;
            }

        
            
        }


        /// <summary>
        /// Sets the background color of a given list of 'System.Windows.Controls.Button'
        /// </summary>
        /// <param name="winningButtons">buttons to be colored</param>
        /// <param name="winningState">A bullian to control what color should be useed</param>
        public void SetButtonColor(List<System.Windows.Controls.Button> winningButtons, bool winningState)
        {
            try
            {
                if (winningState == true)
                {
                    // Sets the background color of alle buttons.
                    foreach (var button in winningButtons)
                    {
                        // Sets the Background color to 'light green'
                        button.Background = Brushes.LightGreen;
                    }
                }
                else
                {
                    // Sets the background color of alle buttons.
                    foreach (var button in winningButtons)
                    {
                        // Sets the Background color to 'light yellow'
                        button.Background = Brushes.LightYellow;
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        #endregion


        #region change player turn

        /// <summary>
        /// Changes the turn between the players using a bool 'player1Turn'
        /// </summary>
        /// <param name="isPlayer1Turn">true = Player1 | false = Player2</param>
        /// <returns> the next players turn in form of a bool</returns>
        public bool TurnChange(bool isPlayer1Turn)
        {
            try
            {
                if (isPlayer1Turn == true) 
                {
                    isPlayer1Turn = false;
                    return isPlayer1Turn;
                }
                else 
                {
                    isPlayer1Turn = true;
                    return isPlayer1Turn;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        #endregion


        #region UI Bottons 

        /// <summary>
        /// Disabels all buttons send to it.
        /// </summary>
        /// <param name="buttons">a list of System.Windows.Controls.Button</param>
        public void DisabelsButtons(List<System.Windows.Controls.Button> buttons)
        {
            try
            {
                // Disable alle in-game buttons.
                foreach (var button in buttons)
                {
                    // Disable a button
                    button.IsEnabled = false;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        /// <summary>
        /// Manipulates a given list of WPF buttons
        /// </summary>
        /// <param name="buttons">A list of buttons using the 'PresentationFramework.dll' and 'PresentationCore.dll' Assemblies from WPF</param>
        public void SetAllButtonsContent(List<System.Windows.Controls.Button> buttons)
        {
            try
            {
                foreach (var button in buttons)
                {
                    // Changes the content of alle buttons
                    button.Content = "";

                    // Enabels a button
                    button.IsEnabled = true;

                    // Changes color on both forground and background
                    button.Background = Brushes.White;
                }
            }
            catch (NullReferenceException)
            {

            }
            catch (Exception)
            {

            }

        }


        /// <summary>
        /// Sets the symbol value, dependig on what players turn it is.
        /// </summary>
        /// <param name="button">A clicked button from the UI</param>
        /// <param name="isPlayer1Turn">a players turn</param>
        public bool SetButtonSymbol(System.Windows.Controls.Button button, bool isPlayer1Turn)
        {
            try
            {
                button.Content = isPlayer1Turn ? "X" : "O";     // Sets the 'Content' on a button

                if (isPlayer1Turn == true)
                {
                    button.Foreground = Brushes.Blue;
                }
                else
                {
                    button.Foreground = Brushes.Red;
                }

                return TurnChange(isPlayer1Turn);               // Changes the players turn
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion


        #region Checking Symbols

        /// <summary>
        /// Checks if a specific fild on the gameboard is free or not
        /// </summary>
        /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        /// <param name="gameboardFildIndex">The selected/clicked idex of the array</param>
        /// <returns></returns>
        public bool CheckGameboardFildStatus(GameSymbolTypes[] gameboardFildsArray, int gameboardFildIndex)
        {
            if (gameboardFildsArray[gameboardFildIndex] != GameSymbolTypes.Free)
            {
                return false;           // if the gameboard file is NOT 'free' return false
            }
            else
            {
                return true;            // if the gameboard file is 'free' return true
            }
        }

        /// <summary>
        /// Simply sets a value on the gameboard
        /// </summary>
        /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        /// <param name="gameboardFildIndex">The selected/clicked idex of the array</param>
        /// <param name="player1Turn">whhos turn is it</param>
        public void SetAGameboardFildValue(GameSymbolTypes[] gameboardFildsArray, int gameboardFildIndex, bool isPlayer1Turn)
        {
            if (isPlayer1Turn == true)
            {
                // Player 1 is always X
                gameboardFildsArray[gameboardFildIndex] = GameSymbolTypes.Cross;
            }
            else
            {
                // Player 2 is always O
                gameboardFildsArray[gameboardFildIndex] = GameSymbolTypes.Cirkle;
            }
        }

        #endregion


        #region Prepare symbols

        /// <summary>
        /// Explisitly sets alle filds to free. 
        /// Used for clearing the gameboard between or after game(s).
        /// </summary>
        /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        public void SetAllSymbolTypesToFree(GameSymbolTypes[] gameboardFildsArray)
        {
            try
            {
                // Explisitly sets alle filds to free
                for (int i = 0; i < gameboardFildsArray.Length; i++)
                {
                    gameboardFildsArray[i] = GameSymbolTypes.Free;
                }
            }
            catch (NullReferenceException)
            {

            }
            catch (Exception)
            {

            }

        }

        /// <summary>
        /// Chouse if you will make a new gameboard or just clear the gameboard
        /// </summary>
        /// <param name="gameboardFildsArray">gameboard array fild(s)</param>
        /// <param name="amountOfFilds">Amount of buttons/filds on the gameboard</param>
        /// <param name="newBoeardOrClearBoard"> true = new gameboard | false = clear gameboard)</param>
        public void PrepareTheGameboardSymbols(GameSymbolTypes[] gameboardFildsArray, int amountOfFilds, bool newBoeardOrClearBoard)
        {
            try
            {
                if (newBoeardOrClearBoard == true)
                {
                    gameboardFildsArray = new GameSymbolTypes[amountOfFilds];           // Creates a new object of GameSymbolTypes with default value 'free'
                    SetAllSymbolTypesToFree(gameboardFildsArray);                       // Explisitly set all the fild sympols to 'Free'

                }
                else
                {
                    SetAllSymbolTypesToFree(gameboardFildsArray);                       // Explisitly set all the sympols to 'Free'

                }
            }
            catch (NullReferenceException)
            {

            }
            catch (Exception)
            {

            }
        }

        #endregion


        #region MessageBox(s)

        /// <summary>
        /// Makes a costum error message popup on the windows desktop
        /// </summary>
        /// <param name="msg">Costum error message</param>
        public void ErrorMessageBoxPopup(string msg)
        {
            MessageBox.Show("Error: " + msg);
        }

        /// <summary>
        /// Makes a costum generic MessageBox
        /// </summary>
        /// <param name="msg">costum string message</param>
        public void GenericMessageBoxPopup(string msg)
        {
            MessageBox.Show(msg);
        }

        #endregion


        #region Multiblayer specific methods

        #region Serilize and Deserilize

        /// <summary>
        /// Serializes Game data into a byte[] array
        /// </summary>
        /// <param name="DisplayName">String from UI</param>
        /// <param name="hasGameEnded">TTT_Models.GameModel</param>
        /// <param name="isPlayer1Turn">TTT_Models.GameModel</param>
        /// <param name="gameboard">TTT_Models.GameModel</param>
        /// <returns>byte[] Array</returns>
        public byte[] ConvertDataToByteArray(int serverMethosCallNumber, string DisplayName, bool hasGameEnded, bool isPlayer1Turn, GameSymbolTypes[] gameboard)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            // Make one big datastring
            string dataString = (serverMethosCallNumber + "~" + DisplayName + "~" + hasGameEnded + "~" + isPlayer1Turn + "~" + gameboard);

            // Serialize the dataString and returns is in form of a array 
            bf.Serialize(ms, dataString);
            return ms.ToArray();
        }

        /// <summary>
        /// Serializes Game data into a byte[] array
        /// </summary>
        /// <param name="serverMethosCallNumber">int</param>
        /// <param name="hasGameEnded">TTT_Models.GameModel</param>
        /// <param name="isPlayer1Turn">TTT_Models.GameModel</param>
        /// <param name="gameboard">TTT_Models.GameModel</param>
        /// <returns>byte[] Array</returns>
        public byte[] ConvertDataToByteArray(int serverMethosCallNumber, bool hasGameEnded, bool isPlayer1Turn, GameSymbolTypes[] gameboard)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            // Make one big datastring
            string dataString = (serverMethosCallNumber + "~" + hasGameEnded + "~" + isPlayer1Turn + "~" + gameboard);

            // Serialize the dataString and returns is in form of a array 
            bf.Serialize(ms, dataString);
            return ms.ToArray();
        }

        /// <summary>
        /// Serializes Game data into a byte[] array
        /// </summary>
        /// <param name="serverMethosCallNumber">int</param>
        /// <returns>byte[] Array</returns>
        public byte[] ConvertDataToByteArray(int serverMethosCallNumber)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            // Make one big datastring
            string dataString = serverMethosCallNumber.ToString();

            // Serialize the dataString and returns is in form of a array 
            bf.Serialize(ms, dataString);
            return ms.ToArray();
        }


        /// <summary>
        /// Deserializees a serialized string
        /// </summary>
        /// <param name="serializedDataString">A Serialize string in form of a byte[] array</param>
        /// <returns>A deserialized string</returns>
        public object ConvertByteArrayToData(byte[] serializedDataString)
        {
            BinaryFormatter bf = new BinaryFormatter();
            MemoryStream ms = new MemoryStream();

            // Deserialize the dataString and returns is to a string
            ms.Write(serializedDataString, 0, serializedDataString.Length);         // Insert 'serializedDataString' into the memoryStream
            ms.Seek(0, SeekOrigin.Begin);                                           // sets memoryStream start point to '0'
            object obj = (object)bf.Deserialize(ms);                                // Deserialize the memoryStream

            return obj;                                                  // returns the Deserialized memoryStream as a string
        }

        #endregion 


        #region Connect and send to Server

        /// <summary>
        /// Makes a connection request to the server
        /// </summary>
        /// <param name="master">A socket using System.Net</param>
        /// <param name="ip">string</param>
        /// <param name="port">int</param>
        public void ConnectToServer(Socket master, string ip, int port)
        {
            try
            {
                IPEndPoint ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);                           // Ipadress and port number for the Server
                master.Connect(ipEnd);                                                                  // Aim at server
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// // Send a data in form of a byte[] array to a socket connection.
        /// </summary>
        /// <param name="master">a websocket using System.Net.Sockets</param>
        /// <param name="SeriliesedDataString"></param>
        public void SendDataToServer(Socket master, byte[] SeriliesedDataString)
        {
            try
            {
                // Send a data in form of a byte[] array
                master.Send(Encoding.ASCII.GetBytes(SeriliesedDataString.ToString()));
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        /// <summary>
        /// Alarms the server that a player has left the game
        /// </summary>
        /// <param name="master">a websocket using System.Net.Sockets</param>
        /// <exception cref="SocketException">Does not send anything is the socket is not connected</exception>
        public void SendCloseApplicationNotisToServer(Socket master)
        {
            try
            {
                byte[] methodCall = ConvertDataToByteArray(2);                  // Selects the Method you want to trigger
                master.Send(Encoding.ASCII.GetBytes(methodCall.ToString()));    // Send message to server
            }
            catch(SocketException)
            {
                // Don't send anything is the socket if not connected
            }
            catch (Exception)
            {

                throw;
            }
            
        }


        #endregion

        #endregion

    }
}