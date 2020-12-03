using Domain.Entities;
using Domain.Exceptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbQueries
{
  public  class VehicleQuery :BaseQuery
  
    {

        string language = "en"; 

        public VehicleQuery(string language)
        {
            this.language = language;
        }

        public string CheckFixPaperByPlateNumber(string plateNumber)
        {
            string Query = "select * from fixpapers where expiryDate >= trunc(sysdate) and car_plateNumber  = '"+ plateNumber + "'";
            return Query; 
        }


        public string GetCarBasicInformation(int user_id)
        {


            string Query = "select * from  CARBASICINFOWITHNONSONFIRMED_V where user_id ='" + user_id .ToString()+ "'    ";
            return Query;
        }

        public string GetVehiclesByUserId(string user_id)
        {


            string Query = "select v.id , logo , MODELNAME_EN	 ,	MODELNAME_AR ,FOUNDDATE ,COLOR_NAME ,b.brandname_ar ,b.brandname_en " +
                " ,user_id  from vehicle v " +
                " left join car_model m on v.model_id = m.id  " +
                " left join car_brand b on m.brand_id = b.id " +
                " left join color_translation ct on v.color_id = ct.COLOR_NON_TRANS_ID " +
                " join language l on ct.lang_id = l.LANGUAGE_ID where  l.name ='"+ language + "' and  user_id ="+ user_id;
            return Query;
        }

        public string GetModelsByBrandId(int brandId)
        {


            string Query = "select  * from car_model where brand_id ="+brandId+"  and isactive = 1"; 
            return Query;
        }



        public string CheckFixPaperByAccidentId(int accident_id)
        {


            string Query = "select * from accident  ac join fixpapers f on  ac.fixpaper_id = f.id   and f.expiryDate >= trunc(sysdate )    and ac.id ="+ accident_id  ;
            return Query;
        }



        public string GetWorkshopCars(int workshop_id)
        {
            string Query = "select PLATENUMBER ,accident_id ,price from offer o " +
                " join accident ac on  o.accident_id = ac.id " +
                " join VEHICLE  v on ac.VEHICLE_ID = v.id " +
                " where confirmation =1 and workshop_id =" + workshop_id;
            return Query;

        }
        public bool InsertVehicleData( Vehicle vehicle ,string user_id)
        {
             

            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
           
 
                  new OracleParameter("@c_model_id ",vehicle.modelId),
                  new OracleParameter("@c_color_id ",vehicle.colorId) ,
                  new OracleParameter("@foundDate",vehicle.foundDate),
                  new OracleParameter("@user_id",user_id)

                             };
                procConn.RunProc("insertvehicle_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }


        public bool InsertFixPaper(FixPaper fixPaper , int user_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_user_id",user_id),
                  new OracleParameter("@p_paper_id",fixPaper.paper_id),
                  new OracleParameter("@p_issuedate",fixPaper.issueDate.ToString("yyyy-MM-dd")),
                  new OracleParameter("@p_expirydate",fixPaper.expiryDate.ToString("yyyy-MM-dd")) ,
                  new OracleParameter("@p_car_platenumber ",fixPaper.car_plateNumber ),
               


                             };
                procConn.RunProc("insertFixPaper_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }



    }
}
