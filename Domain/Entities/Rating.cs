using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class Rating
    {
        public int raterId { get; set; } = 0;
        public int ratedId { get; set; } = 0;
        public int starNo { get; set; } = 0;
        public double ratingRatio { get; set; } = 0;
        public int workshop_id { get; set; } = 0;
        public string accessToken { get; set; } = "";
    }
}
