using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Services
{
    public class Notification
    {
        public string to { get; set;  }
        public string title { get; set; }
        public string body { get; set; }
        public int badge { get; set; }
        public string  accessToken { get; set; }

    }
}
