using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Order
{
   public class CarInfoOrder
    {





        public int? carId { get; set; } = 0;
        public string OrderIdentity { get; set; } = "";
        public int? orderType { get; set; } = 0;
        public double locationX { get; set; } = 0;
        public double locationY { get; set; } = 0;
        public int orderisActive { get; set; } = 0;
        public int? cityId { get; set; } = 0;
        public int order_id { get; set; } = 0;
        public List<string> images { get; set; }
        public List<int> orderDetailsIds { get; set; }
        public string note { get; set; } = "";
        public string accessToken { get; set; } = "";




    }
}
