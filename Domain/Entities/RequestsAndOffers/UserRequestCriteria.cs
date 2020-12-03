using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class UserRequestCriteria
    {



        public int ORDER_ID { get; set; } = 0;
        public int offer_id { get; set; } = 0;
        public string ORDER_IDENTITY { get; set; } = "";
        public int USER_ID { get; set; } = 0;
        public string PLATENUMBER { get; set; } = "";
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public int CountryCode { get; set; } =0 ;
        public string phoneNumber { get; set; } = "";
        public int VEHICLE_ID { get; set; } = 0;
        public int ORDERTYPE_ID { get; set; } = 0;
        public string ORDERTYPE_NAME { get; set; } = "";
        public string ORDERDATE { get; set; } = "";
        public string MODELNAME  { get; set; } = "";
        public string BRANDNAME  { get; set; } = "";
        public string COLORNAME { get; set; } = "";
        public int OFFERS_COUNT { get; set; } = 0;
        public int ORDER_STATUS { get; set; } = 0;
        public int ISACTIVE { get; set; } = 0;
        public string carIMAGE { get; set; } = "";
        public string userIMAGE { get; set; } = "";
        public string Note { get; set; } = "";
        public double lat { get; set; } = 0;
        public double lng { get; set; } = 0;
        public double distance  { get; set; } = 0;
        public string time { get; set; } = "";

        public List<infoAssign> info { get; set; } = new List<infoAssign>();




    }
}
