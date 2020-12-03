using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Order
{
    public class accidentDetails
    {       

        public int    ACCIDENT_ID { get; set; }
        public string FULLNAME { get; set; } = "";
        public string ACCIDENTDATE { get; set; }
        public string PAPER_NO { get; set; }
        public string PLATENUMBER  { get; set; }
        
        public string MANUFACTURER { get; set; }
        public string MODEL { get; set; }
        public string COLOR { get; set; }
        public string FOUNDDATE { get; set; }
        public string FIXTYPENAME_EN { get; set; }
        public string FIXTYPENAME_AR { get; set; }
        public double LOCATIONX_Accident { get; set; }
        public double LOCATIONY_Accident { get; set; }


        




    }
}
