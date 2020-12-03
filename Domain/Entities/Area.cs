using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
     public class Area
    {

        public  int AreaId { get; set; }
        public  string AreaName { get; set;  }
        public  int CityId { get; set; }
        public  string CityName { get; set; }

    }
}
