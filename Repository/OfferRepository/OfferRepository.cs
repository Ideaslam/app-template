using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using DbQueries;
using Domain.Entities.supplier;
using Domain.Interfaces.Offer;
using Domain.Exceptions;
using Domain.Entities.Offers;
using Domain.Enums;
using Domain.Entities.Person;
using Domain.Entities.Order;
using Domain.Messages;
using static Domain.Enums.Enums;
using static Domain.Messages.Messages;
using Domain.Entities.Filter;
using Repository.ServicesRepository;

namespace OfferRepository
{
    public class OfferRepository : Domain.Interfaces.Offer.IOfferRepository
    {

        Common conn_db = new Common();

        string language = "en"; 

        public OfferRepository (string language)
        {
            this.language = language;
        }



        // Inserts
        public bool InsertOffer(int user_id , Offer offer)
        {
            return new OfferQuery(language).InsertOffer(user_id , offer);
        }
        public bool InsertPermissionArea(int accident_id, int area_id)
        {
            return new OfferQuery(language).InsertPermissionArea(accident_id, area_id);
        }

        public bool InsertPermissionCity(int accident_id, int city_id)
        {
            return new OfferQuery(language).InsertPermissionCity(accident_id, city_id);
        }

        // updates
        public bool UpdateConfirmOffer(int offer_id)
        {
            return new OfferQuery(language).UpdateConfirmOffer(offer_id);
        }

        public bool UpdateWaitingFix(int offer_id)
        {
            return new OfferQuery(language).UpdateWaitingFix_sp(offer_id);
        }

        public bool UpdateFinishedFlag(int offer_id)
        { 
            return new OfferQuery(language).UpdateFinishedFlag(offer_id);
        }

        public bool UpdateDeliveredFlag(int offer_id)
        { 
            return new OfferQuery(language).UpdateDeliveredFlag(offer_id);
        }

        public bool UpdateReadyToFixFlag(int accident_id)
        { 
            return new OfferQuery(language).UpdateReadyToFixFlag(accident_id);
        }

        public bool RejectOffer(int offer_id)
        {
            return new OfferQuery(language).RejectOffer(offer_id);
        }

        // Deletes
        public bool DeleteOffer(int offer_id)
        { 
            return new OfferQuery(language).DeleteOffer(offer_id);
        }




        public bool DeleteIndustrialPermission(int accident_id)
        {
            return new OfferQuery(language).DeleteIndustrialPermission(accident_id);
        }



        // Gets 
     

