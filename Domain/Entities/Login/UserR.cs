using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Login
{
   public class UserR
    {
   
        public int user_id { get; set; }
        public string accessToken { get; set; }
        public string username { get; set; }
        public int userType { get; set; }
        public double rating { get; set; }
        public string fullName { get; set; }
        public string phoneNumber { get; set; }
        public string countryCode { get; set; }
        public int isActive { get; set; }
        public string email { get; set; }
        public string img { get; set; }
        public string socialId { get; set; }

        //workshop
        public string recordNumber { get; set; }
        public string  expiryDate { get; set; }
        public string shopNumber { get; set; }
        public string shopName { get; set; }
        public int city_Id { get; set; }
        public int    industrialAreaId { get; set; }
        public string industrialAreaName { get; set; }
        

        public double LocationX { get; set; }
        public double LocationY { get; set; }
        



        public UserR()
        {
            
            user_id = 0;
            accessToken = "";
            username = "";
            phoneNumber = "";
            countryCode = "";
            userType = 0;
            rating = 0;
            fullName = "";
            email = "";
            img = "";
            socialId = "";

          
            recordNumber = "";
            expiryDate = DateTime.Now.ToString("dd-MM-yyyy");
            shopNumber = "";
            shopName = "";
            industrialAreaId = 0;
            industrialAreaName = "";
            LocationX = 0;
            LocationY  = 0;
            city_Id = 0;



        }



    }
}
