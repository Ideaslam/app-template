using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Vechiles
{
   public class VehicleDTO
    {
        public int vechile_id { get; set; }
        public string BrandLogo { get; set; }
        public string modelName { get; set; }
        public string brandName { get; set; }
        public string year { get; set; }
        public string color { get; set; }

    }
}
