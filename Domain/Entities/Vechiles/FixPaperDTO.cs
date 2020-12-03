using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Vechiles
{
    public class FixPaperDTO
    {
        public int id { get; set; }
        public string paper_id { get; set; }
        public string issueDate { get; set; }
        public string expiryDate { get; set; }
        public int status { get; set; }
        public string car_plateNumber  { get; set; }
       

        public FixPaperDTO()
        {
            id = 0;
            paper_id = "";
            issueDate = DateTime.Now.ToString("dd-MM-yyyy");
            expiryDate = DateTime.Now.ToString("dd-MM-yyyy");
            status = 0;
            car_plateNumber = "";
             
        }


    }
}
