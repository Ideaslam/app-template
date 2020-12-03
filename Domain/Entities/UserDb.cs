using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserDb
    {
        public int userId { get; set; } = 0;
        public string username { get; set; } = "";
        public string fullname { get; set; } = "";
        public string email { get; set; } = "";
        public string phoneNumber { get; set; } = "";
        public string countryCode { get; set; } = "";
        public int isActive { get; set; } = 0;
        public string password { get; set; } = "";
        public int userType { get; set; } = 0;
        public double rating { get; set; } = 0;
        public string accessToken { get; set;  } = "";
        public string userImage { get; set; } = "";
        public string socialId { get; set; } = "";
    }

}
