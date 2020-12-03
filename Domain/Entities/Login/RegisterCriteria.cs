using System;
using System.Collections.Generic;
using System.Text;

namespace Login
{
   public class RegisterCriteria
    {
        public string username    { get; set; }
        public string password    { get; set; }
        public string phoneNumber { get; set; }
        public string countryCode { get; set; }
        public int isActive { get; set; }
        public int    userType    { get; set; } // 1 person , 2 workshop
        public string email { get; set; }
        public string userImage { get; set; }
        public string socialId { get; set; }
        public bool   doInsert    { get; set; }

    }
}
