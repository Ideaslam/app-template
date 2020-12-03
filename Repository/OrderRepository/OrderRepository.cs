using DbQueries;
using Domain.Entities;
using Domain.Entities.Offers;
using Domain.Entities.Order;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Offer;
using Domain.Messages;
using Repository.HelperRepository;
using Repository.ServicesRepository;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Enums.Enums;
using static Domain.Messages.Messages;

namespace OrderRepository
{
    public class OrderRepository : Domain.Interfaces.Order.IOrderRepository
    {
        Common conn_db = new Common();

        string language = "en";

        public OrderRepository(string language)
        {
            this.language = language;
        }


        public bool DeleteImageByUrl(string image_url)
        {
            BaseQuery baseQuery = new BaseQuery();
            try
            {
                conn_db.ExecuteSql(baseQuery.DeleteObjectByColname("picture", "pic_url", image_url));
                return true;
            }
            catch (Exception ex)
            {
                throw new DeleteException(language, ex.Message);
            }
        }



        public int GetAccidentId(string plateNumber, string accientDate)
        {
            OrderQuery accidentQuery = new OrderQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(accidentQuery.GetAccidentId(plateNumber, accientDate));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            return Convert.ToInt32(dataTable.Rows[0]["ACCIDENTID"].ToString());


        }

        public int GetOrderIdByUserId(int  user_id )
        {
            OrderQuery orderQuery = new OrderQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(orderQuery.GetOrderIdByUserId(user_id));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            return Convert.ToInt32(dataTable.Rows[0]["id"].ToString());


        }


        public int GetCityIdByUserId(int car_id)
        {
            OrderQuery orderQuery = new OrderQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(orderQuery.GetOrderIdByUserId(car_id));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            return Convert.ToInt32(dataTable.Rows[0]["id"].ToString());


        }

