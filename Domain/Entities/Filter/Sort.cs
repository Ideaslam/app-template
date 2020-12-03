using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.Filter
{
    public class Sort
    {
        public string column_name { get; set; }
        public string SortOrder { get; set; }

        enum sortOfferColumn
        {
            dateAsc=1 ,
            dateDesc = 2,
            priceAsc = 3,
            priceDesc = 4
        }



        public string generateSort(Sort sort , string tableAlias)
        {
            string orderby = "";

            orderby += " order by "+ tableAlias+ "." + sort.column_name + "  " + sort.SortOrder; 


            return orderby;
        }


        public string getSortOffer(int  sortId, string tableAlias)
        {
            string orderby = "";
            string colName = "";
            string orderType = ""; 

            if(sortId == (int) sortOfferColumn.dateAsc)
            {

                colName = " OFFER_DATETIME ";
                orderType = " asc ";
            }
            else if (sortId == (int)sortOfferColumn.dateDesc)
            {
                colName = " OFFER_DATETIME ";
                orderType = " desc ";
            }
            else if (sortId == (int)sortOfferColumn.priceAsc)
            {
                colName = " price ";
                orderType = " asc ";
            }
            else
                 if (sortId == (int)sortOfferColumn.priceDesc)
            {
                colName = " price ";
                orderType = " desc ";
            }
            else
            {
                return "";
            }


            orderby += " order by " + tableAlias + "." + colName + "  " + orderType;


            return orderby;
        }
    }
}
