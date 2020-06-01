using System;
using System.Collections.Generic;
using System.Text;

namespace TTT_Models
{
    public class PopupMsg
    {
        public string Title { get; private set; }           // Title on the popup message
        public string Message { get; private set; }         // The message inside the popup
        public bool YesNo { get; private set; }             // Yes = true | No = false

        /// <summary>
        /// All the necessary values for the Generic popup message
        /// </summary>
        /// <param name="title">Title on the popup message</param>
        /// <param name="message">The message inside the popup</param>
        public void GenericPopupMsg(string title, string message)
        {
            Title = title;
            Message = message;
        }

        /// <summary>
        /// All the necessary values for the Exit popup message
        /// </summary>
        /// <param name="title">Title on the popup message</param>
        /// <param name="yesNo">true or false for the Yes and no buttons (Yes = true | No = false)</param>
        /// <returns></returns>
        public bool ExitPopupMsg(string title, bool yesNo)
        {
            Title = title;
            YesNo = yesNo;

            return YesNo;
        }

    }
}