        public List<WarshaOffersCriteria> GetOffersByWarsha_id(int warsha_id, Enums.OfferType offerType)
        {


            OfferQuery offerQuery = new OfferQuery(language);
            System.Data.DataTable dataTable = new System.Data.DataTable();

            if (offerType == Enums.OfferType.offerNotAccepted)
            {
                dataTable = conn_db.ReadTable(offerQuery.GetWorkshopOfferNotAccespted(warsha_id));
            }
            else if (offerType == Enums.OfferType.offerAccepted)
            {
                dataTable = conn_db.ReadTable(offerQuery.GetWorkshopOfferAccespted(warsha_id));
            }
            else if (offerType == Enums.OfferType.offerIsFixing)
            {
                dataTable = conn_db.ReadTable(offerQuery.GetWorkshopOfferIsFixing(warsha_id));
            }
            else if (offerType == Enums.OfferType.offerFinishFixing)
            {
                dataTable = conn_db.ReadTable(offerQuery.GetWorkshopOfferFinishFixing(warsha_id));
            }

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            WarshaOffersCriteria warshaOffersCriteria = new WarshaOffersCriteria();
            List<WarshaOffersCriteria> warshaOffersCriterias = new List<WarshaOffersCriteria>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                warshaOffersCriteria = new WarshaOffersCriteria();
                warshaOffersCriteria.offer_id = Convert.ToInt32(row["OFFER_ID"].ToString());
                warshaOffersCriteria.workshop_id = Convert.ToInt32(row["WORKSHOP_ID"].ToString());
                warshaOffersCriteria.accident_Id = Convert.ToInt32(row["accidentId"].ToString());
                warshaOffersCriteria.Price = row["PRICE"].ToString();
                try { warshaOffersCriteria.timeValue = Convert.ToInt32(row["timeValue"]); } catch (Exception ex) { warshaOffersCriteria.timeValue = 0; }
                try { warshaOffersCriteria.timeFlag = Convert.ToInt32(row["timeFlag"]); } catch (Exception ex) { warshaOffersCriteria.timeFlag = 0; }
                try { warshaOffersCriteria.offerDateTime = Convert.ToDateTime(row["offer_DateTime"].ToString()).ToString("dd-MM-yyyy"); }
                catch { warshaOffersCriteria.offerDateTime = "01-01-2000"; }


                //IMPORTANT
                //if(offerType == Enums.OfferType.offerIsFixing)
                //{
                //    DateTime fixingStartDate =  Convert.ToDateTime(row["WAITINGFIX_DATETIME"].ToString()).AddDays( Convert.ToInt32(warshaOffersCriteria.workDays+1) );
                //    DateTime Nowaday = DateTime.Now ;
                //    TimeSpan noOfDays = fixingStartDate -Nowaday ;
                //    if(noOfDays.Days<0)
                //    {
                //        warshaOffersCriteria.workDays = 0;
                //    }
                //    else
                //    {
                //        DateTime age = DateTime.MinValue + noOfDays;
                //        int days = age.Day - 1;
                //        warshaOffersCriteria.workDays = days;
                //    }
                    
                //}


                warshaOffersCriteria.plateNumber = row["plateNumber"].ToString();
                warshaOffersCriteria.Status = Convert.ToInt32(row["Status"].ToString());
                warshaOffersCriteria.statusNameEn = row["STATUS_NAME_EN"].ToString();
                warshaOffersCriteria.statusNameAr = row["STATUS_NAME_AR"].ToString();


                warshaOffersCriterias.Add(warshaOffersCriteria);
            }
            return warshaOffersCriterias;

        }

