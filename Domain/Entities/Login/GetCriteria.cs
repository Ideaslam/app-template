using System;
using System.Collections.Generic;
using System.Text;

namespace Login
{
    public class GetCriteria
    {

        public string accessToken { get; set; }
        public string phoneNumber { get; set; }
        public string countryCode { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public int userType { get; set; }
        public string lastLogin { get; set; }
        public int log { get; set; }

        public string oldPassword { get; set; }
        public string code { get; set; }
        public string image { get; set; }
        public int accident_id { get; set; }
        public int offer_id { get; set; }
        public int order_id { get; set; }
        public int rate { get; set; }

        public int user_id { get; set; }
        public int supplierType_id { get; set; }
        public int typePhone { get; set; }
        public double lat { get; set; }
        public double lang { get; set; }

        public int PageSize {get;set ;}

        public int PageNumber { get; set; }

    }
}
