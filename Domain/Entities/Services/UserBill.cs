using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Services
{
   public class UserBill
    {


        public int id { get; set; }
        public string PLATENUMBER { get; set; }
        public string ACCIDENTDATE { get; set; }
        public int USER_ID { get; set; }
        public int workshop_id { get; set; }
        public double FIX_PRICE { get; set; }
        public double SPARE_PRICE { get; set; }
        public double RENT_PRICE { get; set; }
        public double TRANSCAR_PRICE { get; set; }
        public string note { get; set; }

    }
}
