using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.supplier
{
   public class Stats
    {
        public int UNCONFIRMED { get; set; } = 0;
        public int CONFIRMED { get; set; } = 0;
        public int STARTED { get; set; } = 0;
        public int FINISHED { get; set; } = 0;

    }
}
