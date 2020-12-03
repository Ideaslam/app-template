using DbQueries;
using Domain.Entities.Services;
using Domain.Entities.supplier;
using Domain.Exceptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.DbQueries
{
    class ServicesQuery :BaseQuery 
    {

        string language = "en";

        public ServicesQuery(string language)
        {
            this.language = language;
        }

        public string GetServiceTypes()
        {
            string Query = "select * from serviceTypes"; 
            return Query;

        }

        public string GetUserBill(int user_id )
        {
            string Query = "select  b.id, PLATENUMBER, ACCIDENTDATE ,USER_ID ,  FIX_PRICE  ,	" +
                " SPARE_PRICE,	RENT_PRICE, TRANSCAR_PRICE  ,note ,workshop_id  from  billfix  b  " +
                " join  accident ac on  ac.id = b.accident_id  " +
                " join  VEHICLE  v  on  v.id =ac.VEHICLE_id where user_id = "+ user_id;
            return Query;

        }

        public string GetWorkshopBill(int workshop_id)
        {
            string Query = "select  b.id, PLATENUMBER, ACCIDENTDATE ,USER_ID ,  FIX_PRICE  ,	" +
                " SPARE_PRICE,	RENT_PRICE, TRANSCAR_PRICE  ,note ,workshop_id  from  billfix  b  " +
                " join  accident ac on  ac.id = b.accident_id  " +
                " join  VEHICLE  v  on  v.id =ac.VEHICLE_id where workshop_id = " + workshop_id;
            return Query;

        }

        public string GetServiceByTypeId(int servcieTypeId)
        {
            string Query = "select * from services where SERVICETYPE_ID = "+ servcieTypeId;
            return Query;

        }

        public string ComparePassword(string oldPassword ,int user_id )
        {
            string Query = "select password from users where   id= "+user_id;
            return Query;

        }


        public bool InsertService(serviceRequest serviceRequest , int user_id )
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {

                  new OracleParameter("@p_SERVICE_ID", serviceRequest.SERVICE_ID),
                  new OracleParameter("@p_SRCX", serviceRequest.SRCX),
                  new OracleParameter("@p_SRCY",  serviceRequest.SRCY),
                  new OracleParameter("@p_DESTX",  serviceRequest.DESTX),
                  new OracleParameter("@p_DESTX",  serviceRequest.DESTY),
                  new OracleParameter("@p_USER_ID",user_id )
                             };
                procConn.RunProc("insertService_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language ,ex.Message);
            }

        }


        public bool InsertBill(BillFix  bill)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {

                 new OracleParameter("@p_accident_id", bill.accident_id),
                 new OracleParameter("@p_workshop_id", bill.workshop_id),
                 new OracleParameter("@p_fix_price", bill.fix_price),
                 new OracleParameter("@p_spare_price", bill.spare_price),
                 new OracleParameter("@p_trans_price", bill.TRANSCAR_PRICE),
                 new OracleParameter("@p_rent_price", bill.rent_price),
                 new OracleParameter("@p_note", bill.note),
                
                             };
                procConn.RunProc("insertBill_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }


        public bool ChangePassword(string newPassword ,int user_id)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {

                 new OracleParameter("@p_newPassword", newPassword),
                 new OracleParameter("@p_user_Id", user_id)
                             };
                procConn.RunProc("changePassword_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }

        public bool ChangePhoneNumber(string phoneNumber , int user_id)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {

                 new OracleParameter("@p_phoneNumber", phoneNumber),
                 new OracleParameter("@p_user_Id", user_id)
                             };
                procConn.RunProc("changePhoneNumber_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }


        public bool CancelServiceRequest(int service_id )
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {

                  new OracleParameter("@p_SERVICE_ID", service_id)
                             };
                procConn.RunProc("CANCELSERVICE_SP", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }


        public bool FinishServiceRequest(int service_id , int rating)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {

                  new OracleParameter("@p_SERVICE_ID", service_id),
                  new OracleParameter("@p_rating", rating)
                             };
                procConn.RunProc("CANCELSERVICE_SP", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }




    }
}
