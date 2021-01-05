using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Common
{
    public static class Globals
    {
        public static TwilioConfig TwilioConfig { get; set; } 
        public static Env Environment { get; set; }


    }




    public class Env
    {
        public static bool Production { get; set; }

    }
    public class TwilioConfig
    {
        ///<summary>
        ///Gets or Sets the Admin sdk file name
        /// </summary>            
        /// 
        public static string AccountSid { get; set; }
        public static string AuthToken { get; set; }
        public static string ServiceSid { get; set; }
        public static string PhoneNumber { get; set; }
        public static string invoiceTemp { get; set; }
    }

}
