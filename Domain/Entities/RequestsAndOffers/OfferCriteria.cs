 
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class OfferCriteria
    {

        public int offer_id { get; set; } = 0;
        public int order_id { get; set; } = 0;
        public string accessToken { get; set; } ="";
        public List<Domain.Entities.Filter.Filter> filters;
        public Domain.Entities.Filter.Sort sort;
        public int sortType { get; set; }
        public double lat { get; set; } = 0;
        public double lng { get; set; } = 0;

        public double fromPrice { get; set; } = 0;
        public double toPrice { get; set; } = 0;
        public double rate { get; set; } = 0;
        public int modelID { get; set; } = 0;
        public int orderType { get; set; } = 0;
        public int offerType { get; set; } = 0;
    }

}

