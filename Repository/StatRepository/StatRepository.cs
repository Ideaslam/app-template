using DbQueries;
using Domain.Entities.supplier;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Stat;
using Repository.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.StatRepository
{
   public class StatRepository : IStatRepository 
    {
        Common conn_db = new Common();

        string language = "en";

        public StatRepository(string language)
        {
            this.language = language;
        }


        public List<Bill> GetWorkshopBills(int workshop_id)
        {


            StatQuery statQuery = new StatQuery();
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = conn_db.ReadTable(statQuery.GetWorkshopBills(workshop_id));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            Bill bill = new Bill();
            List<Bill> bills = new List<Bill>();

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                bill=  new Bill();
                bill.OFFER_ID = Convert.ToInt32(row["OFFER_ID"] );
                bill.WORKSHOP_ID = Convert.ToInt32(row["WORKSHOP_ID"] );
                bill.PLATENUMBER = row["PLATENUMBER"].ToString();
              
                bill.PRICE  = Convert.ToDouble(row["PRICE"]);
                bill.WORKDAYS = Convert.ToInt32(row["WORKDAYS"]);
                bill.ACTUALWORKDAYS = Convert.ToInt32(row["ACTUALWORKDAYS"]);
                bill.PAYMENT = Convert.ToDouble(row["PAYMENT"]);



                bills.Add(bill);
            }
            return bills;

        }
    }
}
