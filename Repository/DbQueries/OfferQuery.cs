using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Text;
using HelperClass;
using Domain.Entities;
using System.Data;
using Domain.Exceptions;
using Domain.Entities.Filter;
using Domain.Enums;

namespace DbQueries
{
    class OfferQuery :BaseQuery
    {

        string language = "en";

        public OfferQuery(string language)
        {
            this.language = language;
        }

        public string GetAllRequestsAreaPermission(int workshop_id)
        {
            string Query = "select ar.* , st.STATUS_NAME_AR,  st.STATUS_NAME_EN  from AllRequestsStatus     ar " +
                "JOIN statustype st ON ar.status = st.id where ar.accident_id in (select accident_id from permission " +
                "where INDUSTRIALAREA_ID in (select INDUSTRIALAREA_ID from workshop where id = '"+ workshop_id + "')) and confirmation <>0  order by ACCIDENTDATE desc ";
            return Query;
        }

        public string GetAllRequestsCityPermission(int workshop_id)
        {
            string Query = "select ar.* , st.STATUS_NAME_AR,  st.STATUS_NAME_EN  from ALLREQUESTSSTATUS  ar "+
                " JOIN statustype st ON ar.status = st.id "+
                " where ar.accident_id in (select accident_id from permission where city_id = GetCityIdByWorkshopId('"+ workshop_id + "') ) and "+
                " CheckWorkshopOffer(" + workshop_id + ", ar.accident_id) = 1 " +
                " order by REQUESTDATE desc";

            return Query;
        }

   

        public string GetOrderDetailsAssigns(int order_id  )
        {
            string Query = "select * from ORDER_LIST_DETAILS_V where order_id = "+ order_id;
            return Query;

        }
        public string GetWorkshopOfferNotAccespted(int workshop_id)
        {
            string Query = " select * from workshop_requests_st where status in(11,33,55,444) and workshop_id  ='" + workshop_id + "'   order by OFFER_DATETIME desc , status asc ";
          //  string Query = " select * from workshop_requests_st where    workshop_id  ='" + workshop_id + "'   order by OFFER_DATETIME desc , status asc ";
            return Query;

        }
        public string GetWorkshopOfferAccespted(int workshop_id)
        {
            string Query = " select * from workshop_requests_st where status in(22) and workshop_id  ='" + workshop_id + "'   order by OFFER_DATETIME desc , status asc ";
            return Query;

        }
        public string GetWorkshopOfferIsFixing(int workshop_id)
        {
            string Query = " select * from workshop_requests_st where status in(101) and workshop_id  ='" + workshop_id + "'   order by OFFER_DATETIME desc , status asc ";
            return Query;

        }
        public string GetWorkshopOfferFinishFixing(int workshop_id)
        {
            string Query = " select * from workshop_requests_st where status in(102 ,103) and workshop_id  ='" + workshop_id + "'   order by OFFER_DATETIME desc , status asc ";
            return Query;

        }
        public string GetSupplierStats(int supplier_Id)
        {
            string Query = " select * from SUPPLIERSTATS_V where supplier_id  = '" + supplier_Id + "'   and OFFER_TYPE ='Confirmed' ";
            return Query;

        }

        public string GetPersonStats(int user_id)
        {
            string Query = "with  cte as " +
                " ( select  accident_id as id   from offer " +
                " where accident_id in (select id from accident where  VEHICLE_ID in(select id from VEHICLE where user_id = "+ user_id + " ))  " +
                " group by   accident_id   having     max (confirmation)  =0  ) " +
                " select     count(  confirmation) as Offer      from offer join cte on offer.accident_id = cte.id ";
            return Query;

        }


        public string GetOfferDetailsByOrderId(int order_id)
        {
            string Query = " select * from offerDetails_v where confirmation =1 and  order_id = " + order_id;
            return Query;

        }

        


        public string GetOffertempOfferByUser_id(int user_id)
        {
            string Query = " select * from offerDetails_v where  SUPPLIER_USER_ID = " + user_id;
            return Query;

        }

        public string GetOffertempOfferByOffer_id(int offer_id)
        {
            string Query = " select * from offerDetails_v where  OFFER_ID = " + offer_id;
            return Query;

        }

