using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Login
{
   public class UserDevice
    {
        public string device_id { get; set; }
        public int phoneType { get; set; }
        public string accessToken { get; set; }
    }
}
