using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Exceptions
{
   public class EmptyViewException : Exception
    {
       public string RespMessage { get; set; }
       
       public string ErrorMessage { get; set; }

        public EmptyViewException(string lang)
        {
            ErrorMessage = Message;
            RespMessage = GetMessage(lang, TypeM.DEFAULT, defaultM.NODATA); 
        }


        public EmptyViewException(string lang , string message )
        {
            ErrorMessage = Message;
            RespMessage = message;
        }






    }
}
