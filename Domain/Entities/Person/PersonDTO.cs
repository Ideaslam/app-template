using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Person
{
   public class PersonDTO
    {
        public int personId { get; set; } = 0;
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public double rating { get; set; } = 0;
        //public int IqammaNumber { get; set; }
        public string email { get; set; } = "";
    }
}
