using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class BrandDTO
    {
        public List<Brand> brands { get; set; }
        public List<Model> models { get; set; }
    }
}
