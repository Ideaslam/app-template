using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
   public class InfoWindow
    {

        public int List_id { get; set; } = 0;
        public string Listname { get; set; } = "";
        public List<orderListDetail> contents { get; set; }
    }
}
