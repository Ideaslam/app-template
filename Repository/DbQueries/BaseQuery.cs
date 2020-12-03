using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Reflection;
using System.Text;


namespace DbQueries
{
   public class BaseQuery : Domain.Interfaces.IRepository
    {
        private string query { get; set; }
        private string columns  { get; set; }
        private string where { get; set; }
        private string input { get; set; }
        private string join { get; set; }
        private string orderBy { get; set; }
        private string groupBy { get; set; }

        //----------------------Basic Query---------------------------
        public BaseQuery()
        {
            this.query = "";
            this.columns = "";
            this.where = "";
            this.input = "";
            this.join = "";
            this.orderBy = "";
            this.groupBy = "";
        }


        //Get All Data Query
        public string GetAllObjects(string viewName)
        {
            string Query = "select * from " + viewName;
            return Query;
        }



        //Get Data By  Query
        public string GetObjectByColname<T>(string viewName, string condCol, T value)
        {
            string Query = "select * from "+ viewName + " where " + condCol + " = '" + value + "'   ";
            return Query;
        }

        public string DeleteObjectByColname<T>(string viewName, string colname, T value)
        {
            string Query = " delete from " + viewName + " where "+ colname + " ='" + value + "'";
            return Query;
        }

        public string GetMasterTranslated(string table_name ,string lang)
        {
            string Query = "select * from "+ table_name + "  main " +
                "join "+ table_name + "_translation    on  main.id = " + table_name + "_translation."+ table_name + "_non_trans_id " +
                "join language lang on "+ table_name + "_translation.lang_id    = lang.language_id " +
                "where main.isactive = 1 and lang.name ='" + lang + "'  order by main.id asc";
            return Query;
        }


        public string GetAllObjectsByColName(string viewName ,string colname)
        {
            string Query = "select "+colname+" from " + viewName ;
            return Query;
        }

        public string GetObjectsByColName(string viewName, string colname , string cond ,string condValue)
        {
            string Query = "select " + colname + " from " + viewName +" where  "+cond+" = '"+condValue+"' ";
            return Query;
        }


        //public string GetDataByOffset(string Query , int offset , int limit )
        //{
        //    string Query = "select   ";
        //    return Query;
        //}

        //----------------------------------------------------------------------
        public void AddColumn(string variable)
    {
            if (columns == "")
            {
                columns +=  variable ;
            }
            else
            {
                columns += " , " + variable ;
            }
    }
        public void AddCondition(string column  ,string value )
        {
            if (where == "")
            {
                where += column +" = '"+ value + "'";
            }
            else
            {
                where += " , "+column + " = '" + value + "'";
            }
        }
        public void AddJoin(string firstTableName, string secondTableName)
        {
            //if (where == "")
            //{
            //    where += column + " = '" + value + "'";
            //}
            //else
            //{
            //    where += " , " + column + " = '" + value + "'";
            //}
        }
        //----------------------------------------------------------------------
        public void AddOrder(string firstTableName, string secondTableName)
        {
            throw new NotImplementedException();
        }
        public void AddGroup(string firstTableName, string secondTableName)
        {
            throw new NotImplementedException();
        }

     
    }
}
