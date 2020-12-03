using Domain.Entities.Order;
using Domain.Enums;
using Domain.Exceptions;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;

namespace DbQueries
{
    public class OrderQuery :BaseQuery
    {
        string language = "en"; 
        public OrderQuery(string language )
        {
            this.language = language; 

        }
        public string GetAccidentId ( string plateNumber , string accidentDate)
        {
            string  Query = "select ACCIDENTID from CARBASICINFOALLCARS_V where  PLATENUMBER = '" + plateNumber + "' and  ACCIDENTDATE   = to_date ('" + accidentDate + "'  , 'yyyy-mm-dd hh24:mi:ss')";
            return Query;

        }

        public string GetControls(int orderType_id  )
        {
            string Query = "select * from datacontrol where ordertype_id =    "+ orderType_id;
            return Query;

        }


        public string GetRequestsByUser_id(int user_id, string lang)
        {
            string Query = " select * from PERSON_REQUEST_V where  OrderTypeLang  ='" + lang + "'     and ColorLang='" + lang + "'  and user_id = " + user_id + "  order by order_id desc";
            return Query;

        }

        public string GetAllRequests( int city_id , int supplier_type ,int supplier_id, int pageSize, int pageNumber, string lang)
        {
            

            if (pageSize == 0)
                pageSize = 30;

            if (pageNumber == 0)
                pageNumber = 1;

            int from = 0;
            int to = 0;
            int offset = pageSize;

            from = (offset * pageNumber) - offset + 1;
            to = (offset * pageNumber);

            string Query = "select * from (select r.* , rownum as seq   from (";
                   Query += " select * from PERSON_REQUEST_V PR  where  PR.isaccepted =1 and order_id not in (select order_id from offer  where supplier_id = " + supplier_id + " ) and  PR.order_Status   in (" + (int)Enums.orderStatus.UNCONFIRMED + ")   and   OrderTypeLang  ='" + lang + "'   " +
                "  and ColorLang='" + lang + "' and city_id ="+city_id+ "     and ORDERTYPE_ID in (select  id from ordertype  where SUPPLIERTYPE_ID ="+ supplier_type + "  )    order by order_id desc";
                   Query += " ) r)where seq  between "+from+" and "+to  ; 



            return Query;

        }

        public string GetRequestByOrder_id(int order_id, string lang)
        {
            string Query = " select * from PERSON_REQUEST_V where  OrderTypeLang  ='" + lang + "'     and ColorLang='" + lang + "'  and order_id = " + order_id + "  order by order_id desc";
            return Query;

        }

        public string GetOrderTypeWithControls()
        {
            string Query = "select photo , video , location ,infowindow ,car ,city , ordertype.id, ordertype_name ,ordertype_icon from datacontrol  dt " +
                " join orderType  on dt.ordertype_id = ordertype.id " +
                " join ordertype_translation ot on  orderType.id     =ot.ORDERTYPE_NON_TRANS_ID " +
                " join language l  on    ot.lang_id = l.LANGUAGE_ID " +
                " where isactive = 1 and l.name ='"+language+"'";
            return Query;

        }

        public string GetAllControls( )
        {
            string Query = "select * from datacontrol"  ;
            return Query;

        }

        public string GetOrderListTitle(int orderType_id)
        {
            string Query = "select * from ORDERLIST where ordertype_id ="+ orderType_id;
            return Query;

        }

        public string GetOrderListDetailsByList(int orderList_id)
        {
            string Query = "select * from ORDERLIST_details where orderlist_id =" + orderList_id;
            return Query;

        }
        public string GetOrderListDetailsByType(int orderType_id)
        {
            string Query = "select old.id ,old.ORDERLIST_ID , CONTENTNAME_EN	,	CONTENTNAME_AR  from orderList_details  old " +
                           "join  orderlist ol on   old.orderlist_id =ol.id where ol.ordertype_id ="+ orderType_id;
            return Query;

        }


        public string GetOrderIdByCarId(int car_id )
        {
            string Query = "select id  from orders  where vehicle_id = "+ car_id + "   order by id desc";
            return Query;

        }

