using Domain.Entities.supplier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Offer
{
    public class OffersDTO
    {
        public int offerId { get; set; } = 0;
        public double offerPrice { get; set; } = 0;
        public int workingDays { get; set; } = 0;
        public double distance { get; set; } = 0;
        public int evaluate_flag { get; set; } = 0;
        public string evaluate_Ar { get; set; } = "";
        public string evaluate_En { get; set; } = "";
        public WorkshopObjDTO  workshopObjDTO { get;set;}
    }
}
