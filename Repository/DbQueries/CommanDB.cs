using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;



namespace DbQueries
{
    public class CommanDB
    {

        OracleConnection con;

        public CommanDB( )
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("appsettings.json", optional: false);
            var configuration = builder.Build();
            string connectionString = configuration.GetConnectionString("islah").ToString();
            con = new OracleConnection(connectionString);


        }


      


        // //// Bind Parameters Comllection to Command ///////

        public OracleCommand QueryBuilder(String ProcName, OracleParameter[] param)
        {
            //OracleParameter prm;
            OracleCommand cmd = new OracleCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandTimeout = 0;
            cmd.Connection = con;
            cmd.CommandText = ProcName;

            foreach (OracleParameter p in param)
            {
                cmd.Parameters.Add(p);
            }
            return cmd;
        }
        public OracleCommand InCmdBuilder(String ProcName, OracleParameter[] param)
        {
            OracleCommand command = QueryBuilder(ProcName, param);
            //OracleParameter prm = new OracleParameter();
            //prm.ParameterName = "ReturnValue";
            //prm.OracleDbType = OracleDbType.Int32;
            //prm.Size = 4;
            //prm.Scale = 0;
            //prm.Precision = 0;
            //prm.IsNullable = false;
            //prm.Direction = ParameterDirection.ReturnValue;
            //prm.SourceColumn = string.Empty;
            //prm.Value = null;
            //prm.SourceVersion = DataRowVersion.Default;
            //command.Parameters.Add(prm);
            //return command;



            return command;
        }

        public void RunProc(String ProcName, OracleParameter[] param, int rows)
        {
            int rowaf;
            con.Open();

            OracleCommand cmd = new OracleCommand();
            cmd = InCmdBuilder(ProcName, param);
            cmd.CommandTimeout = 0;
            rowaf = cmd.ExecuteNonQuery();

            con.Close();
        }
        public int RunProc(String ProcName, OracleParameter[] param)
        {
            int rowaf;
            con.Open();

            OracleCommand cmd = InCmdBuilder(ProcName, param);
            cmd.CommandTimeout = 0;
            rowaf = cmd.ExecuteNonQuery();
            con.Close();
            return rowaf;
        }


        public DataSet RunProcDS2(String ProcName, OracleParameter[] param)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            DataSet ds = new DataSet();
            con.Close();
            con.Open();
            da = new OracleDataAdapter("", con);
            cmd = QueryBuilder(ProcName, param);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            cmd.Connection = con;
            cmd.CommandTimeout = 0;

            //  da.SelectCommand = cmd;
            //  da.Fill(ds);
            cmd.ExecuteNonQuery();
            string bestCAP = string.Empty;
            bestCAP = cmd.Parameters["@FPID_v"].Value.ToString();
            con.Close();
            return ds;
        }

        public DataSet RunProcDS(String ProcName, OracleParameter[] param)
        {
            OracleDataAdapter da = new OracleDataAdapter();
            OracleCommand cmd = new OracleCommand();
            DataSet ds = new DataSet();
            con.Close();
            da = new OracleDataAdapter("", con);
            cmd = QueryBuilder(ProcName, param);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            cmd.Connection = con;
            cmd.CommandTimeout = 0;

            da.SelectCommand = cmd;
            da.Fill(ds);
            con.Close();
            return ds;
        }

        //public DataSet RunProcDS(SqlDataSource datas ,String ProcName, OracleParameter[] param)
        //{
        //    OracleDataAdapter da = new OracleDataAdapter();
        //    OracleCommand cmd = new OracleCommand();
        //    DataSet ds = new DataSet();
        //    con.Close();
        //    da = new OracleDataAdapter("", con);
        //    cmd = QueryBuilder(ProcName, param);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.CommandText = ProcName;
        //    cmd.Connection = con;
        //    cmd.CommandTimeout = 0;

        //    datas.SelectCommand = cmd;

        //    con.Close();
        //    return ds;
        //}
        public DataSet RunProcDS(String ProcName)
        {
            OracleDataAdapter da;
            OracleCommand cmd;
            DataSet ds = new DataSet();

            cmd = new OracleCommand();
            da = new OracleDataAdapter("", con);

            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = ProcName;
            cmd.Connection = con;
            cmd.CommandTimeout = 0;

            da.SelectCommand = cmd;
            da.Fill(ds);
            con.Close();
            return ds;
        }

        public string NumWord(double Num)
        {
            DataSet ds = new DataSet();
            OracleParameter[] param = { new OracleParameter("@pnumber", OracleDbType.Double) };
            param[0].Value = Num;
            ds = RunProcDS("GB_NumberToWords", param);
            return " " + ds.Tables[0].Rows[0][0].ToString() + " Only.";
        }


    }
}