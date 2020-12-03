using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Login
{
   public class UserDTO
    {
         // User Data
        public int user_id { get; set; } = 0;
        public string accessToken { get; set; } = "";
        // public string username { get; set; }
        public int userType { get; set; } = 0;
        public double rating { get; set; } = 0;
        public int rater_no { get; set; } = 0;
        // public string fullName { get; set; }
        public string phoneNumber { get; set; } = "";
        public int countryCode { get; set; } =0;
        //  public int isActive { get; set; }
        // public string email { get; set; }
        public string userImage { get; set; } = "";
        // public string socialId { get; set; }

        public string registeredDate { get; set; } = "";


        // Person Data
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";


        //workshop
        // public string recordNumber { get; set; }
        // public string expiryDate { get; set; }
        public int shopNumber { get; set; } = 0;
        public string shopName { get; set; } = "";
        public string cityName { get; set; } = "";
        public int cityId { get; set; } = 0;
        public string address { get; set; } = "";
        public string supplierType { get; set; } = "";
        //  public int industrialAreaId { get; set; }
        //  public string industrialAreaName { get; set; }
        public double Lat { get; set; } = 0;
        public double Lng { get; set; } = 0;
        public int isActive { get; set; } = 0;


    }
}
