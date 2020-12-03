using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using HelperClass;

namespace Domain.Interfaces.Vehicle
{
   public interface IVehicleService 
    {
         Response StoreVehicleData(Domain.Entities.Vehicle vehicle ,string user_id);
         Response StoreFixPaper(FixPaper fixPaper);
         Response GetFixPaper(string accessToken);
     //    Response GetCarInformations(string accessToken);

    }
}
