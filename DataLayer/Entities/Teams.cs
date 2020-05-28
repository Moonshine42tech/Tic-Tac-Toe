using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities
{
    public class Teams
    {
        // PK
        public int TeamID { get; set; }


        // FK
        public int PlayerID { get; set; }


    }
}