        public List<UserRequestCriteria> GetAllRequests(int user_id , int pageSize ,int pageNumber)
        {

            //OfferQuery offerQuery = new OfferQuery(language);
            OrderQuery orderQuery = new OrderQuery(language);

            System.Data.DataTable Supplier = conn_db.ReadTable(orderQuery.GetSupplierByUserId(user_id));
            int city_id = 0;
            int supplier_type = 0;
            int supplier_id = 0;
            double supplier_lat = 0;
            double supplier_lng = 0;

            if (Supplier.Rows.Count > 0)
            {
                supplier_id =    Supplier.Rows[0]["ID"] is DBNull ? 0 : Convert.ToInt32(Supplier.Rows[0]["ID"]);
                city_id =        Supplier.Rows[0]["city_id"] is DBNull ? 0 : Convert.ToInt32(Supplier.Rows[0]["city_id"]);
                supplier_type  = Supplier.Rows[0]["SUPPLIER_TYPE_ID"] is DBNull ? 0 : Convert.ToInt32(Supplier.Rows[0]["SUPPLIER_TYPE_ID"]);
                 
                supplier_lat = Supplier.Rows[0]["LOCATIONX"] is DBNull ? 0 : Convert.ToDouble(Supplier.Rows[0]["LOCATIONX"].ToString());
                supplier_lng = Supplier.Rows[0]["LOCATIONY"] is DBNull ? 0 : Convert.ToDouble(Supplier.Rows[0]["LOCATIONY"].ToString());


            }
                


            System.Data.DataTable dataTable = conn_db.ReadTable(orderQuery.GetAllRequests(city_id , supplier_type, supplier_id ,  pageSize,   pageNumber, language));
           
             


            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            UserRequestCriteria userRequestCriteria = new UserRequestCriteria();
            List<UserRequestCriteria> ListRequestCriterias = new List<UserRequestCriteria>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                userRequestCriteria = new UserRequestCriteria();
                userRequestCriteria.ORDER_ID = row["ORDER_ID"] is DBNull ? 0 : Convert.ToInt32(row["ORDER_ID"]);
                userRequestCriteria.USER_ID = row["USER_ID"] is DBNull ? 0 : Convert.ToInt32(row["USER_ID"].ToString());
                try
                {
                    userRequestCriteria.ORDERDATE = Convert.ToDateTime(row["ORDERDATE"].ToString()).ToString("dd-MM-yyyy");
                }
                catch (Exception ex)
                {
                    userRequestCriteria.ORDERDATE = "";
                }
                userRequestCriteria.PLATENUMBER = row["PLATENUMBER"].ToString();
                userRequestCriteria.ORDER_IDENTITY = row["ORDER_IDENTITY"].ToString();
                userRequestCriteria.firstName = row["FIRSTNAME"].ToString();
                userRequestCriteria.lastName = row["LASTNAME"].ToString();
                userRequestCriteria.phoneNumber = row["PHONENUMBER"].ToString();
                userRequestCriteria.ORDERTYPE_ID = row["ORDERTYPE_ID"] is DBNull ? 0 : Convert.ToInt32(row["ORDERTYPE_ID"]);
                userRequestCriteria.ORDERTYPE_NAME = row["ORDERTYPE_NAME"].ToString();
                userRequestCriteria.VEHICLE_ID = row["VEHICLE_ID"] is DBNull ? 0 : Convert.ToInt32(row["VEHICLE_ID"]);
                userRequestCriteria.ORDER_STATUS = row["ORDER_STATUS"] is DBNull ? 0 : Convert.ToInt32(row["ORDER_STATUS"]);
                userRequestCriteria.ISACTIVE = row["ISACTIVE"] is DBNull ? 1 : Convert.ToInt32(row["ISACTIVE"].ToString());
                userRequestCriteria.OFFERS_COUNT = row["OFFERS_COUNT"] is DBNull ? 0 : Convert.ToInt32(row["OFFERS_COUNT"].ToString());
                if (language == Messages.language.ar.ToString())
                {
                    userRequestCriteria.BRANDNAME = row["BRANDNAME_AR"].ToString();
                    userRequestCriteria.MODELNAME = row["MODELNAME_AR"].ToString();
                }
                else
                {
                    userRequestCriteria.BRANDNAME = row["BRANDNAME_EN"].ToString();
                    userRequestCriteria.MODELNAME = row["MODELNAME_EN"].ToString();
                }
                userRequestCriteria.carIMAGE = row["carImage"].ToString();
                userRequestCriteria.userIMAGE = row["userImage"].ToString();
                userRequestCriteria.Note = row["NOTE"].ToString();
                userRequestCriteria.COLORNAME = row["COLOR_NAME"].ToString();

                userRequestCriteria.lat = row["lat"] is DBNull ? 0 : Convert.ToDouble(row["lat"].ToString());
                userRequestCriteria.lng = row["lng"] is DBNull ? 0 : Convert.ToDouble(row["lng"].ToString());


                if (supplier_lat == 0 || supplier_lng == 0 || userRequestCriteria.lat==0 || userRequestCriteria.lng == 0)
                {
                    userRequestCriteria.time = "0";
                        userRequestCriteria.distance = 0;
                }

                    
                else
                {
                    double distanceMiles = Math.Sqrt(
                   Math.Pow((supplier_lat - userRequestCriteria.lat) * 69, 2) +
                   Math.Pow((supplier_lng - userRequestCriteria.lng) * 69.172, 2));
                    userRequestCriteria.distance = Math.Round(distanceMiles * 1.60934, 2);
                    userRequestCriteria.time = new ServicesRepository(language).getTime(supplier_lat + "," + supplier_lng, userRequestCriteria.lat + "," + userRequestCriteria.lng);
                }
                   

                ListRequestCriterias.Add(userRequestCriteria);


            }
            return ListRequestCriterias;


        }


