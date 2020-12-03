using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Needs
    {
        public int id  {get;set; } = 0;
        public List<control> controls   {get;set;}
        public List<InfoWindow>  info { get; set; }
    }
}
