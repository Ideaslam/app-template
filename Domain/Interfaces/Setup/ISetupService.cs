using System;
using System.Collections.Generic;
using System.Text;
using HelperClass;

namespace Domain.Interfaces.Setup
{
   public interface ISetupService 
    {
        Response GetAllSetups();
        Response GetAllTables();
        Response GetAllMasters();
        Response GetAllReferenceByMasterName(string MasterTable);
        Response GetAllColumnsByTableName(string TableName); 
            
           
    }
}
