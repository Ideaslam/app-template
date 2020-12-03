 
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;

namespace Repository.HelperRepository
{
   public class HelperRepository : Domain.Interfaces.Helper.IHelperRepository
    {
        public string convertDateFormat(string datetime ,string format ,string ToFormat)
        {
            try
            {
                // Change Format Of date 
                DateTime date = DateTime.ParseExact(datetime, format, CultureInfo.InvariantCulture);
               return date.ToString(ToFormat);
            }
            catch (Exception ex )
            {
                return null;
            }
        }


        public string generateIds(int creationCode, DateTime dateTime )
        {
            try
            {
                string code = new Random().Next(10,99).ToString() + creationCode.ToString() + dateTime.ToString("ss") + dateTime.ToString("yy")+
                    dateTime.ToString("mm") + dateTime.ToString("MM") +  dateTime.ToString("HH") + dateTime.ToString("dd")+ new Random().Next(10, 99).ToString();
                return code;  
            }
            catch (Exception ex)
            {
                return null;
            }
        }


      
    }
}