        public string GetOfferDetailsWorkShop(int accident_id , int  workshop_id )
        {
            string Query = " select * from offerDetails_v where  workshop_id ='"+workshop_id+"' and  accident_id = " + accident_id;
            return Query;
        }

        public string GetPersonOffersWithCondition(OfferCriteria offerCriteria, string language ,int user_id)
        {
            
            string Query = " select  *  from personoffers_v  pv    join PERSON_REQUEST_V PRO on pv.order_id = PRO.order_id  " +

                          "  where  confirmation not in ("+(int)Enums.orderStatus.REJECTED+","+ (int)Enums.orderStatus.CONFIRMED + " ) and  colorLang = '" + language + "' and OrderTYPELANG = '"+ language + "' and PRO.user_id = " +  user_id + "  and  1=1 ";

            //filters 

            if (offerCriteria.modelID != 0)
            {
                Query += "  and model_id =" + offerCriteria.modelID+"  ";
               
            }

            if (offerCriteria.orderType != 0)
            {
                Query += "  and PRO.ORDERTYPE_ID =" + offerCriteria.orderType + "  ";

            }

            string PriceCondition = "";
            string Sort = "";
            string RatingCondition = "  and  pv.rating >= " + offerCriteria.rate + "  ";

            
            Sort = new   Sort().getSortOffer(offerCriteria.sortType , "pv");

            if (offerCriteria.fromPrice != 0 && offerCriteria.toPrice != 0)
            {
                PriceCondition = "  and  pv.price >= " + offerCriteria.fromPrice + " and pv.price <= " + offerCriteria.toPrice;
              
            }

           


            return Query + PriceCondition + RatingCondition + Sort;
        }


        public string GetPersonOfferCount(int user_id )
        {
            string Query = " select count(offer_id) as offerCount  from personoffers_V where offer_status =0  and    confirmation not in (-1,1 )   and user_id =" + user_id + " group by user_id ";
 
            return Query;
        }


        public string GetsupplierOffersUnConfirmed(int user_id , string language  )
        {
             
            string Query = " select  *  from personoffers_v  pv    join PERSON_REQUEST_V PRO on pv.order_id = PRO.order_id  " +

                          "  where  PRO.order_Status   in (" + (int)Enums.orderStatus.UNCONFIRMED + " ," + (int)Enums.orderStatus.REJECTED + " ) and  colorLang = '" + language + "' and OrderTYPELANG = '" + language + "' and PV.supplier_user_id = " + user_id + "  and  1=1 ";

            return Query;
        }

        public string GetsupplierOffersConfirmed(int user_id, string language)
        {
            
            string Query = " select  *  from personoffers_v  pv    join PERSON_REQUEST_V PRO on pv.order_id = PRO.order_id  " +

                          "  where  PRO.order_Status   in ( " + (int)Enums.orderStatus.CONFIRMED + " ) and  colorLang = '" + language + "' and OrderTYPELANG = '" + language + "' and PV.supplier_user_id = " + user_id + " and  1=1 ";

            return Query;
        }

        public string GetsupplierOffersStart(int user_id, string language)
        {
             
            string Query = " select  *  from personoffers_v  pv    join PERSON_REQUEST_V PRO on pv.order_id = PRO.order_id  " +

                          "  where  PRO.order_Status   in ( " + (int)Enums.orderStatus.WAITINGTOFINISH + " ) and  colorLang = '" + language + "' and OrderTYPELANG = '" + language + "' and PV.supplier_user_id = " + user_id + "  and  1=1 ";

            return Query;
        }

        public string GetsupplierOffersfinish(int user_id, string language)
        {
           
            string Query = " select  *  from personoffers_v  pv    join PERSON_REQUEST_V PRO on pv.order_id = PRO.order_id  " +

                          "  where  PRO.order_Status   in ( " + (int)Enums.orderStatus.FINISHED + " ," + (int)Enums.orderStatus.DELIVERED + "  ) and  colorLang = '" + language + "' and OrderTYPELANG = '" + language + "' and PV.supplier_user_id = " + user_id + "  and  1=1 ";

            return Query;
        }

