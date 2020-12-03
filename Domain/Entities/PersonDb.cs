using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class PersonDb 
    {
        public int id { get; set; } = 0;
        public int user_id { get; set; } = 0;
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        //  public int IqammaNumber { get; set;  }
        // public double rating { get; set; }
        // public string accessToken { get; set; }

    }
}
