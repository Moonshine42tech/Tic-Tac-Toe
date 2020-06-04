using System;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

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
            master.Send(Encoding.ASCII.GetBytes(TextMessageBox.Text.ToString()));


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
    }
}
