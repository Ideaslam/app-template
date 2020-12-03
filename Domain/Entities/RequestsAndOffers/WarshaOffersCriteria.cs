using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class WarshaOffersCriteria
    {

        

        public int accident_Id { get; set; }
        public int offer_id { get; set; }
        public int workshop_id { get; set; }

        //public string accident_date { get; set; }
        public string offerDateTime { get; set; }
        // public string EstimatedTime { get; set; }
         public int timeValue { get; set; }
        public int timeFlag { get; set; }
        public string Price { get; set; }


        public string plateNumber  { get; set; }
      
        public int Status { get; set; }
        public string statusNameEn { get; set; }
        public string statusNameAr { get; set; }
      

    }
}