        public string GetOrderIdByUserId(int user_id)
        {
            string Query = "select *  from orders  where user_id = " + user_id + "   order by id desc";
            return Query;

        }

        public string GetCityIdByUserId(int user_id)
        {
            string Query = "select city_id  from supplier  where user_id = " + user_id + "  ";
            return Query;

        }

        public string GetSupplierByUserId(int user_id)
        {
            string Query = "select *  from supplier  where user_id = " + user_id + "  ";
            return Query;

        }


        public string GetLastWorkshopLocation(int user_id)
        {
            string Query = "select LOCATIONX	, LOCATIONY from offer   o  " +
                " join workshop w on  o.workshop_id = w.id " +
                " where accident_id in(select id from accident where      vehicle_id in (select id from VEHICLE where user_id ="+user_id+"  )  ) and " +
                "  confirmation =1  and waitingfix =0 order by  CONFIRM_DATETIME desc";
            return Query; 
        }

        public string GetAccidentDetails(int accident_id)
        {
            string Query = " select * from AccidentDetails_v where accident_id = "+ accident_id ;
            return Query;

        }

        public string GetOrderAllMedia(int order_id)
        {
            string Query = "select * from picture where order_id =" + order_id + "  ";
            return Query;

        }

        public string GetAccidentPictures(int accident_id)
        {
            string Query = "select * from picture where accident_id ="+ accident_id + "  and pic_type  =1 ";
            return Query;

        }


        public string GetAccidentVideos(int accident_id)
        {
            string Query = "select * from picture where accident_id =" + accident_id + "  and pic_type  = 2 ";
            return Query;

        }


        public bool DeleteRequestByOrderId(int order_id)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_order_id",order_id),
                             };
                procConn.RunProc("DeleteOrder_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new DeleteException(language, ex.Message);
            }

        }

        public bool ResetReadyToFixFlag(int order_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_order_id",order_id),
                             };
                procConn.RunProc("ResetReadyToFixFlag_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }

        public bool SetReadyToFixFlag(int accident_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_accident_id",accident_id),
                             };
                procConn.RunProc("UPDATEREADYTOFIXFLAG_SP", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }

        public bool InsertOrderData(CarInfoOrder carInfoOrder , string user_id)
        {  
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {


                  new OracleParameter("@ac_order_identity", carInfoOrder.OrderIdentity),
                  new OracleParameter("@ac_user_id", user_id),
                  new OracleParameter("@ac_car_id", carInfoOrder.carId),
                  new OracleParameter("@ac_city_id", carInfoOrder.cityId),
                  new OracleParameter("@ac_orderType", carInfoOrder.orderType),
                  new OracleParameter("@ac_locationX", carInfoOrder.locationX),
                  new OracleParameter("@ac_locationY", carInfoOrder.locationY),
                  new OracleParameter("@ac_note", carInfoOrder.note),

                             };

                procConn.RunProc("insertOrder_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message+"  INSERT Order DATA");
            }

        }


        public bool InsertOrderDetailAssign(int order_id, int  order_detail_assign_id)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {



                  new OracleParameter("@p_orderId", order_id),
                  new OracleParameter("@p_orderAssignId",order_detail_assign_id),

                             };

                procConn.RunProc("insertOrderAssigns_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message + "  INSERT Order DATA");
            }

        }

        public bool UpdateAccidentData(CarInfoOrder carInfoOrder)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {

                  new OracleParameter("@c_accident_id",carInfoOrder.order_id),
                  new OracleParameter("@ac_fixType", carInfoOrder.orderType),
                  new OracleParameter("@ac_locationX", carInfoOrder.locationX),
                  new OracleParameter("@ac_locationY", carInfoOrder.locationY),

               
                             };

                procConn.RunProc("updateInfoAndAccidentData_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }


        public bool InsertOrderImageUrl(string imageUrl , int image_type, int accidentId)
        {
            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@order_id", accidentId),
                  new OracleParameter("@pic_url", imageUrl),
                  new OracleParameter("@pic_type", image_type),
                             };

                procConn.RunProc("insertOrderPic_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message+"-> INSERT IMAGE OR VIDEO URLS");
            }

        }
    }
}
