using System;
using System.Collections.Generic;
using System.Text;

namespace TTT_Models
{
    public class PlayerModel
    {
        public string DisplayName { get; set; }         // Is only used to se the ingame name of who you are playing agenst.
        public bool PlayerID { get; set; }              // PlayerModel 1 will always be true. and player 2 will always be false.
    }   
}
