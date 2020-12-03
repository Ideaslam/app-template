using Domain.Entities;
using Domain.Entities.Vechiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Vehicle
{
  public  interface IVehicleRepository
    {
       bool  InsertVehicleData(Domain.Entities.Vehicle vehicle ,string user_id );
       bool  InsertFixPaper(FixPaper fixPaper);
       List<FixPaperDTO> GetFixPaper(int user_id);
      // List <Domain.Entities.Vechiles.VehicleDTO> GetCarBasicAndAccident(int user_id); 
       

    }
}
