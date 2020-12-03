using Domain.Entities.Order;
using Domain.Entities.Offers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class OrderDatails
    {
        public List<string> car_images { get; set; } = new List<string>();
        public object accidentDetails { get; set; }
        public object offerDetails { get; set; }


            public OrderDatails() {
            car_images = new List<string>();

             }

    }
}
