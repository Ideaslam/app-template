using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
  public  class AppInfo
    {
        public string appName_En { get; set; } = "";
        public string  appName_Ar { get; set; } = "";
        public string  version { get; set; } = "";
        public string  app_url { get; set; } = "";
        public string help_url { get; set; } = "";
        public string aboutus_url { get; set; } = "";
        public string contact_url { get; set; } = "";
    }
}
