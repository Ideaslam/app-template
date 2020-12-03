using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Helper
{
   public interface IHelperRepository
    {

        string convertDateFormat(string date ,string format ,string ToFormat);
      
    }
}