        public string GetOffersWithCondition(List<Filter> filters ,Sort sort)
        {
           
            Filter filter = new Filter();
            Sort orderBy = new Sort();

            string Query = " select main.*  from personoffers_v  main    " +
                          "  where  1=1 ";
            return Query + filter.generateCondition(filters, "main") + orderBy.generateSort(sort, "main");
        }

        //public string GetAllRequests()
        //{
        //    string Query = "select * from RequestsForWarsha_v";
        //    return Query;
        //}


        public bool InsertPermissionArea(int accident_id ,int area_id)
        {


            try
            { 
                  CommanDB procConn = new CommanDB();
                  OracleParameter[] param = {
                  new OracleParameter("@p_accident_id",accident_id),
                  new OracleParameter("@p_area_id",area_id)
                             };
                procConn.RunProc("InsertPermissionArea_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language  ,ex.Message);
            }

        }

        public bool InsertPermissionCity(int accident_id, int city_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_accident_id",accident_id),
                  new OracleParameter("@p_city_id",city_id)
                             };
                procConn.RunProc("InsertPermissionCity_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }

        public bool UpdateReadyToFixFlag(int accident_id )
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_accident_id",accident_id),
                             };
                procConn.RunProc("UpdateReadyToFixFlag_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }

        public bool UpdateConfirmOffer(int offer_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_offer_id",offer_id),
                  new OracleParameter("@p_order_status",(int)Domain.Enums.Enums.orderStatus.CONFIRMED),
                  new OracleParameter("@p_confirm_status",(int)Domain.Enums.Enums.orderStatus.REJECTED)
                             };
                procConn.RunProc("UpdateConfirmOffer_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }
        public bool UpdateFinishedFlag(int offer_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_offer_id",offer_id),
                  new OracleParameter("@p_order_status",(int)Domain.Enums.Enums.orderStatus.FINISHED),
                             };
                procConn.RunProc("UpdateFinishedFlag_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }

        public bool UpdateDeliveredFlag(int offer_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_offer_id",offer_id),
                  new OracleParameter("@p_order_status",(int)Domain.Enums.Enums.orderStatus.DELIVERED),
                             };
                procConn.RunProc("UpdateDELIVEREDFLAG_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }
        public bool UpdateWaitingFix_sp(int offer_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_offer_id",offer_id),
                  new OracleParameter("@p_order_status",(int)Domain.Enums.Enums.orderStatus.WAITINGTOFINISH),
                             };
                procConn.RunProc("UpdateWAITINGFIX_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }

        public bool RejectOffer(int offer_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_offer_id",offer_id),
                  new OracleParameter("@p_confirm_Status",(int)Enums.orderStatus.REJECTED),

                             };
                procConn.RunProc("UpdateRejectOffer_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new UpdateException(language, ex.Message);
            }

        }

        public bool DeleteOffer(int offer_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_offer_id",offer_id),
                             };
                procConn.RunProc("DeleteOffer_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new DeleteException(language, ex.Message);
            }

        }
        public bool DeleteIndustrialPermission(int accident_id)
        {


            try
            {
                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {
                  new OracleParameter("@p_accident_id",accident_id),
                             };
                procConn.RunProc("DeleteIndustrialPermission_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new DeleteException(language, ex.Message);
            }

        }
        public bool InsertOffer(int user_id , Offer offer)
        {
            try
            {
                int confirmation = (int)Domain.Enums.Enums.orderStatus.UNCONFIRMED; 

                CommanDB procConn = new CommanDB();
                OracleParameter[] param = {

                  new OracleParameter("@p_USER_ID", user_id),
                  new OracleParameter("@p_ORDER_ID", offer.order_id),
                  new OracleParameter("@p_price", offer.price),
                  new OracleParameter("@p_timeValue", offer.timeValue),
                  new OracleParameter("@p_timeFlag", offer.timeFlag),
                  new OracleParameter("@p_CONFIRMATION",confirmation) ,
                  new OracleParameter("@p_OFFER_STATUS",confirmation)
                             };

                procConn.RunProc("insertOffer_sp", param);
                return true;
            }
            catch (Exception ex)
            {
                throw new InsertException(language, ex.Message);
            }

        }
     

        

    }
}
