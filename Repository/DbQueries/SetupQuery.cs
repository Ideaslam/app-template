using System;
using System.Collections.Generic;
using System.Text;

namespace DbQueries
{
    class SetupQuery : BaseQuery
    {

        
       

        public string GetAllTables(string owner)
        {
            string Query = "select * from all_tables where owner = '" + owner + "' ";
            return Query;
        }

        public string GetAllReferenceByMater(string owner)
        {
            string Query = "select c.owner , a.table_name child_Table, a.column_name FK_column, c_pk.table_name master_table, col_pk.column_name PK_column" +
                            "from all_cons_columns a" +
                            "join all_constraints c on a.owner = c.owner and  a.constraint_name = c.constraint_name" +
                            "join all_constraints c_pk on c.owner = c_pk.owner and c.r_constraint_name = c_pk.constraint_name" +
                            "join all_cons_columns col_pk on c_pk.constraint_name = col_pk.constraint_name" +
                            "where c.constraint_type = 'R'" +
                            "and c.owner = '"+ owner + "'";
            return Query;
        }

        public string GetAppVersion(int typePhone)
        {
            int id = 0;
            if (typePhone == 1)
                id = 11;
            else
                id = 22; 


            string Query = "select * from setup_table where id ="+id; 
            return Query;
        }

      

        public string GetAppUrl()
        {
            string Query = "select * from setup_table where ID_NAME in ('URL_ANDROID', 'URL_IOS', 'HELP_LINK', 'ABOUTUS_LINK', 'CONTACT_LINK' ,'ISLAH_ANDROID', 'ISLAH_IOS')";
            return Query;
        }


        public string GetColumnsByTable(string tableName , string owner)
        {
            string Query = "select from ALL_TAB_COLUMNS where table_name =upper('"+ tableName + "') and owner =upper('"+ owner + "') ";
            return Query;
        }

    }
}
