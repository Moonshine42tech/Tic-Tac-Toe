using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace TTT_Models
{
    public class AvailableOpponent
    {
        public string DisplayName { get; set; }
        public Socket ClientSocket { get; set; }
    }
}
