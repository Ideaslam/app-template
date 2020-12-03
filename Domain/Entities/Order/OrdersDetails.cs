using Domain.Entities.Offers;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Order
{
   public class OrdersDetails
    {

        public UserRequestCriteria order { get; set;  }
        public OfferDTO  offer { get; set; }
        public List<string> media { get; set; }
    }
}

