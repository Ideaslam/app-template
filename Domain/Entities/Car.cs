using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class Car
    {
        public Vehicle Vehicle = new Vehicle();
        public Accident accident = new Accident();
        public List<Image> listImage = new List<Image>();
        
    }
}
