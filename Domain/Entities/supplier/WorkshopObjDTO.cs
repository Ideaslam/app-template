using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.supplier
{
   public class WorkshopObjDTO
    {
        public int workshopId { get; set; }
        public string shopName { get; set; }
        public string RecordNumber { get; set; }
        public string image { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public double rating  { get; set; }
        public string areaName { get; set; }
        public string phonenumber { get; set; }
    }
}
