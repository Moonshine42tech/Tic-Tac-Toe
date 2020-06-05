using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using DTOs;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using TTT_Models;

namespace webSocketClient2Multiconnection
{
    public partial class Form1 : Form
    {
        string ip;                      // Server ip address
        int port;                       // Server port

        Socket master;
        IPEndPoint ipEnd;

        public Form1()
        {
            InitializeComponent();
        }

        private void SendButton_Click(object sender, EventArgs e)
        {
            GameObject GameObj = new GameObject()
            { 
                DisplayName = "Kasper", 
                hasGameEnded = false, 
                isPlayer1Turn = true, 
                O_Score = 0, 
                X_Score = 1, 
                GameboardFildsArray = new GameSymbolTypes[9] 
                { 
                    (GameSymbolTypes)0, 
                    (GameSymbolTypes)1, 
                    (GameSymbolTypes)0, 
                    
                    (GameSymbolTypes)2, 
                    (GameSymbolTypes)1,
                    (GameSymbolTypes)2,

                    (GameSymbolTypes)0,
                    (GameSymbolTypes)1,
                    (GameSymbolTypes)0 
                
                }
            };                   // Initialize a new GameObject
            master.Send(GameObjectToBye(GameObj));                      // Sending a GameObject to the server as byte[] array

            //master.Send(Encoding.ASCII.GetBytes(TextMessageBox.Text.ToString()));


            //int byteArraySize = (TextMessageBox.Text.Length + 18);
            //byte[] piggybackMsgBuffer = new byte[byteArraySize];
            //master.Receive(piggybackMsgBuffer);

            //if (piggybackMsgBuffer != null)
            //{
            //    PiggybackMessageBox.Text = Encoding.ASCII.GetString(piggybackMsgBuffer);
            //}

        }

        private void ConnectToServer_Click(object sender, EventArgs e)
        {
            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);                   // Ipadress and port number for the Server
            master.Connect(ipEnd);
        }

        private void DisconnectFromServer_Click(object sender, EventArgs e)
        {
            master.Close();
        }

        private void IpAddressBox_TextChanged(object sender, EventArgs e)
        {
            ip = IpAddressBox.Text;
        }

        private void PortNumberBox_TextChanged(object sender, EventArgs e)
        {
            port = Convert.ToInt32(PortNumberBox.Text);
        }

        /// <summary>
        /// Serializes a GameObject into a byte[] array
        /// </summary>
        /// <param name="gameObj">GameObject DTO</param>
        /// <returns>A MemoryStream in form of a byte[] array</returns>
        private byte[] GameObjectToBye(GameObject gameObj)
        {
            BinaryFormatter bf = new BinaryFormatter(); 
            MemoryStream ms = new MemoryStream();

            bf.Serialize(ms, gameObj);                      // Serializes the gameObj into the MemoryStream
            return ms.ToArray();                            // returns the MemoryStream in form of a byte[] array
        }
    }
}
