using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;

namespace ConectionTester
{
    public partial class Form1 : Form
    {

        string ip;                  // Server ip address
        int port;                    // Server port

        Socket master;
        IPEndPoint ipEnd;

        public Form1()
        {
            InitializeComponent(); 
        }

        private void button1_Click(object sender, EventArgs e)
        {
            master.Send(Encoding.ASCII.GetBytes(Message.Text.ToString()));
        }

        private void disconnect_Click(object sender, EventArgs e)
        {
            master.Close();
        }

        private void Connect_Click(object sender, EventArgs e)
        {
            master = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ipEnd = new IPEndPoint(IPAddress.Parse(ip), port);                   // Ipadress and port number for the Server
            master.Connect(ipEnd);
        }

        private void ipAddressBox_TextChanged(object sender, EventArgs e)
        {
            ip = ipAddressBox.Text;
        }

        private void PortBox_TextChanged(object sender, EventArgs e)
        {
            port = Convert.ToInt32(PortBox.Text);
        }

        private void responseFromServer_TextChanged(object sender, EventArgs e)
        {
            //int returnMagSize = (ipAddressBox.Text.Length + 18);
            //byte[] piggybackMsgBuffer = new byte[returnMagSize];

            //master.Receive(piggybackMsgBuffer);

            //responseFromServer.Text = Encoding.ASCII.GetString(piggybackMsgBuffer);
        }
    }
}
