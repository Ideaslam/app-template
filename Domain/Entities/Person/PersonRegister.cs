using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Person
{
   public class PersonRegister
    {
        public string firstName { get; set; } = "";
        public string lastName { get; set; } = "";
        public string phoneNumber { get; set; } = "";
        public int country_code { get; set; } = 0;

    }
}
