using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Services
{
  public  class serviceRequest
    {
          
        public int id { get; set; } = 0;
        public int SERVICE_ID { get; set; } = 0;
        public int USER_ID { get; set; } = 0;
        public string REQUEST_DATETIME { get; set; } = "2019-01-01";
        public int ACTIVE { get; set; } = 0;
        public int FINISH { get; set; } = 0;
        public int RATE { get; set; } = 0;
        public double SRCX { get; set; } = 0;
        public double SRCY { get; set; } = 0;
        public double DESTX { get; set; } = 0;
        public double DESTY { get; set; } = 0;
        public string accessToken { get; set; } = "";

    }
}
