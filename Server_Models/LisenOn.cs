using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Server_Models
{
    public class LisenOn
    {
        public string IpAddress { get; set; }                   // Listening for a connection on this ip address
        public int PortNumber { get; set; }                     // Listening for a connection on this Port Number
    }
}
