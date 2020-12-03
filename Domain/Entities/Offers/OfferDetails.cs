using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Offers
{
    public class OfferDetails
    {
        public int OFFER_ID { get; set; } = 0;
        public int shop_ID { get; set; } = 0;
        public string FULLNAME { get; set; } = "";
        public string SHOPNUMBER { get; set; } = "";
        public string SHOPNAME { get; set; } = "";
        public double LOCATIONX_WORKSHOP { get; set; } = 0;
        public double LOCATIONY_WORKSHOP { get; set; } = 0;
        public int CONFIRMATION { get; set; } = 0;
        public int timeValue { get; set; } = 0;
        public int timeFlag { get; set; } = 0;
        public double PRICE { get; set; } = 0;
        public string OFFER_DATETIME { get; set; } = "";
        public double rating { get; set; } = 0;
        public string workshop_image { get; set; } = "";
        public int canFinish { get; set; } = 0;
        public int isDelivered { get; set; } = 0;
        public string AreaName { get; set; } = "";
        public string phonenumber { get; set; } = "";





    }
}
