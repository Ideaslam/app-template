using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Vechiles
{
    public class ResVechileDb
    {

        public int carId { get; set; }
        public int accidentId { get; set; }

        public string fixPaperId { get; set; }
        public int fixPaperStatus { get; set; }


        public string plateNumber  { get; set; }
      
        public string manufacturer { get; set; }
        public string model { get; set; }
        public string color { get; set; }
        //  public int registrationType { get; set; }
        public string foundDate { get; set; }

    }
}
