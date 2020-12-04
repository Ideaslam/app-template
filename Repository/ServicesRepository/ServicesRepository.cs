using DbQueries;
using Domain.Exceptions;
using Domain.Entities.Services;
using Domain.Interfaces.Services;
using Repository.DbQueries;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using HelperClass;

namespace Repository.ServicesRepository
{
  public  class ServicesRepository : IServiceRepository
    {
        Common conn_db = new Common();

        string language = "en"; 
        public ServicesRepository(string language )
        {
            this.language = language;
        }


        public bool InsertService(serviceRequest serviceRequest , int user_id )
        {
             
            return new ServicesQuery(language).InsertService(serviceRequest, user_id);
        }

        public bool InsertBill(BillFix billFix  )
        {
            
            return new ServicesQuery(language).InsertBill(billFix);
        }


        public  string   ComparePassword(string oldPassword , int user_id )
        {
            ServicesQuery serviceQuery = new ServicesQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(serviceQuery.ComparePassword(oldPassword , user_id));
            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            return  dataTable.Rows[0]["Password"].ToString();
        }

        public bool ChangePassword(string newPassword , int user_id)
        {
            
            return new ServicesQuery(language).ChangePassword(newPassword, user_id);
        }


        public bool ChangePhoneNumber(string phoneNumber, int  user_id)
        {
           
            return new ServicesQuery(language).ChangePhoneNumber(phoneNumber, user_id);
        }



        public bool CancelServiceRequest(int service_id )
        {
           
            return new ServicesQuery(language).CancelServiceRequest(service_id );
        }

        public bool FinishServiceRequest(int service_id, int rating)
        {
           
            return new ServicesQuery(language).FinishServiceRequest(service_id, rating);
        }



        public string getTime(string org , string dest)
        {
            string origin = org;
            string destination = dest;

            string distance = "";
            string time = "";
            string url = "";
            
            if (language=="ar")
              url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&language=ar-AR&key=AIzaSyDbpc1_Uh86TEweWA4lom5-a5ugWVXW3gE";
            else
                url = "https://maps.googleapis.com/maps/api/distancematrix/xml?origins=" + origin + "&destinations=" + destination + "&key=AIzaSyDbpc1_Uh86TEweWA4lom5-a5ugWVXW3gE";

            WebRequest request = WebRequest.Create(url);
            using (WebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    DataSet dsResult = new DataSet();
                    dsResult.ReadXml(reader);

                 
                    if (dsResult.Tables["duration"] == null)
                        time = "0";
                    else
                        time = dsResult.Tables["duration"].Rows[0]["text"].ToString();
                    //distance = dsResult.Tables["distance"].Rows[0]["text"].ToString();
                    return time;


                }
            }
        }



