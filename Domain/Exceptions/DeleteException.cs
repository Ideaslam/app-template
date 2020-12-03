using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Exceptions
{
   public class DeleteException :Exception
    {
        public string RespMessage { get; set; }  
 
        public string ErrorMessage { get; set; }
 

        public DeleteException(string lang)
        {
            this.ErrorMessage = Message;
            this.RespMessage = GetMessage(lang, TypeM.DEFAULT, defaultM.DELETE_ERROR); 


        }

        public DeleteException(string lang , string ErrorMessage)
        {
            this.RespMessage = GetMessage(lang, TypeM.DEFAULT, defaultM.DELETE_ERROR);
            this.ErrorMessage = ErrorMessage;

        }
    }
}
