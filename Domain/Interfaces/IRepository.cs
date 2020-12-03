using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces
{
   public interface IRepository
    {
         string GetAllObjects(string viewName);
         string GetObjectByColname<T>(string viewName, string colname, T value);
         string GetAllObjectsByColName(string viewName , string colname);

         void AddColumn(string variable);
         void AddCondition(string column, string value);
         void AddJoin(string firstTableName, string secondTableName);
         void AddOrder(string firstTableName, string secondTableName);
         void AddGroup(string firstTableName, string secondTableName);
    }
}
