using System;
using System.Collections.Generic;
using System.Text;
using HelperClass;
using DbQueries;
using Domain.Entities.Setup;
using Domain.Entities;
using Domain.Messages;

namespace SetupRepository
{
   public class SetupRepository : Domain.Interfaces.Setup.ISetupRepository
        
    {

        string language = "en";

        public SetupRepository(string language)
        {
            this.language = language;

        }

        Common conn_db = new Common();

        public List<string> GetAllColumnsOfTableName(string TableName)
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllMasters()
        {
            throw new NotImplementedException();
        }

        public List<string> GetAllReferenceByMasterName(string MasterTable)
        {
            SetupQuery setupQuery = new SetupQuery();
            System.Data.DataTable dataTable = conn_db.ReadTable(setupQuery.GetAllReferenceByMater("islah"));
            List<string> tablesNames = new List<string>();

            foreach (System.Data.DataRow dataRow in dataTable.Rows)
            {
                tablesNames.Add(dataRow["table_name"].ToString());
            }
            return tablesNames;
        }

        public AppInfo GetAppInfo(int typePhone)
        {
            SetupQuery setupQuery = new SetupQuery();
            System.Data.DataTable version;
            System.Data.DataTable Url;
           
               // version = conn_db.ReadTable(setupQuery.GetAppVersion(typePhone));
                Url = conn_db.ReadTable(setupQuery.GetAppUrl());
           

            AppInfo appInfo = new AppInfo();

           if(Url.Rows.Count > 0)
            {
                foreach(System.Data.DataRow row in Url.Rows)
                {

                    if (typePhone == 0)
                    {
                        if (row["ID_NAME"].ToString() == "ISLAH_ANDROID")
                        {
                            appInfo.appName_Ar = row["NAME_AR"].ToString();
                            appInfo.appName_En = row["NAME_EN"].ToString(); appInfo.appName_En = row["NAME_EN"].ToString();
                            appInfo.version = row["QTY"].ToString();

                        }
                    }
                    else
                    {
                        if (row["ID_NAME"].ToString() == "ISLAH_IOS")
                        { 
                                appInfo.appName_Ar = row["NAME_AR"].ToString();
                                appInfo.appName_En = row["NAME_EN"].ToString();
                                appInfo.version = row["QTY"].ToString();
                        }
                    }
                        
                    


                   if (row["ID_NAME"].ToString() == "URL_ANDROID")
                    {
                        appInfo.app_url = row["QTY"].ToString();
                    }
                    else if (row["ID_NAME"].ToString() == "URL_IOS")
                    {
                        appInfo.app_url = row["QTY"].ToString();
                    }
                    else if (row["ID_NAME"].ToString() == "HELP_LINK")
                    {
                        appInfo.help_url = row["QTY"].ToString();
                    }
                    else if (row["ID_NAME"].ToString() == "ABOUTUS_LINK")
                    {
                        appInfo.aboutus_url = row["QTY"].ToString();
                    }
                    else if (row["ID_NAME"].ToString() == "CONTACT_LINK")
                    {
                        appInfo.contact_url = row["QTY"].ToString();
                    }


                }

            }

             
            return appInfo;
        }

        public Setup GetAllSetups()
        {
            SetupQuery setupQuery = new SetupQuery();
            System.Data.DataTable dataTable = conn_db.ReadTable(setupQuery.GetAllObjects("setup_table"));
            Setup setup = new Setup();
            if (dataTable.Rows.Count > 0)
            {
                foreach(System.Data.DataRow row in dataTable.Rows)
                {
                    if (row["ID_NAME"].ToString() == "TOKEN_LENGTH")
                        setup.accessTokenLength =Convert.ToInt32( row["QTY"].ToString());

                    if (row["ID_NAME"].ToString() == "PUBLIC_IP")
                        setup.publicIp = row["QTY"].ToString();
                }
            }
            return setup;
        }

        public List<string> GetAllTables()
        {
            SetupQuery setupQuery = new SetupQuery();
            System.Data.DataTable dataTable = conn_db.ReadTable(setupQuery.GetAllTables("islah"));
            List<string> tablesNames = new List<string>();

            foreach(System.Data.DataRow dataRow in dataTable.Rows)
            {
                tablesNames.Add(dataRow["table_name"].ToString());
            }
            return tablesNames;

        }

      

    }
}
