using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelperClass;
using Service.UserService;
using Domain.Entities;
using Service.OfferService;
using Login;
using Domain.Messages;
using static Domain.Messages.Messages;
using Service.OrderService;

namespace IslahProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OfferController : ControllerBase
    {



        //------------------Post Functions ----------------------------------

        [ActionName("AcceptOffer")]
        [HttpPost]
        public ActionResult<Response> AcceptOffer([FromQuery(Name ="lang")] string lang ,  [FromBody] Offer offer)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(offer.accessToken))
                return new OfferService(lang).AcceptOffer(offer.offer_id);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }

        [ActionName("DeleteOffer")]
        [HttpPost]
        public ActionResult<Response> DeleteOffer([FromQuery(Name = "lang")] string lang, [FromBody] Offer offer)
        {
                //check AccessToken 
                if (new UserService(lang).CheckAccessToken(offer.accessToken))
                    return new OfferService(lang).DeleteOffer(offer.offer_id);
                else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));

        }

        [ActionName("RejectOffer")]
        [HttpPost]
        public ActionResult<Response> RejectByDeleteOffer([FromQuery(Name = "lang")] string lang, [FromBody] Offer offer)
        { 
                //check AccessToken 
                if (new UserService(lang).CheckAccessToken(offer.accessToken))
                    return new OfferService(lang).RejectOffer(offer.offer_id);
                else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));

        }


        [ActionName("RejectOfferChangeFlag")]
        [HttpPost]
        public ActionResult<Response> RejectByChangeFlagOffer([FromQuery(Name = "lang")] string lang, [FromBody] Offer offer)
        {
            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(offer.accessToken))
                return new OfferService(lang).RejectOffer(offer.offer_id);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));

        }


        [ActionName("ConfirmRepair")]
        [HttpPost]
        public ActionResult<Response> ConfirmRepair([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new OfferService(lang).ConfirmRepair(getCriteria.offer_id);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }


        [ActionName("ConfirmFinishedOrder")]
        [HttpPost] 
        public ActionResult<Response> ConfirmFinishedOrder([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new OfferService(lang).ConfirmFinishedRepair(getCriteria.offer_id);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }


        [ActionName("ConfirmDeliver")]
        [HttpPost]
        public ActionResult<Response> ConfirmDeliver([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new OfferService(lang).ConfirmDeliverCar(getCriteria);
            return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }

        [ActionName("CreateOffer")] 
        [HttpPost]
        public ActionResult<Response> CreateOffer([FromQuery(Name = "lang")] string lang, [FromBody] Offer offer)
        {

            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(offer.accessToken);
            if (user_id != null)
            {
                return new OfferService(lang).CreateOffer(Convert.ToInt32(user_id), offer);
            }
               
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }


        [ActionName("CreateRequest")]
        [HttpPost]
        public ActionResult<Response> CreateRequest( [FromQuery(Name ="lang")] string lang,  [FromBody] RequestCriteria requestCriteria)
        {
            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(requestCriteria.accessToken))
                return new OfferService(lang).CreateRequest(requestCriteria);
            else
                return new Response(false,  Messages.GetMessage(lang ,   TypeM.DEFAULT , defaultM.WRONG_ACCESS_TOKEN ));

        }




        //----------------Get Functions ----------------------reject

        [ActionName("GetArea")]
        [HttpPost]
        public ActionResult<Response> GetAreaByCity([FromQuery(Name = "lang")] string lang, [FromBody] Area area)
        {
            return new OfferService(lang).GetAreaByCity(area.CityId);
        }

        [ActionName("GetCity")]
        [HttpGet]
        public ActionResult<Response> GetCity([FromQuery(Name = "lang")] string lang)
        {
            return new OfferService(lang).GetCity(lang);
        }

       


        [HttpPost]
        [ActionName("GetOrderDetails")]
        public ActionResult<Response> GetOrderDetails([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            string user_id = new UserService(lang).CheckAccessTokenUser(getCriteria.accessToken);
            //check AccessToken 
            if (user_id != null )
                return new OrderService(lang).GetOrderDetails( user_id , getCriteria);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));


        }

      

        [HttpPost]
        [ActionName("GetWorkshopOffers")]
        public ActionResult<Response> GetWorkshopOffers([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {
            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new OfferService(lang).GetWorkshopOffers(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());
        }

        [HttpPost]
        [ActionName("GetWorkshopOffersAccepted")]
        public ActionResult<Response> GetWorkshopOffersAccepted([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new OfferService(lang).GetWorkshopOffersAccepted(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());

        }

        [HttpPost]
        [ActionName("GetWorkshopOffersIsFixing")]
        public ActionResult<Response> GetWorkshopOffersIsFixing([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {


            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new OfferService(lang).GetWorkshopOffersIsFixing(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());

        }

        [HttpPost]
        [ActionName("GetWorkshopOffersFinishFix")]
        public ActionResult<Response> GetWorkshopOffersFinishFix([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {


            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new OfferService(lang).GetWorkshopOffersFinishFix(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());

        }


        [HttpPost]
        [ActionName("GetSupplierStats")]
        public ActionResult<Response> GetSupplierStats([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(getCriteria.accessToken); 
            if (user_id !=null )
                return new OfferService(lang).GetSupplierStats(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));

        }

        [HttpPost]
        [ActionName("GetPersonStats")]
        public ActionResult<Response> GetPersonStats([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new OfferService(lang).GetPersonStats(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));

        }


        [HttpPost]
        [ActionName("GetPersonRequests")]
        public ActionResult<Response> GetPersonRequests([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {
            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(getCriteria.accessToken); 
            if (user_id != null)
                return new OrderService(lang).GetPersonRequests(user_id);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());

        }

        [HttpPost]
        [ActionName("GetPersonOffers")]
        public ActionResult<Response> GetPersonOffers([FromQuery(Name = "lang")] string lang, [FromBody] OfferCriteria offerCriteria)
        {
            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(offerCriteria.accessToken); 

            if (user_id != null)
                return new OfferService(lang).GetPersonOffers(offerCriteria ,Convert.ToInt32( user_id));
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());
        }


        [HttpPost]
        [ActionName("GetSupplierOffers")]
        public ActionResult<Response> GetSupplierOffers([FromQuery(Name = "lang")] string lang, [FromBody] OfferCriteria offerCriteria)
        {
            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(offerCriteria.accessToken);

            if (user_id != null)
                return new OfferService(lang).GetSupplierOffers(offerCriteria, Convert.ToInt32(user_id));
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());
        }



    }
}
