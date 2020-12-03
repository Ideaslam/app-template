using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Services
{
    public class Services
    {

        public int id { get; set; } = 0;
        public string SERVICENAME_AR { get; set; } = "";
        public string SERVICENAME_EN { get; set; } = "";
        public double SERVICEPRICE { get; set; } = 0;
        public int SERVICETYPE_ID { get; set; } = 0;
        public int SERVICETIME { get; set; } = 0;
        public string IMAGE { get; set; } = "";

    }
}
