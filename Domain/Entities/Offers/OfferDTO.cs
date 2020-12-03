using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Offers
{
  public  class OfferDTO
    {


        public int OFFER_ID { get; set; } = 0;
        public int ORDER_ID { get; set; } = 0;
        public string SUPPLIER_Name { get; set; } = "";
        public int CountryCode { get; set; } = 0;
        public string PHONENUMBER { get; set; } = ""; 
        public string supplierImage { get; set; } = "";
        public double PRICE { get; set; } = 0;
        public double lat { get; set; } = 0;
        public double lng { get; set; } = 0;
        public int timeValue { get; set; } = 0;
        public int timeFlag { get; set; } = 0;

        public double DISTANCE { get; set; } = 0;
        public double Rating { get; set; } = 0;
        public string RateType { get; set; } = "";
        public int RateTypeId { get; set; } = 0;
        public int offerStatus { get; set; } = 0;
        public string time { get; set; } = "";

    }
}
