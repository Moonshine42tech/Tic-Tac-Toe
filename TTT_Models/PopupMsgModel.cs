using System;
using System.Collections.Generic;
using System.Text;

namespace TTT_Models
{
    public class PopupMsgModel
    {
        public string Title { get; private set; }           // Title on the popup message
        public string Message { get; private set; }         // The message inside the popup
        public bool YesNo { get; private set; }             // Yes = true | No = false
    }
}
