using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.supplier
{
   public class WarshaRequestDTO
    {
  
        public int ACCIDENT_ID { get; set;  }
        public string ACCIDENTDATE { get; set; }
        public string PLATENUMBER  { get; set; }
        public string MANUFACTURER { get; set; }
        public string MODEL { get; set; }
        public string FOUNDDATE { get; set; }
        public string COLOR { get; set; }
        public string paper_NO { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public string FULLNAME { get; set; }
        public string Image { get; set; }
        public int Status { get; set; }
        public string statusNameEn { get; set; }
        public string statusNameAr { get; set; }




    }
}
