using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class Offer
    {
        public int offer_id { get; set; } = 0;
        public int order_id { get; set; } = 0;
        public int supplier_id { get; set; } = 0;
        public double price { get; set; } = 0;
        public string offerDateTime { get; set; } = ""; 
        //public string estimatedTime { get; set; }
        public int timeValue { get; set; } = 0;
        public int timeFlag { get; set; } = 0;
        public bool confirmation { get; set; } = false;
        public bool finishFlag { get; set; } = false;
        public string accessToken { get; set; } = "";
    }
}
