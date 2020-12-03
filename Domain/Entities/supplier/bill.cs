using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.supplier
{
  public  class Bill
    {

        public int OFFER_ID { get; set; }
        public int WORKSHOP_ID { get; set; }
        public string PLATENUMBER  { get; set; }
       
        public double PRICE { get; set; }
        public int WORKDAYS { get; set; }
        public int ACTUALWORKDAYS { get; set; }
        public double PAYMENT { get; set; }
    }
}
