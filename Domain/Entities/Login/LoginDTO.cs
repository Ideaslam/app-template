using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Login
{
   public  class LoginDTO
    {

        public string accessToken { get; set; }
        public string user_id { get; set; }
        public string username { get; set; }
        public double rating { get; set; }
        public int userType { get; set; }
    }
}
