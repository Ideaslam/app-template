using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Exceptions
{
   public class UpdateException: Exception
    {

        public string RespMessage { get; set; } 
        public string ErrorMessage { get; set; }

      

        public UpdateException(string lang)
        {
            this.RespMessage = GetMessage(lang, TypeM.DEFAULT, defaultM.UPDATE_ERROR);
            this.ErrorMessage = Message;

        }

        public UpdateException(string lang, string ErrorMessage)
        {
            this.RespMessage = GetMessage(lang, TypeM.DEFAULT, defaultM.UPDATE_ERROR);
            this.ErrorMessage = ErrorMessage;

        }
    }
}
