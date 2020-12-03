using DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.DbQueries
{
    class StatQuery : BaseQuery 
    {

        public string GetWorkshopBills(int workshop_id )
        {
            string Query = " select * from bills where workshop_id =" + workshop_id;
            return Query;

        }

    }
}