        public List<ServiceTypes> GetServiceTypes()
        {
            ServicesQuery serviceQuery = new ServicesQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(serviceQuery.GetMasterTranslated("servicetypes",language));
            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            List<ServiceTypes> listServiceTypes = new List<ServiceTypes>();
           
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                ServiceTypes serviceTypes = new ServiceTypes();

                serviceTypes.serviceTypeId = Convert.ToInt32(row["ID"].ToString());
                serviceTypes.serviceType_Name  =  row["SERVICETYPES_NAME"].ToString() ;         
                serviceTypes.serviceType_icon =   row["serviceType_icon"].ToString();

                listServiceTypes.Add(serviceTypes);
            }
            return listServiceTypes;
        }


        public List<UserBill> GetUserBills(int user_id)
        {
            ServicesQuery serviceQuery = new ServicesQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(serviceQuery.GetUserBill(user_id));
            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            List<UserBill> UserBills = new List<UserBill>();

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                UserBill userBill = new UserBill();
                         
                userBill.id = Convert.ToInt32(row["ID"].ToString());
                userBill.PLATENUMBER =  row["PLATENUMBER"].ToString() ;
                userBill.ACCIDENTDATE = Convert.ToDateTime(row["ACCIDENTDATE"].ToString()).ToString("yyyy-mm-dd");
                userBill.USER_ID = Convert.ToInt32(row["USER_ID"].ToString());
                userBill.FIX_PRICE = Convert.ToDouble(row["FIX_PRICE"].ToString());
                userBill.SPARE_PRICE = Convert.ToDouble(row["SPARE_PRICE"].ToString());
                userBill.RENT_PRICE = Convert.ToDouble(row["RENT_PRICE"].ToString());
                userBill.TRANSCAR_PRICE = Convert.ToDouble(row["TRANSCAR_PRICE"].ToString());
                userBill.note = row["NOTE"].ToString() ;
                userBill.workshop_id = Convert.ToInt32(row["workshop_id"].ToString());


                UserBills.Add(userBill);
            }
            return UserBills;
        }



        public List<UserBill> GetWorkshopBills(int workshop_id)
        {
            ServicesQuery serviceQuery = new ServicesQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(serviceQuery.GetWorkshopBill(workshop_id));
            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            List<UserBill> UserBills = new List<UserBill>();

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                UserBill userBill = new UserBill();

                userBill.id = Convert.ToInt32(row["ID"].ToString());
                userBill.PLATENUMBER = row["PLATENUMBER"].ToString();
                userBill.ACCIDENTDATE = Convert.ToDateTime(row["ACCIDENTDATE"].ToString()).ToString("yyyy-mm-dd");
                userBill.USER_ID = Convert.ToInt32(row["USER_ID"].ToString());
                userBill.FIX_PRICE = Convert.ToDouble(row["FIX_PRICE"].ToString());
                userBill.SPARE_PRICE = Convert.ToDouble(row["SPARE_PRICE"].ToString());
                userBill.RENT_PRICE = Convert.ToDouble(row["RENT_PRICE"].ToString());
                userBill.TRANSCAR_PRICE = Convert.ToDouble(row["TRANSCAR_PRICE"].ToString());
                userBill.note = row["NOTE"].ToString();
                userBill.workshop_id =Convert.ToInt32( row["workshop_id"].ToString());


                UserBills.Add(userBill);
            }
            return UserBills;
        }




        public List<Services> GetServiceByTypeId(int id )
        {
            ServicesQuery serviceQuery = new ServicesQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(serviceQuery.GetServiceByTypeId(id));
            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            List<Services> listServices = new List<Services>();

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                Services service = new Services();

                service.id = Convert.ToInt32(row["ID"].ToString());
                service.SERVICENAME_AR = row["SERVICENAME_AR"].ToString();
                service.SERVICENAME_EN = row["SERVICENAME_EN"].ToString(); 
                service.SERVICEPRICE =Convert.ToDouble( row["SERVICEPRICE"].ToString());
                service.SERVICETIME =Convert.ToInt32( row["SERVICETIME"].ToString());
                service.SERVICETYPE_ID =Convert.ToInt32(row["SERVICETYPE_ID"].ToString());
                service.IMAGE = row["IMAGE"].ToString();


                listServices.Add(service);
            }
            return listServices;  
        }

        public bool SendNotification(Notification notification)
        {

            try
            {


                WebRequest tRequest = WebRequest.Create("https://fcm.googleapis.com/fcm/send");
                tRequest.Method = "post";
                //serverKey - Key from Firebase cloud messaging server  
                tRequest.Headers.Add(string.Format("Authorization: key={0}", "AAAAnhvLjnk:APA91bFvb5R_S208lRR08BX7xGlHXM67A8YMkgtkj8-rgRqpufwNgpJWtB_-H3YtdChQiUL8h-RHF7OmcwO7myDDurdQItrUDbC4r7iTPcsjFFry7oBWWOlBWmLdsgF2VWTSJh5bPEtj"));
                //Sender Id - From firebase project setting  
                tRequest.Headers.Add(string.Format("Sender: id={0}", "679071157881"));
                tRequest.ContentType = "application/json";

                var payload = new
                {
                    notification.to,
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                         notification.body,
                         notification.title,
                         notification.badge,
                         image = "http://144.91.94.182/41.png"
                    },
                };

                string postbody = JsonConvert.SerializeObject(payload).ToString();
                Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
                tRequest.ContentLength = byteArray.Length;
                using (Stream dataStream = tRequest.GetRequestStream())
                {
                    dataStream.Write(byteArray, 0, byteArray.Length);
                    using (WebResponse tResponse = tRequest.GetResponse())
                    {
                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {
                            if (dataStreamResponse != null) using (StreamReader tReader = new StreamReader(dataStreamResponse))
                                {
                                    String sResponseFromServer = tReader.ReadToEnd();
                                    return true;
                                    //result.Response = sResponseFromServer;
                                }
                        }
                    }
                }
                return false;

            }
            catch(Exception ex )
            {
                return false;
            }
            
        }

        public bool SendNotiAllDevices(int user_id , string title , string body ,int badge )
        {

            try
            {
                Notification notification; 
                List<string> devices =  new UserRepository.UserRepository(language).GetUserDevices(user_id); 

                for(int i=0; i < devices.Count; i++)
                {
                    notification=  new Notification();
                    notification.title = title;
                    notification.body = body;
                    notification.badge = badge;
                    notification.to = devices[i];
                    if(SendNotification(notification))
                    {
                        continue;
                    }
                    else
                    {
                        new Response(false, "Notification Not Sent", "Notification Not Sent : Device Id : " + devices[i] + " :: , User_id :  " + user_id); 
                    }
                }

                return true; 

            }
            catch (Exception ex)
            {
                new Response(false, "Notification Not Sent", "Notification Not Sent :   User_id :  " + user_id+ " exception : "+ ex);
                return false ; 
            }

        }



    }
}
