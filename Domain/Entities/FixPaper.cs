using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
  public   class FixPaper
    {

      public int id { get; set; }
      public string   paper_id { get; set; }
      public DateTime issueDate { get; set; }
      public DateTime expiryDate { get; set; }
      public int status { get; set; }
      public string   car_plateNumber  { get; set; }
        public string accessToken { get; set; }
    }
}
