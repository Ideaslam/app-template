using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Order
{
  public  class OrderType
    {
        public int id { get; set; } = 0;
        public string OrderTypeName { get; set; } = "";
        public string OrderTypeIcon { get; set; } = "";
        public  control controls { get; set; }

    }
}
