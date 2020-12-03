using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Offers
{
   public class UserOffer
    {
        public string modelname { get; set; } = "";
        public string brandname { get; set; } = "";
        public string colorname { get; set; } = "";
        public string ORDERTYPE_NAME { get; set; } = "";
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public string carImage { get; set; } = "";
        public string userImage { get; set; } = "";


        public int OFFER_ID { get; set; } = 0;
        public int ORDER_ID { get; set; } = 0;
        public string ORDER_DATE { get; set; } = "";
        public  Int64 ORDER_NO { get; set; } = 0;
        public string SUPPLIER_Name { get; set; } = "";
        public string PHONENUMBER { get; set; } = "";
        public string supplierImage { get; set; } = "";
        public double PRICE { get; set; } = 0;
        public double lat { get; set; } = 0;
        public double lng { get; set; } = 0;
        public int timeValue { get; set; } = 0;
        public int timeFlag { get; set; } = 0;
        public double DISTANCE { get; set; } = 0;
        public string time { get; set; } = "";
        public double Rating { get; set; } = 0;
        public int Rater_No { get; set; } = 0;
        public string RateType { get; set; } = "";
        public int RateTypeId { get; set; } = 0;
        public int offerStatus { get; set; } = 0;
        


    }
}
