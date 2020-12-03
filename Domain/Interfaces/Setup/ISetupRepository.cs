using System;
using System.Collections.Generic;
using System.Text;


namespace Domain.Interfaces.Setup
{
   public interface   ISetupRepository
    {
       
        Domain.Entities.Setup.Setup  GetAllSetups();
        List<string> GetAllTables();
        List<string> GetAllMasters();
        List<string> GetAllReferenceByMasterName(string MasterTable);
        List<string> GetAllColumnsOfTableName(string TableName);
        

    }
}
 