        public List<UserRequestCriteria> GetRequestsByUser_id(int user_id)
        {

            //OfferQuery offerQuery = new OfferQuery(language);
            OrderQuery orderQuery = new OrderQuery(language);
            OfferQuery offerQuery = new OfferQuery(language);

         
            System.Data.DataTable dataTable = conn_db.ReadTable(orderQuery.GetRequestsByUser_id(user_id, language));


            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            UserRequestCriteria userRequestCriteria = new UserRequestCriteria();
            List<UserRequestCriteria> ListRequestCriterias = new List<UserRequestCriteria>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                userRequestCriteria = new UserRequestCriteria();
                userRequestCriteria.ORDER_ID = row["ORDER_ID"] is DBNull ? 0 : Convert.ToInt32(row["ORDER_ID"]);
                userRequestCriteria.USER_ID = row["USER_ID"] is DBNull ? 0 : Convert.ToInt32(row["USER_ID"].ToString());
                try
                {
                    userRequestCriteria.ORDERDATE = Convert.ToDateTime(row["ORDERDATE"].ToString()).ToString("dd-MM-yyyy");
                }
                catch (Exception ex)
                {
                    userRequestCriteria.ORDERDATE = "";
                }
                userRequestCriteria.PLATENUMBER = row["PLATENUMBER"].ToString();
                userRequestCriteria.ORDER_IDENTITY = row["ORDER_IDENTITY"].ToString();               
                userRequestCriteria.firstName = row["FIRSTNAME"].ToString();
                userRequestCriteria.lastName = row["LASTNAME"].ToString();
                userRequestCriteria.phoneNumber = row["PHONENUMBER"].ToString();     
                userRequestCriteria.ORDERTYPE_ID = row["ORDERTYPE_ID"] is DBNull ? 0 : Convert.ToInt32(row["ORDERTYPE_ID"]);
                userRequestCriteria.ORDERTYPE_NAME = row["ORDERTYPE_NAME"].ToString();
                userRequestCriteria.VEHICLE_ID = row["VEHICLE_ID"]  is DBNull ? 0 :  Convert.ToInt32(row["VEHICLE_ID"]);
                userRequestCriteria.ORDER_STATUS = row["ORDER_STATUS"]  is DBNull ? 0 : Convert.ToInt32(row["ORDER_STATUS"]);
                userRequestCriteria.ISACTIVE = row["ISACTIVE"]  is DBNull ? 1 : Convert.ToInt32(row["ISACTIVE"].ToString());
                userRequestCriteria.OFFERS_COUNT = row["OFFERS_COUNT"]  is DBNull ? 0 : Convert.ToInt32(row["OFFERS_COUNT"].ToString());
                if (language == Messages.language.ar.ToString())
                {
                    userRequestCriteria.BRANDNAME = row["BRANDNAME_AR"].ToString();
                    userRequestCriteria.MODELNAME = row["MODELNAME_AR"].ToString();
                }
                else
                {
                    userRequestCriteria.BRANDNAME = row["BRANDNAME_EN"].ToString();
                    userRequestCriteria.MODELNAME = row["MODELNAME_EN"].ToString();
                }
                userRequestCriteria.carIMAGE = row["carImage"].ToString();
                userRequestCriteria.userIMAGE = row["userImage"].ToString();

                userRequestCriteria.Note = row["NOTE"].ToString();
                userRequestCriteria.COLORNAME = row["COLOR_NAME"].ToString();
                userRequestCriteria.lat = row["lat"] is DBNull ? 0 : Convert.ToDouble(row["lat"].ToString());
                userRequestCriteria.lng = row["lng"] is DBNull ? 0 : Convert.ToDouble(row["lng"].ToString());
                userRequestCriteria.offer_id = -1;

                if (userRequestCriteria.ORDER_STATUS ==(int) orderStatus.FINISHED)
                {
                    
                   System.Data.DataTable  offerTable= conn_db.ReadTable(new OfferQuery(language).GetOfferDetailsByOrderId(userRequestCriteria.ORDER_ID ));
                    if (offerTable.Rows.Count > 0)
                        userRequestCriteria.offer_id = Int32.Parse(offerTable.Rows[0]["OFFER_ID"].ToString());
                    else
                        userRequestCriteria.offer_id = -1;
                }

                ListRequestCriterias.Add(userRequestCriteria);
            }
            return ListRequestCriterias;


        }

        public    Domain.Entities.Order.OrdersDetails   GetOrderDetails(int user_id , int order_id  ,double lat , double lng ,int offer_id)
        {
         
            OrderQuery orderQuery = new OrderQuery(language);
            OfferQuery offerQuery = new OfferQuery(language);
            System.Data.DataTable OrderDT = conn_db.ReadTable(orderQuery.GetRequestByOrder_id(order_id, language));
            int orderStatus=0;

            if (OrderDT.Rows.Count>0)
                orderStatus= OrderDT.Rows[0]["order_status"] is DBNull ?  0:Convert.ToInt32(OrderDT.Rows[0]["order_status"]) ; 

            System.Data.DataTable OrderDetailsAssign = conn_db.ReadTable(offerQuery.GetOrderDetailsAssigns(order_id));
            System.Data.DataTable OfferDT = new System.Data.DataTable();

            if (orderStatus == (int)Enums.orderStatus.UNCONFIRMED)
              OfferDT = conn_db.ReadTable(offerQuery.GetOffertempOfferByOffer_id(offer_id));
            else
              OfferDT = conn_db.ReadTable(offerQuery.GetOfferDetailsByOrderId(order_id));

            UserRequestCriteria userRequestCriteria = new UserRequestCriteria();
            OfferDTO  offersDTO = new OfferDTO();

            if (OrderDT.Rows.Count == 0)
                throw new EmptyViewException( language , Messages.GetMessage(language ,   TypeM.ACCIDENT, orderM.Order_DATA_NOT_FOUND));



            userRequestCriteria.ORDER_ID = OrderDT.Rows[0]["ORDER_ID"] is DBNull ? 0 : Convert.ToInt32(OrderDT.Rows[0]["ORDER_ID"]);
            userRequestCriteria.USER_ID = OrderDT.Rows[0]["USER_ID"]  is DBNull ? 0 : Convert.ToInt32(OrderDT.Rows[0]["USER_ID"].ToString());
            try
            {
                userRequestCriteria.ORDERDATE = Convert.ToDateTime(OrderDT.Rows[0]["ORDERDATE"].ToString()).ToString("dd-MM-yyyy");
            }
            catch (Exception ex)
            {
                userRequestCriteria.ORDERDATE = "";
            }
            userRequestCriteria.PLATENUMBER = OrderDT.Rows[0]["PLATENUMBER"].ToString();
            userRequestCriteria.ORDER_IDENTITY = OrderDT.Rows[0]["ORDER_IDENTITY"].ToString();
            userRequestCriteria.firstName = OrderDT.Rows[0]["FIRSTNAME"].ToString();
            userRequestCriteria.lastName = OrderDT.Rows[0]["LASTNAME"].ToString();
            userRequestCriteria.CountryCode = OrderDT.Rows[0]["COUNTRY_CODE"] is DBNull ? 0 : Convert.ToInt32(OrderDT.Rows[0]["COUNTRY_CODE"].ToString());
            userRequestCriteria.phoneNumber = OrderDT.Rows[0]["PHONENUMBER"].ToString();
            userRequestCriteria.ORDERTYPE_ID = OrderDT.Rows[0]["ORDERTYPE_ID"]  is DBNull ? 0 : Convert.ToInt32(OrderDT.Rows[0]["ORDERTYPE_ID"].ToString());
            userRequestCriteria.ORDERTYPE_NAME = OrderDT.Rows[0]["ORDERTYPE_NAME"].ToString();
            userRequestCriteria.VEHICLE_ID = OrderDT.Rows[0]["VEHICLE_ID"]  is DBNull ? 0 :  Convert.ToInt32(OrderDT.Rows[0]["VEHICLE_ID"].ToString());
            userRequestCriteria.ORDER_STATUS = OrderDT.Rows[0]["ORDER_STATUS"] is DBNull ? 0 : Convert.ToInt32(OrderDT.Rows[0]["ORDER_STATUS"].ToString());
            userRequestCriteria.ISACTIVE = OrderDT.Rows[0]["ISACTIVE"] is DBNull ? 0 : Convert.ToInt32(OrderDT.Rows[0]["ISACTIVE"].ToString());
            userRequestCriteria.OFFERS_COUNT = OrderDT.Rows[0]["OFFERS_COUNT"]  is DBNull ? 0 : Convert.ToInt32(OrderDT.Rows[0]["OFFERS_COUNT"].ToString());
            if (language == Messages.language.ar.ToString())
            {
                userRequestCriteria.BRANDNAME = OrderDT.Rows[0]["BRANDNAME_AR"].ToString();
                userRequestCriteria.MODELNAME = OrderDT.Rows[0]["MODELNAME_AR"].ToString();
            }
            else
            {
                userRequestCriteria.BRANDNAME = OrderDT.Rows[0]["BRANDNAME_EN"].ToString();
                userRequestCriteria.MODELNAME = OrderDT.Rows[0]["MODELNAME_EN"].ToString();
            }
            userRequestCriteria.carIMAGE = OrderDT.Rows[0]["carImage"].ToString();
            userRequestCriteria.userIMAGE = OrderDT.Rows[0]["userImage"].ToString();
            userRequestCriteria.Note = OrderDT.Rows[0]["NOTE"].ToString();
            userRequestCriteria.COLORNAME = OrderDT.Rows[0]["COLOR_NAME"].ToString();

            userRequestCriteria.lat = OrderDT.Rows[0]["lat"] is DBNull ? 0 : Convert.ToDouble(OrderDT.Rows[0]["lat"].ToString());
            userRequestCriteria.lng = OrderDT.Rows[0]["lng"] is DBNull ? 0 : Convert.ToDouble(OrderDT.Rows[0]["lng"].ToString());


            if (lat == 0 || lng == 0 || userRequestCriteria.lat == 0 || userRequestCriteria.lng == 0)
            {
                userRequestCriteria.time = "0";
                userRequestCriteria.distance = 0;
            }
            else
            {
                double distanceMiles1 = Math.Sqrt(
                     Math.Pow((lat - userRequestCriteria.lat) * 69, 2) +
                     Math.Pow((lng - userRequestCriteria.lng) * 69.172, 2));
                userRequestCriteria.distance = Math.Round(distanceMiles1 * 1.60934, 2);
                userRequestCriteria.time = new ServicesRepository(language).getTime(lat + "," + lng, userRequestCriteria.lat + "," + userRequestCriteria.lng);
            }
                


            if (OfferDT.Rows.Count > 0)
            {


                offersDTO.OFFER_ID = OfferDT.Rows[0]["OFFER_ID"] is DBNull ? 0 : Convert.ToInt32(OfferDT.Rows[0]["OFFER_ID"]);
                offersDTO.offerStatus = OfferDT.Rows[0]["OFFER_STATUS"] is DBNull ? 0: Convert.ToInt32(OfferDT.Rows[0]["OFFER_STATUS"]);
                offersDTO.ORDER_ID = OfferDT.Rows[0]["ORDER_ID"] is DBNull ? 0 : Convert.ToInt32(OfferDT.Rows[0]["ORDER_ID"]);
                offersDTO.PRICE = OfferDT.Rows[0]["PRICE"] is DBNull ? 0 : Convert.ToDouble(OfferDT.Rows[0]["PRICE"]);
                offersDTO.SUPPLIER_Name = OfferDT.Rows[0]["SUPPLIER_Name"] is DBNull ? "" : OfferDT.Rows[0]["SUPPLIER_Name"].ToString();
                offersDTO.timeValue = OfferDT.Rows[0]["timeValue"] is DBNull ? 0 : Convert.ToInt32(OfferDT.Rows[0]["timeValue"]);
                offersDTO.timeFlag = OfferDT.Rows[0]["timeFlag"] is DBNull ? 0 : Convert.ToInt32(OfferDT.Rows[0]["timeFlag"]);
                offersDTO.supplierImage = OfferDT.Rows[0]["supplierImage"] is DBNull ? "" :  OfferDT.Rows[0]["supplierImage"].ToString();
                offersDTO.CountryCode = OfferDT.Rows[0]["COUNTRY_CODE"] is DBNull ? 0 : Convert.ToInt32(OfferDT.Rows[0]["COUNTRY_CODE"]);
                offersDTO.PHONENUMBER = OfferDT.Rows[0]["PHONENUMBER"] is DBNull ? "" : OfferDT.Rows[0]["PHONENUMBER"].ToString();
                offersDTO.lat = OfferDT.Rows[0]["LOCATIONX"] is DBNull ? 0 : Convert.ToDouble(OfferDT.Rows[0]["LOCATIONX"]);
                offersDTO.lng = OfferDT.Rows[0]["LOCATIONY"] is DBNull ? 0 : Convert.ToDouble(OfferDT.Rows[0]["LOCATIONY"]);
                offersDTO.Rating = OfferDT.Rows[0]["RATING"] is DBNull ? 0 : Convert.ToDouble(OfferDT.Rows[0]["RATING"]);
                
                offersDTO.RateType = new Enums().checkRateTypeWords(offersDTO.Rating,language);
                offersDTO.RateTypeId =(int)new Enums().checkRateType(offersDTO.Rating);


                if (lat == 0 || lng == 0 || offersDTO.lat == 0 || offersDTO.lng == 0)
                {
                    offersDTO.time = "0";
                    offersDTO.DISTANCE = 0;
                }
                else
                {

                    double distanceMiles2 = Math.Sqrt(
                     Math.Pow((lat - offersDTO.lat) * 69, 2) +
                     Math.Pow((lng - offersDTO.lng) * 69.172, 2));
                    offersDTO.DISTANCE = Math.Round(distanceMiles2 * 1.60934, 2);
                    offersDTO.time = new ServicesRepository(language).getTime(lat + "," + lng, offersDTO.lat + "," + offersDTO.lng);
                }
            }
            infoAssign info;
            List<infoAssign> infoAssigns = new List<infoAssign>();
            if (OrderDetailsAssign.Rows.Count >0)
            {
                
                foreach (System.Data.DataRow row in OrderDetailsAssign.Rows)
                {

                    info = new infoAssign();
                    if (language == Messages.language.ar.ToString())
                    {
                        info.key = row["LISTNAME_AR"].ToString();
                        info.value = row["CONTENTNAME_AR"].ToString();
                    }
                    else
                    {
                        info.key = row["LISTNAME_EN"].ToString();
                        info.value = row["CONTENTNAME_EN"].ToString();
                    }
                    infoAssigns.Add(info);
                }
            }
           

            userRequestCriteria.info = infoAssigns;
            Domain.Entities.Order.OrdersDetails orderDatails = new Domain.Entities.Order.OrdersDetails();
            orderDatails.order = userRequestCriteria;
            orderDatails.offer = offersDTO;

            


            return orderDatails;
        }



     

        //public WorkshopDb GetLastWorkshopLocation(int  user_id)
        //{
        //    AccidentQuery accidentQuery = new AccidentQuery();
        //    System.Data.DataTable dataTable = conn_db.ReadTable(accidentQuery.GetLastWorkshopLocation(user_id));
        //    WorkshopDb WorkshopLocation = new WorkshopDb();

        //    if (dataTable.Rows.Count == 0)
        //        throw new EmptyViewException("بيانات الورشة غير موجودة", "workshop data are not exist");

        //    WorkshopLocation = new WorkshopDb();
        //    WorkshopLocation.LocationX = Convert.ToDouble(dataTable.Rows[0]["LOCATIONX"]);
        //    WorkshopLocation.LocationY = Convert.ToDouble(dataTable.Rows[0]["LOCATIONY"]);

        //    return WorkshopLocation;
        //}

        

        public List<string> GetOrderAllMedia(int order_id)
        {
            OrderQuery accidentQuery = new OrderQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(accidentQuery.GetOrderAllMedia(order_id));
              

            List<string> images = new List<string>();

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                images.Add(row["PIC_URL"].ToString());
            }
            return images;

        }


        public List<string> GetAccidentImages(int accident_id)
              {
            OrderQuery accidentQuery = new OrderQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(accidentQuery.GetAccidentPictures(accident_id));
            
            List<string> images = new List<string>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {

                images.Add(row["PIC_URL"].ToString());
               
            }
            return images;

        }



        public List<string> GetAccidentVideos(int accident_id)
        {
            OrderQuery accidentQuery = new OrderQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(accidentQuery.GetAccidentVideos(accident_id) );

            List<string> images = new List<string>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {

                images.Add(row["PIC_URL"].ToString());

            }
            return images;

        }


        public List<OrderType> GetOrderType(string lang)
        {
            OrderQuery orderQuery = new OrderQuery(language);

            
            System.Data.DataTable dataTable = conn_db.ReadTable(orderQuery.GetOrderTypeWithControls());

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            OrderType orderType;
            control control;
            List<OrderType> orderTypes = new List<OrderType>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                orderType = new OrderType();
                control = new control();
                orderType.id = Convert.ToInt32(row["id"].ToString());
                orderType.OrderTypeName = row["ordertype_Name"].ToString();
                orderType.OrderTypeIcon = row["ORDERTYPE_ICON"].ToString();

                control.PHOTO =Convert.ToBoolean( row["PHOTO"] );
                control.LOCATION = Convert.ToBoolean(row["LOCATION"]);
                control.CITY  = Convert.ToBoolean(row["CITY"]);
                control.CAR = Convert.ToBoolean(row["CAR"]);
                control.INFOWINDOW  = Convert.ToBoolean(row["INFOWINDOW"]);


                orderType.controls = control;
                orderTypes.Add(orderType);
            }
            return orderTypes;


        }



        public bool ResetReadyToFixFlag(int accident_id)
        {
            
            return new OrderQuery(language).ResetReadyToFixFlag(accident_id);
            
        }

        public bool DeleteRequestByOrderId(int order_id)
        {
           

            return new OrderQuery(language).DeleteRequestByOrderId(order_id);

        }

        public bool SetReadyToFixFlag(int accident_id)
        {


            return new OrderQuery(language).SetReadyToFixFlag(accident_id);
          
        }
        public List<DamagesType> GetDamagesType()
        {
            OrderQuery accidentQuery = new OrderQuery(language);
           

                System.Data.DataTable dataTable = conn_db.ReadTable(accidentQuery.GetAllObjects("DAMAGES_TYPE"));
                DamagesType damagesType = new DamagesType();
                List<DamagesType> damagesTypes = new List<DamagesType>();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

                    foreach (System.Data.DataRow row in dataTable.Rows)
                    {
                        damagesType = new DamagesType();
                        damagesType.id = Convert.ToInt32(row["id"].ToString());
                        damagesType.DamagesType_AR = row["DAMAGE_NAME_AR"].ToString();
                        damagesType.DamagesType_EN = row["DAMAGE_NAME_EN"].ToString();
                        damagesTypes.Add(damagesType);
                    }
                    return damagesTypes;
               
        }

        public  control  GetControls(int orderType_id)
        {
            OrderQuery OrderQuery = new OrderQuery(language);


            System.Data.DataTable dataTable = conn_db.ReadTable(OrderQuery.GetControls(orderType_id));
            control control = new control();
         

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                control = new control();
                control.PHOTO = Convert.ToBoolean(row["PHOTO"] );
                control.LOCATION  = Convert.ToBoolean(row["LOCATION"] );
                control.CITY  = Convert.ToBoolean(row["CITY"] );
                control.CAR  = Convert.ToBoolean(row["CAR"] );
                control.INFOWINDOW  = Convert.ToBoolean(row["INFOWINDOW"] );

                 
            }
            return control;

        }

        public control GetOrderListTitles(int orderType_id)
        {
            OrderQuery OrderQuery = new OrderQuery(language);


            System.Data.DataTable dataTable = conn_db.ReadTable(OrderQuery.GetOrderListTitle(orderType_id));
            control control = new control();


            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                control = new control();
                control.PHOTO = Convert.ToBoolean(row["PHOTO"] );
                control.LOCATION = Convert.ToBoolean(row["LOCATION"] );
                control.CITY = Convert.ToBoolean(row["CITY"] );
                control.CAR = Convert.ToBoolean(row["CAR"] );
                control.INFOWINDOW = Convert.ToBoolean(row["INFOWINDOW"] );


            }
            return control;

        }


        public List<InfoWindow> GetOrderListDetails(int orderType_id)
        {
            OrderQuery OrderQuery = new OrderQuery(language);


             System.Data.DataTable dataTable = conn_db.ReadTable(OrderQuery.GetOrderListDetailsByType(orderType_id));
            System.Data.DataTable OrderList = conn_db.ReadTable(OrderQuery.GetOrderListTitle(orderType_id));
             orderListDetail orderListDetail  = new  orderListDetail ();
            List<orderListDetail> orderListDetails;


            InfoWindow info  ;
            List<InfoWindow> infoWindows = new List<InfoWindow>();
             

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);
            foreach (System.Data.DataRow rowList in OrderList.Rows)
            {
                orderListDetails = new List<orderListDetail>();
                info =   new InfoWindow();
                info.List_id = Convert.ToInt32(rowList["ID"]);
                if (language == Messages.language.ar.ToString())
                    info.Listname = rowList["LISTNAME_AR"].ToString();
               else
                    info.Listname = rowList["LISTNAME_EN"].ToString();


                foreach (System.Data.DataRow row in dataTable.Rows)
                {
                    orderListDetail = new orderListDetail();
                     if (info.List_id == Convert.ToInt32(row["ORDERLIST_ID"]))
                    {
                        if (language == Messages.language.ar.ToString())
                            orderListDetail = new orderListDetail { id= Convert.ToInt32(row["ID"]),contentName = row["CONTENTNAME_AR"].ToString() } ;
                        else
                           orderListDetail = new orderListDetail { id = Convert.ToInt32(row["ID"]), contentName = row["CONTENTNAME_EN"].ToString() } ;


                        orderListDetails.Add(orderListDetail);
                    }



                    
                }


                info.contents = orderListDetails;
              
                infoWindows.Add(info);

            }
            return infoWindows;

        }


        public bool InsertOrderData(CarInfoOrder carInfoOrder, string user_id)
        {
            return new OrderQuery(language).InsertOrderData(carInfoOrder, user_id);
        }

        public bool InsertOrderDetailAssign(int order_id, int order_detail_assign_id)
        {

            return new OrderQuery(language).InsertOrderDetailAssign(order_id, order_detail_assign_id);
        }

        public bool UpdateAccidentData(CarInfoOrder carInfoOrder)
        {
            
            return new OrderQuery(language).UpdateAccidentData(carInfoOrder);
        }



        public bool InsertOrderPictureUrl(string imageUrl, int imageType, int accidentId)
        {
            
            return new OrderQuery(language).InsertOrderImageUrl(imageUrl, imageType, accidentId);
           

        }


       


    }
}
