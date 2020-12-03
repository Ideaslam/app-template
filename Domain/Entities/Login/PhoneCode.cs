using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Login
{
   public class PhoneCode
    {
        public string phoneNumber { get; set; }
        public string countryCode { get; set; }
        public int code { get; set; }

    }
}
