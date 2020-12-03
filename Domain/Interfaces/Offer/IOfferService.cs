using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using HelperClass;
using Login;

namespace Domain.Interfaces.Offer
{
    public interface IOfferService
    {
        //POSTS
        Response CreateRequest(RequestCriteria requestCriteria);
        Response AcceptOffer(int offer_id);
        Response CreateOffer(int user_id ,Domain.Entities.Offer offer);
        Response DeleteOffer(int offer_id);
        Response ConfirmDeliverCar(GetCriteria getCriteria);
        Response ConfirmFinishedRepair(int offer_id);
        Response ConfirmRepair(int offer_id);

        //GETS
        //Response GetPersonOffers(OfferCriteria offerCriteria);
        Response GetAllRequests(string accessToken);
        
        Response GetWorkshopOffers(string accessToken);
        Response GetWorkshopOffersAccepted(string accessToken);
        Response GetWorkshopOffersIsFixing(string accessToken);
        Response GetWorkshopOffersFinishFix(string accessToken);
        Response GetSupplierStats(string accessToken);
        Response GetAreaByCity(int cityId);
        Response GetCity(string lang);
        //Response GetPersonRequests(string accessToken);
    }
}
