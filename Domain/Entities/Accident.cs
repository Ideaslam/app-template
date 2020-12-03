using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Accident
    {
          public int accident_id { get; set; }
        public int vehicle_id { get; set; }
        public int damageLevel { get; set; }
        public int fixType { get; set; }
        public double locationX { get; set; }
        public double locationY { get; set; }
        public bool readyToFix { get; set; }
        public bool publicDamages { get; set; }
        public bool anotherVictim { get; set; }
        public DateTime accidentDate { get; set; }
        public int fixPaperId { get; set; }
        public string accessToken { get; set; }



                
    }
}
