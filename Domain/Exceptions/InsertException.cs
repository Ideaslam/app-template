using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Exceptions
{
   public class InsertException :Exception
    {
        public string RespMessage { get; set; } 
        public string ErrorMessage { get; set; }

       
    

        public InsertException(string lang )
        {
            this.ErrorMessage = Message;
            this.RespMessage = GetMessage(lang, TypeM.DEFAULT, defaultM.INSERT_ERROR);
        }

        public InsertException(string lang  ,string ErrorMessage)
        {
            this.RespMessage = GetMessage(lang, TypeM.DEFAULT, defaultM.INSERT_ERROR);
            this.ErrorMessage = ErrorMessage;
            

        }
    }
}
