using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Services
{
   public class BillFix
    {

        public int id { get; set; } = 0;
        public int accident_id { get; set; } = 0;
        public int workshop_id { get; set; } = 0;
        public double  fix_price { get; set; } = 0;
        public double spare_price { get; set; } = 0;
        public double TRANSCAR_PRICE { get; set; } = 0;
        public double rent_price { get; set; } = 0;
        public string note { get; set; } = "";
        public string accessToken { get; set; } = "";
    }
}
