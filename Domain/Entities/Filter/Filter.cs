using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Filter
{
    public class Filter
    {
        public string column_name { get; set; }
        public string fromValue { get; set; }
        public string toValue { get; set; }




        public string generateCondition(List<Filter> filters ,string tableAlias)
        {
            string condition = ""; 

            foreach(Filter filter in filters)
            {
                condition += " and "+ tableAlias+"." + filter.column_name + " >= " + filter.fromValue + "  and  " + tableAlias + "." + filter.column_name + "  <=  " + filter.toValue+" ";
            }
            return condition;
        }
    }
}
