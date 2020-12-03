using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.HelperClass
{
    public class DataTransfer
    {
        public int state { get; set; }
        public bool status { get; set; }
        public string MessageAr { get; set; }
        public string MessageEn { get; set; }
        public string ErrorMessage { get; set; }
        public object innerData { get; set; }
        // public dynamic test { get; set; }

        public DataTransfer(bool status, string MessageAr ,string MessageEn, object innerData)
        {
            this.status = status;
            this.MessageAr = MessageAr;
            this.MessageEn = MessageEn;
            this.innerData = innerData;
            this.ErrorMessage = "";
        }
        public DataTransfer(bool status, string MessageAr, string MessageEn )
        {
            this.status = status;
            this.MessageAr = MessageAr;
            this.MessageEn = MessageEn;
            this.ErrorMessage = "";
        }

        public DataTransfer(bool status, string MessageAr, string MessageEn ,string ErrorMessage)
        {
            this.status = status;
            this.MessageAr = MessageAr;
            this.MessageEn = MessageEn;
            this.ErrorMessage = ErrorMessage;
        }

        public DataTransfer()
        {
            
        }

    }
}
