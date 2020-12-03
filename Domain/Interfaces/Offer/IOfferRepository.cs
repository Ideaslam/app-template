using Domain.Entities;
using Domain.Entities.Offers;
using Domain.Entities.supplier;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Offer
{
   public  interface IOfferRepository 
    {
        
        //Posts
        bool InsertPermissionArea(int  accident_id ,int area_id);
        bool InsertOffer(int user_id ,Domain.Entities.Offer offer);
        bool UpdateWaitingFix(int offer_id);
        bool UpdateFinishedFlag(int offer_id);
        bool UpdateDeliveredFlag(int offer_id);
        bool UpdateReadyToFixFlag(int accident_id);
        bool UpdateConfirmOffer(int offer_id);
        bool DeleteOffer(int offer_id);

        //Gets
        //List<UserRequestCriteria> GetRequestsByUser_id(int user_id);
        List<WarshaOffersCriteria> GetOffersByWarsha_id(int warsha_id, Domain.Enums.Enums.OfferType offerType);
        Stats GetSupplierStats(int supplier_id);
        List<WarshaRequestDTO> GetAllRequestsAreaPermission(int workshop);
        //List<OffersDTO> GetOffersByAccidentId(OfferCriteria offerCriteria);
        List<IndustrialArea> GetAreaByCityId(int cityId);
        OfferDetails GetOfferDetails(int accident_id);
        OfferDetails GetOfferDetailsWorkShop(int accident_id, int workshop_id);
        List<City> GetCity(string lang);






    }
}
