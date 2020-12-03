using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class RequestCriteria
    {
        public int accident_id { get; set; }
        public List<int> listArea { get; set; }
        public int city_id { get; set; }
        public string accessToken { get; set; }
    }
}
