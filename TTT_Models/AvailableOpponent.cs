using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Text;

namespace TTT_Models
{
    public class AvailableOpponent
    {
        public string ClientServerId { get; set; }
        public string DisplayName { get; set; }

        public override string ToString() { 
            return DisplayName; 
        }
    }
}