        public Stats GetSupplierStats(int workshop_id)
        {


            OfferQuery offerQuery = new OfferQuery(language);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = conn_db.ReadTable(offerQuery.GetSupplierStats(workshop_id));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            Stats stats = new Stats();

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                //if (row["OFFERTYPE"].ToString() == Enums.OfferType.offerNotAccepted.ToString())
                //    try { stats.offerNotAccepted = Convert.ToInt32(row["OFFERCOUNT"].ToString()); } catch { stats.offerNotAccepted = 0; }

                //if (row["OFFERTYPE"].ToString() == Enums.OfferType.offerAccepted.ToString())
                //    try { stats.offerAccepted = Convert.ToInt32(row["OFFERCOUNT"].ToString()); } catch { stats.offerAccepted = 0; }

                //if (row["OFFERTYPE"].ToString() == Enums.OfferType.offerIsFixing.ToString())
                //    try { stats.offerIsFixing = Convert.ToInt32(row["OFFERCOUNT"].ToString()); } catch { stats.offerIsFixing = 0; }

                //if (row["OFFERTYPE"].ToString() == Enums.OfferType.offerFinishFixing.ToString())
                //    try { stats.offerFinishFixing = Convert.ToInt32(row["OFFERCOUNT"].ToString()); } catch { stats.offerFinishFixing = 0; }

                stats.CONFIRMED = Convert.ToInt32(row["ORDERCOUNT"].ToString());
            }
            return stats;

        }

        public PersonStats GetPersonStats(int user_id)
        {


            OfferQuery offerQuery = new OfferQuery(language);
            System.Data.DataTable dataTable = new System.Data.DataTable();
            dataTable = conn_db.ReadTable(offerQuery.GetPersonOfferCount(user_id));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            PersonStats personStats  = new PersonStats();

            foreach (System.Data.DataRow row in dataTable.Rows)
            {
  
                personStats.AvailableOffers = Convert.ToInt32(row[0]);
            }
            return personStats;

        }

        public List<WarshaRequestDTO> GetAllRequestsAreaPermission(int workshop)
        {
            OfferQuery offerQuery = new OfferQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(offerQuery.GetAllRequestsAreaPermission(workshop));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            WarshaRequestDTO workshopRequestDTO = new WarshaRequestDTO();
            List<WarshaRequestDTO> workshopRequests = new List<WarshaRequestDTO>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {

                workshopRequestDTO = new WarshaRequestDTO();
                workshopRequestDTO.ACCIDENT_ID = Convert.ToInt32(row["ACCIDENT_ID"].ToString());
                workshopRequestDTO.FULLNAME = row["FULLNAME"].ToString();
                workshopRequestDTO.PLATENUMBER = row["PLATENUMBER"].ToString();
                workshopRequestDTO.MANUFACTURER = row["MANUFACTURER"].ToString();
                workshopRequestDTO.MODEL = row["MODEL"].ToString();
                workshopRequestDTO.FOUNDDATE = row["FOUNDDATE"].ToString();
                workshopRequestDTO.COLOR = row["COLOR"].ToString();
                workshopRequestDTO.paper_NO = row["paper_NO"].ToString();
                workshopRequestDTO.LocationX = Convert.ToDouble(row["LocationX"].ToString());
                workshopRequestDTO.LocationY = Convert.ToDouble(row["LocationY"].ToString());
                workshopRequestDTO.Image = row["url"].ToString();
                try
                {
                    workshopRequestDTO.ACCIDENTDATE = Convert.ToDateTime(row["AccidentDate"].ToString()).ToString("dd-MM-yyyy h:mm tt");
                }
                catch
                {
                    workshopRequestDTO.ACCIDENTDATE = "01-01-2000";
                }

                workshopRequestDTO.Status = Convert.ToInt32(row["Status"].ToString());
                workshopRequestDTO.statusNameAr = row["status_Name_Ar"].ToString();
                workshopRequestDTO.statusNameEn = row["status_Name_En"].ToString();

                workshopRequests.Add(workshopRequestDTO);
            }
            return workshopRequests;
        }

        public List<WarshaRequestDTO> GetAllRequestsCityPermission(int workshop)
        {
            OfferQuery offerQuery = new OfferQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(offerQuery.GetAllRequestsCityPermission(workshop));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            WarshaRequestDTO workshopRequestDTO = new WarshaRequestDTO();
            List<WarshaRequestDTO> workshopRequests = new List<WarshaRequestDTO>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {

                workshopRequestDTO = new WarshaRequestDTO();
                workshopRequestDTO.ACCIDENT_ID = Convert.ToInt32(row["ACCIDENT_ID"].ToString());
                workshopRequestDTO.FULLNAME = row["FULLNAME"].ToString();
                workshopRequestDTO.PLATENUMBER = row["PLATENUMBER"].ToString();
                workshopRequestDTO.MANUFACTURER  = row["MANUFACTURER"].ToString();
                workshopRequestDTO.MODEL = row["MODEL"].ToString();
                workshopRequestDTO.FOUNDDATE = row["FOUNDDATE"].ToString();
                workshopRequestDTO.COLOR = row["COLOR"].ToString();
                workshopRequestDTO.paper_NO = row["paper_NO"].ToString();
                workshopRequestDTO.LocationX = Convert.ToDouble(row["LocationX"].ToString());
                workshopRequestDTO.LocationY = Convert.ToDouble(row["LocationY"].ToString());
                workshopRequestDTO.Image = row["url"].ToString();
                try
                {
                    workshopRequestDTO.ACCIDENTDATE = Convert.ToDateTime(row["AccidentDate"].ToString()).ToString("dd-MM-yyyy h:mm tt");
                }
                catch
                {
                    workshopRequestDTO.ACCIDENTDATE = "01-01-2000";
                }

                workshopRequestDTO.Status = Convert.ToInt32(row["Status"].ToString());
                workshopRequestDTO.statusNameAr = row["status_Name_Ar"].ToString();
                workshopRequestDTO.statusNameEn = row["status_Name_En"].ToString();

                workshopRequests.Add(workshopRequestDTO);
            }
            return workshopRequests;
        }

 

        public List<UserOffer> GetSupplierOffers(OfferCriteria offerCriteria   ,int user_id)
        {


            OfferQuery offerQuery = new OfferQuery(language);
            OrderQuery orderQuery  = new OrderQuery(language);
            System.Data.DataTable OfferDT = new System.Data.DataTable();
     

            switch (offerCriteria.offerType)
            {
                case (int)OfferType.offerNotAccepted :
                    OfferDT = conn_db.ReadTable(offerQuery.GetsupplierOffersUnConfirmed(user_id, language));
                 break;
                case (int)OfferType.offerAccepted:
                    OfferDT = conn_db.ReadTable(offerQuery.GetsupplierOffersConfirmed(user_id, language));
                    break;
                case (int)OfferType.offerIsFixing:
                    OfferDT = conn_db.ReadTable(offerQuery.GetsupplierOffersStart(user_id, language));
                    break;
                case (int)OfferType.offerFinishFixing:
                    OfferDT = conn_db.ReadTable(offerQuery.GetsupplierOffersfinish(user_id, language));
                    break;
                default:
                    OfferDT = conn_db.ReadTable(offerQuery.GetsupplierOffersUnConfirmed(user_id, language));
                break;
            }
          

            UserOffer userOffer;
            List<UserOffer> userOffers = new List<UserOffer>();

            if (OfferDT.Rows.Count == 0)
                throw new EmptyViewException(language, Messages.GetMessage(language, TypeM.OFFER, offerM.OFFER_NOT_FOUND));

            foreach (System.Data.DataRow dataRow in OfferDT.Rows)
            {




                userOffer = new UserOffer();
                userOffer.ORDERTYPE_NAME = dataRow["ORDERTYPE_NAME"] is DBNull ? "" : dataRow["ORDERTYPE_NAME"].ToString();
                userOffer.colorname = dataRow["COLOR_NAME"] is DBNull ? "" : dataRow["COLOR_NAME"].ToString();

                userOffer.firstName =   dataRow["FIRSTNAME"].ToString();
                userOffer.lastName =   dataRow["LASTNAME"].ToString();
                userOffer.carImage =   dataRow["CARIMAGE"].ToString();
                userOffer.userImage =   dataRow["userIMAGE"].ToString();

                if (language == Messages.language.ar.ToString())
                {
                    userOffer.brandname = dataRow["BRANDNAME_AR"].ToString();
                    userOffer.modelname = dataRow["MODELNAME_AR"].ToString();
                }
                else
                {
                    userOffer.brandname = dataRow["BRANDNAME_EN"].ToString();
                    userOffer.modelname = dataRow["MODELNAME_EN"].ToString();
                }


                userOffer.OFFER_ID = dataRow["OFFER_ID"] is DBNull ? 0 : Convert.ToInt32(dataRow["OFFER_ID"]);
                userOffer.ORDER_ID = dataRow["ORDER_ID"] is DBNull ? 0 : Convert.ToInt32(dataRow["ORDER_ID"]);
                userOffer.PRICE = dataRow["PRICE"] is DBNull ? 0 : Convert.ToDouble(dataRow["PRICE"]);
                userOffer.SUPPLIER_Name = dataRow["SUPPLIER_Name"] is DBNull ? "" : dataRow["SUPPLIER_Name"].ToString();
                userOffer.timeValue = dataRow["timeValue"] is DBNull ? 0 : Convert.ToInt32(dataRow["timeValue"]);
                userOffer.timeFlag = dataRow["timeFlag"] is DBNull ? 0 : Convert.ToInt32(dataRow["timeFlag"]);
                userOffer.supplierImage = dataRow["supplierImage"] is DBNull ? "" : dataRow["supplierImage"].ToString();
                userOffer.PHONENUMBER = dataRow["PHONENUMBER"] is DBNull ? "" : dataRow["PHONENUMBER"].ToString();
                userOffer.lat = dataRow["LOCATIONX"] is DBNull ? 0 : Convert.ToDouble(dataRow["LOCATIONX"]);
                userOffer.lng = dataRow["LOCATIONY"] is DBNull ? 0 : Convert.ToDouble(dataRow["LOCATIONY"]);
                userOffer.Rating = dataRow["RATING"] is DBNull ? 0 : Convert.ToDouble(dataRow["RATING"]);
                userOffer.offerStatus = dataRow["offer_status"] is DBNull ? 0 : Convert.ToInt32(dataRow["offer_status"]);
                
                userOffer.RateType = new Enums().checkRateTypeWords(userOffer.Rating, language);
                userOffer.RateTypeId = (int)new Enums().checkRateType(userOffer.Rating);

                if (offerCriteria.lat == 0 || offerCriteria.lng == 0 || userOffer.lat == 0 || userOffer.lng == 0)
                {
                    userOffer.DISTANCE = 0;
                    userOffer.time = "0";
                }
                    
                else
                {
                    double distanceMiles = Math.Sqrt(
                   Math.Pow((offerCriteria.lat - userOffer.lat) * 69, 2) +
                   Math.Pow((offerCriteria.lng - userOffer.lng) * 69.172, 2));
                    userOffer.DISTANCE = Math.Round(distanceMiles * 1.60934, 2);
                    userOffer.time = new ServicesRepository(language).getTime(offerCriteria.lat + "," + offerCriteria.lng, userOffer.lat + "," + userOffer.lng);
                }

                userOffers.Add(userOffer);
            }
            return userOffers;
        }


        public List<UserOffer> GetPrsonOffers(OfferCriteria offerCriteria , int user_id )
        {


            OfferQuery offerQuery = new OfferQuery(language);

            System.Data.DataTable OfferDT = conn_db.ReadTable(offerQuery.GetPersonOffersWithCondition(offerCriteria , language , user_id ));

            UserOffer userOffer;
            List<UserOffer> userOffers = new List<UserOffer>();

            if (OfferDT.Rows.Count == 0)
                throw new EmptyViewException(language, Messages.GetMessage(language, TypeM.OFFER, offerM.OFFER_NOT_FOUND));

            foreach (System.Data.DataRow dataRow in OfferDT.Rows)
            {




                userOffer = new UserOffer();
                userOffer.ORDERTYPE_NAME = dataRow["ORDERTYPE_NAME"] is DBNull ? "" : dataRow["ORDERTYPE_NAME"].ToString();
                userOffer.colorname = dataRow["COLOR_NAME"] is DBNull ? "" : dataRow["COLOR_NAME"].ToString();

                if (language == Messages.language.ar.ToString())
                {
                    userOffer.brandname = dataRow["BRANDNAME_AR"].ToString();
                    userOffer.modelname = dataRow["MODELNAME_AR"].ToString();
                }
                else
                {
                    userOffer.brandname = dataRow["BRANDNAME_EN"].ToString();
                    userOffer.modelname = dataRow["MODELNAME_EN"].ToString();
                }

               
                userOffer.OFFER_ID = dataRow["OFFER_ID"] is DBNull ? 0 : Convert.ToInt32(dataRow["OFFER_ID"]);
                userOffer.ORDER_ID = dataRow["ORDER_ID"] is DBNull ? 0 : Convert.ToInt32(dataRow["ORDER_ID"]);
                try
                {
                    userOffer.ORDER_DATE = Convert.ToDateTime(dataRow["ORDERDATE"].ToString()).ToString("dd-MM-yyyy");
                }
                catch (Exception ex)
                {
                    userOffer.ORDER_DATE = "";
                }
                userOffer.ORDER_NO = dataRow["ORDER_IDENTITY"] is DBNull ? 0 : Convert.ToInt64(dataRow["ORDER_IDENTITY"]);
                userOffer.PRICE = dataRow["PRICE"] is DBNull ? 0 : Convert.ToDouble(dataRow["PRICE"]);
                userOffer.SUPPLIER_Name = dataRow["SUPPLIER_Name"] is DBNull ? "" : dataRow["SUPPLIER_Name"].ToString();
                userOffer.timeValue = dataRow["timeValue"] is DBNull ? 0 : Convert.ToInt32(dataRow["timeValue"]);
                userOffer.timeFlag = dataRow["timeFlag"] is DBNull ? 0 : Convert.ToInt32(dataRow["timeFlag"]);
                userOffer.supplierImage = dataRow["supplierImage"] is DBNull ? "" : dataRow["supplierImage"].ToString();
                userOffer.PHONENUMBER = dataRow["PHONENUMBER"] is DBNull ? "" : dataRow["PHONENUMBER"].ToString();
                userOffer.lat = dataRow["LOCATIONX"] is DBNull ? 0 : Convert.ToDouble(dataRow["LOCATIONX"]);
                userOffer.lng = dataRow["LOCATIONY"] is DBNull ? 0 : Convert.ToDouble(dataRow["LOCATIONY"]);
                userOffer.Rating = dataRow["RATING"] is DBNull ? 0 : Convert.ToDouble(dataRow["RATING"]);
                userOffer.Rater_No = dataRow["Rater_NO"] is DBNull ? 0 : Convert.ToInt32(dataRow["Rater_NO"]);
                userOffer.offerStatus = dataRow["offer_status"] is DBNull ? 0 : Convert.ToInt32(dataRow["offer_status"]);
                
                userOffer.RateType = new Enums().checkRateTypeWords(userOffer.Rating, language);
                userOffer.RateTypeId = (int)new Enums().checkRateType(userOffer.Rating);

                if (offerCriteria.lat == 0 || offerCriteria.lng == 0 || userOffer.lat == 0 || userOffer.lng == 0)
                {
                    userOffer.DISTANCE = 0;
                    userOffer.time = "0";
                }

                else
                {
                    double distanceMiles = Math.Sqrt(
                      Math.Pow((offerCriteria.lat - userOffer.lat) * 69, 2) +
                      Math.Pow((offerCriteria.lng - userOffer.lng) * 69.172, 2));
                    userOffer.DISTANCE = Math.Round(distanceMiles * 1.60934, 2);
                    userOffer.time = new ServicesRepository(language).getTime(offerCriteria.lat + "," + offerCriteria.lng, userOffer.lat + "," + userOffer.lng);
                }

                

                userOffers.Add(userOffer);   
            }
            return userOffers;
        }


        public List<OfferDTO> GetOffers(OfferCriteria offerCriteria)
        {

         
            OfferQuery offerQuery = new OfferQuery(language);
       
            System.Data.DataTable OfferDT = conn_db.ReadTable(offerQuery.GetOffersWithCondition(offerCriteria.filters, offerCriteria.sort));

            OfferDTO offersDTO;
            List<OfferDTO> Offers = new List<OfferDTO>();

            if (OfferDT.Rows.Count == 0)
                throw new EmptyViewException(language, Messages.GetMessage(language, TypeM.OFFER, offerM.OFFER_NOT_FOUND));

            foreach(System.Data.DataRow dataRow   in OfferDT.Rows)
            {

                offersDTO = new OfferDTO();
                offersDTO.OFFER_ID = dataRow["OFFER_ID"] is DBNull ? 0 : Convert.ToInt32(dataRow["OFFER_ID"]);
                offersDTO.ORDER_ID = dataRow["ORDER_ID"] is DBNull ? 0 : Convert.ToInt32(dataRow["ORDER_ID"]);
                offersDTO.PRICE = dataRow["PRICE"] is DBNull ? 0 : Convert.ToDouble(dataRow["PRICE"]);
                offersDTO.SUPPLIER_Name = dataRow["SUPPLIER_Name"] is DBNull ? "" : dataRow["SUPPLIER_Name"].ToString();
                offersDTO.timeValue = dataRow["timeValue"] is DBNull ? 0 : Convert.ToInt32(dataRow["timeValue"]);
                offersDTO.timeFlag = dataRow["timeFlag"] is DBNull ? 0 : Convert.ToInt32(dataRow["timeFlag"]);
                offersDTO.supplierImage = dataRow["supplierImage"] is DBNull ? "" : dataRow["supplierImage"].ToString();
                offersDTO.PHONENUMBER = dataRow["PHONENUMBER"] is DBNull ? "" : dataRow["PHONENUMBER"].ToString();
                offersDTO.lat = dataRow["LOCATIONX"] is DBNull ? 0 : Convert.ToDouble(dataRow["LOCATIONX"]);
                offersDTO.lng = dataRow["LOCATIONY"] is DBNull ? 0 : Convert.ToDouble(dataRow["LOCATIONY"]);
                offersDTO.Rating = dataRow["RATING"] is DBNull ? 0 : Convert.ToDouble(dataRow["RATING"]);
                offersDTO.offerStatus = dataRow["offer_status"] is DBNull ? 0 : Convert.ToInt32(dataRow["offer_status"]);
                
                offersDTO.RateType = new Enums().checkRateTypeWords(offersDTO.Rating,language);
                offersDTO.RateTypeId =(int) new Enums().checkRateType(offersDTO.Rating);

                if (offerCriteria.lat == 0 || offerCriteria.lng == 0 || offersDTO.lat == 0 || offersDTO.lng == 0)
                {
                    offersDTO.DISTANCE = 0;
                    offersDTO.time = "0";
                }

                else
                {
                    

                    double distanceMiles = Math.Sqrt(
                Math.Pow((offerCriteria.lat - offersDTO.lat) * 69, 2) +
                Math.Pow((offerCriteria.lng - offersDTO.lng) * 69.172, 2));
                    offersDTO.DISTANCE = Math.Round(distanceMiles * 1.60934, 2);
                    offersDTO.time = new ServicesRepository(language).getTime(offerCriteria.lat + "," + offerCriteria.lng, offersDTO.lat + "," + offersDTO.lng);
                }

             

                Offers.Add(offersDTO);


            }
           
             

            return Offers;
        }

        public List<IndustrialArea> GetAreaByCityId(int cityId)
        {
            OfferQuery offerQuery = new OfferQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(offerQuery.GetObjectByColname("IndustrialAreas", "CITY_ID", cityId));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            IndustrialArea industrialArea;
            List<IndustrialArea> listArea = new List<IndustrialArea>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                industrialArea = new IndustrialArea();

                industrialArea.id = Convert.ToInt32(row["id"].ToString());
                industrialArea.areaName = row["areaName"].ToString();
                industrialArea.areaNumber = row["areaNumber"].ToString();
                industrialArea.areaLocationX = row["LocationX"].ToString();
                industrialArea.areaLocationY = row["LocationY"].ToString();
                listArea.Add(industrialArea);
            }
            return listArea;


        }

        public OfferDetails GetOfferDetails(int accident_id)
        {
            OfferQuery offerQuery = new OfferQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(offerQuery.GetOfferDetailsByOrderId(accident_id));

            OfferDetails offerDetails = new OfferDetails();
            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            offerDetails = new OfferDetails();
            offerDetails.OFFER_ID = Convert.ToInt32(dataTable.Rows[0]["OFFER_ID"]);
            offerDetails.shop_ID = Convert.ToInt32(dataTable.Rows[0]["WORKSHOP_ID"]);
            offerDetails.FULLNAME = dataTable.Rows[0]["FULLNAME"].ToString();
            offerDetails.SHOPNUMBER = dataTable.Rows[0]["SHOPNUMBER"].ToString();
            offerDetails.SHOPNAME = dataTable.Rows[0]["SHOPNAME"].ToString();
            offerDetails.LOCATIONX_WORKSHOP = Convert.ToDouble(dataTable.Rows[0]["LOCATIONX"].ToString());
            offerDetails.LOCATIONY_WORKSHOP = Convert.ToDouble(dataTable.Rows[0]["LOCATIONY"].ToString());
            offerDetails.CONFIRMATION = Convert.ToInt32(dataTable.Rows[0]["CONFIRMATION"].ToString());
            offerDetails.timeValue = dataTable.Rows[0]["timeValue"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["timeValue"]);
            offerDetails.timeFlag = dataTable.Rows[0]["timeFlag"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["timeFlag"]);
            offerDetails.PRICE = Convert.ToDouble(dataTable.Rows[0]["PRICE"]);
            offerDetails.rating = Convert.ToDouble(dataTable.Rows[0]["RATING"]);
            offerDetails.workshop_image = dataTable.Rows[0]["IMAGE"].ToString();
            try { offerDetails.OFFER_DATETIME = Convert.ToDateTime(dataTable.Rows[0]["OFFER_DATETIME"]).ToString("dd-MM-yyyy"); }
            catch { offerDetails.OFFER_DATETIME = "01-01-2000"; }
            offerDetails.canFinish = Convert.ToInt32(dataTable.Rows[0]["WAITINGFIX"].ToString());
            offerDetails.isDelivered = Convert.ToInt32(dataTable.Rows[0]["FINISHEDFLAG"].ToString());

            offerDetails.AreaName = dataTable.Rows[0]["AreaName"].ToString();
            offerDetails.phonenumber = dataTable.Rows[0]["phoneNumber"].ToString();
            return offerDetails;
        }

        public OfferDetails GetOfferDetailsWorkShop(int accident_id, int workshop_id)
        {
            OfferQuery offerQuery = new OfferQuery(language);
            System.Data.DataTable dataTable = conn_db.ReadTable(offerQuery.GetOfferDetailsWorkShop(accident_id, workshop_id));
            OfferDetails offerDetails = new OfferDetails();

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);


            offerDetails = new OfferDetails();
            offerDetails.OFFER_ID = Convert.ToInt32(dataTable.Rows[0]["OFFER_ID"]);
            offerDetails.shop_ID = Convert.ToInt32(dataTable.Rows[0]["WORKSHOP_ID"]);
            offerDetails.FULLNAME = dataTable.Rows[0]["FULLNAME"].ToString();
            offerDetails.SHOPNUMBER = dataTable.Rows[0]["SHOPNUMBER"].ToString();
            offerDetails.SHOPNAME = dataTable.Rows[0]["SHOPNAME"].ToString();
            offerDetails.LOCATIONX_WORKSHOP = Convert.ToDouble(dataTable.Rows[0]["LOCATIONX"].ToString());
            offerDetails.LOCATIONY_WORKSHOP = Convert.ToDouble(dataTable.Rows[0]["LOCATIONY"].ToString());
            offerDetails.CONFIRMATION = Convert.ToInt32(dataTable.Rows[0]["CONFIRMATION"].ToString());
            offerDetails.timeValue = dataTable.Rows[0]["timeValue"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["timeValue"]);
            offerDetails.timeFlag = dataTable.Rows[0]["timeFlag"] is DBNull ? 0 : Convert.ToInt32(dataTable.Rows[0]["timeFlag"]);
            offerDetails.PRICE = Convert.ToDouble(dataTable.Rows[0]["PRICE"]);
            offerDetails.rating = Convert.ToDouble(dataTable.Rows[0]["RATING"]);
            offerDetails.workshop_image = dataTable.Rows[0]["IMAGE"].ToString();
            offerDetails.OFFER_DATETIME = Convert.ToDateTime(dataTable.Rows[0]["OFFER_DATETIME"]).ToString("dd-MM-yyyy");
            offerDetails.canFinish = Convert.ToInt32(dataTable.Rows[0]["WAITINGFIX"].ToString());
            offerDetails.isDelivered = Convert.ToInt32(dataTable.Rows[0]["FINISHEDFLAG"].ToString());

            offerDetails.AreaName =  dataTable.Rows[0]["AreaName"].ToString();
            offerDetails.phonenumber =  dataTable.Rows[0]["phoneNumber"].ToString();
            return offerDetails;
        }

        public List<City> GetCity(string lang)
        {
            OfferQuery offerQuery = new OfferQuery(language);

            System.Data.DataTable dataTable = conn_db.ReadTable(offerQuery.GetMasterTranslated("city",lang));

            if (dataTable.Rows.Count == 0)
                throw new EmptyViewException(language);

            City city;
            List<City> cities = new List<City>();
            foreach (System.Data.DataRow row in dataTable.Rows)
            {
                city = new City();
                city.CityId = Convert.ToInt32(row["id"].ToString());
                city.CityName = row["city_Name"].ToString();
                cities.Add(city);
            }
            return cities;


        }

       


    }
}
