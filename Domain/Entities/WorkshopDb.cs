using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class WorkshopDb 
    {
        public int id { get; set; } = 0;
        public int user_id { get; set; } = 0;
       // public string recordNumber { get; set; } = "";
      //  public DateTime expiryDate { get; set; } = DateTime.Now;
        public string shopNumber { get; set; } = "";
        public string shopName { get; set; } = "";
        public int cityId { get; set; } = 0;
        public string cityName { get; set; } = "";
        public string address { get; set; } = "";
       // public int industrialAreaId { get; set; } = 0;
      //  public string industrialAreaName { get; set; } = "";
        public double lat { get; set; } = 0;
        public double lng { get; set; } = 0;
       // public string fullName { get; set; } = "";
      //  public double rating { get; set; } = 0;
      //  public string accessToken { get; set; } = "";

    }
}
