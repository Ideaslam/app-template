using Microsoft.Extensions.Configuration;
 
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
 
namespace DbQueries
{
    public class Common
    {



        public string ErrorString = "";
        OracleConnection Conn;


        public static Int64 gi = 0;


        public Int64 GI
        {
            get { return gi; }
            set { gi = value; }
        }
        public string C_Errorstring
        {
            get { return ErrorString; }
            set { ErrorString = value; }

        }

        ////////////////////



        public string ProjectNameEn { get; set; }
        public string ProjectNameAr { get; set; }
        public string DeveloperEn { get; set; }
        public string DeveloperAr { get; set; }



        public string eventname { get; set; }
        public string ErrorLine { get; set; }
        public string ExecuteQuery { get; set; }


        public Common()
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("islah").ToString();
            Conn = new OracleConnection(connectionString);


        }




        public DataTable ReadTable(String SELSTR)
        {
            DataTable DT = new DataTable();

            //SELSTR = " select * From city ";  testing

            SELSTR = SELSTR.Replace("&amp;", "&");
            Conn.Close();
            Conn.Open();
            OracleDataAdapter DA = new OracleDataAdapter(SELSTR, Conn);

            DA.Fill(DT);

            Conn.Close();

            return DT;
        }


        public DataTable ReadTableOracle(OracleCommand command)
        {
            DataTable DT = new DataTable();


            command.Connection = Conn;
            Conn.Close();
            Conn.Open();
            OracleDataAdapter DA = new OracleDataAdapter(command);

            DA.Fill(DT);

            Conn.Close();

            return DT;
        }


        public string ExcuteSqlOracle(OracleCommand command)
        {

            string ErrorString = "";
            command.Connection = Conn;
            OracleTransaction tr = null;

            try
            {
                Conn.Close();
                Conn.Open();

                tr = Conn.BeginTransaction();
                command.CommandTimeout = 800;
                command.Transaction = tr;
                command.ExecuteNonQuery();
                command.Transaction.Commit();

                Conn.Close();
            }
            catch (Exception ex)
            {
                ErrorString = ErrorString + "ExecuteSql : " + ex.Message.Replace("\n", "");
                tr.Rollback();
                throw ex;
            }
            finally
            {
                Conn.Close();
            }
            return ErrorString;
        }


        public string ExecuteSql(string SELSTR)
        {
            SELSTR = SELSTR.Replace("\n", "");
            ErrorString = "";
            OracleTransaction tr = null;

            try
            {
                Conn.Close();
                Conn.Open();

                tr = Conn.BeginTransaction();

                OracleCommand rs = new OracleCommand(SELSTR, Conn);
                rs.CommandTimeout = 800;


                rs.Transaction = tr;
                rs.ExecuteNonQuery();
                rs.Transaction.Commit();

            }
            catch (Exception ex)
            {
                ErrorString = ErrorString + "ExecuteSql : " + ex.Message.Replace("\n", "");
                tr.Rollback();
                throw ex;

            }

            finally
            {
                Conn.Close();
            }

            return ErrorString;
        }







    }


}
