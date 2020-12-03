using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Vechiles
{
    public class CarShort
    {
        public string plateNumber { get; set; } = "";
        public int accident_id { get; set; } = 0;
        public double price { get; set; } = 0;
    }
}
