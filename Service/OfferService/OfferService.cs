using System;
using System.Collections.Generic;
using System.Text;
using Domain.Entities;
using Domain.Entities.Order;
using Domain.Entities.Offers;
using Domain.Entities.Person;
using Domain.Entities.supplier;
using Domain.Enums;
using Domain.Exceptions;
using Domain.Interfaces.Offer;
using Domain.Messages;
using HelperClass;
using OfferRepository;
using Repository.HelperRepository;
using UserRepository;
using static Domain.Messages.Messages;
using Login;
using Domain.Entities.Filter;
using Repository.ServicesRepository;

namespace Service.OfferService
{
    public class OfferService : Domain.Interfaces.Offer.IOfferService
    {
        string language = "en"; 
        public OfferService(string language)
        {
            this.language = language;
        }

       
        //posts
        public Response AcceptOffer( int offer_id)
        {
            try
            {
                if (new OfferRepository.OfferRepository(language).UpdateConfirmOffer(offer_id))
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.OFFER_ACCEPT ));
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage , UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response ConfirmRepair( int offer_id)
        {
            try
            {
                if (new OfferRepository.OfferRepository(language).UpdateWaitingFix(offer_id))
                {

                    OrderUser orderUser = new UserRepository.UserRepository(language).GetPersonIdOfferId(offer_id)[0];
                    new ServicesRepository(language).SendNotiAllDevices(orderUser.userId, Messages.GetMessage(language, TypeM.notifyM, notifyM.NOTI_TITLE),
                        Messages.GetMessage(language,TypeM.notifyM, notifyM.NOTI_ORDER_STARTED )+ " order " + orderUser.OrderIdentity ,1); 

                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.CONFIRM_REPAIR));
                }
                    
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage,  UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response ConfirmFinishedRepair( int offer_id)
        {
            try
            {
                if (new OfferRepository.OfferRepository(language).UpdateFinishedFlag(offer_id))
                {
                    OrderUser orderUser = new UserRepository.UserRepository(language).GetPersonIdOfferId(offer_id)[0];
                    new ServicesRepository(language).SendNotiAllDevices(orderUser.userId, Messages.GetMessage(language, TypeM.notifyM, notifyM.NOTI_TITLE),
                        Messages.GetMessage(language, TypeM.notifyM, notifyM.NOTI_ORDER_FINISHED) + " order " + orderUser.OrderIdentity, 1);

                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.FINISH_REPAIR));
                }
                    
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage,  UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response ConfirmDeliverCar(GetCriteria getCriteria)
        {
            try
            {
                if (new OfferRepository.OfferRepository(language).UpdateDeliveredFlag(getCriteria.offer_id))
                {
                    try
                    {
                        int  userId = new UserRepository.UserRepository(language).GetSupplierUserIdByOfferId(getCriteria.offer_id) ;
                        new ServicesRepository(language).SendNotiAllDevices(userId, Messages.GetMessage(language, TypeM.notifyM, notifyM.NOTI_TITLE),
                            Messages.GetMessage(language, TypeM.notifyM, notifyM.NOTI_ORDER_DELIVERED)  , 1);

                        return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.CONFIRM_REPAIR));

                    }
                    catch
                    {

                    }

                   if( new UserRepository.UserRepository(language).SetRatingByOffer(getCriteria.offer_id , getCriteria.rate))
                    {
                        return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.CONFIRM_DELIVER));

                    }
                    else
                    {
                        return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.CONFIRM_DELIVER),"Rate NOT INSERTED");
                    }
                    
                }
                   
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage , UpdateException.ErrorMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response DeleteOffer( int offer_id)
        {
            try
            {
                if (new OfferRepository.OfferRepository(language).DeleteOffer(offer_id))
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.OFFER_DELETE) );
                else
                    throw new DeleteException(language);
            }
            catch (DeleteException DeleteException)
            {
                return new Response(false, DeleteException.RespMessage , DeleteException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response RejectOffer( int offer_id)
        {
            try
            {
                if (new OfferRepository.OfferRepository(language).RejectOffer(offer_id))
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.OFFER_REJECT));
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false, UpdateException.RespMessage , UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT,Messages.defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }



        public Response CreateOffer(int  user_id , Offer offer)
        {
            try
            {
               int isActive = new UserRepository.UserRepository(language).GetProfileByUserId(user_id.ToString(), language, (int)Enums.UserType.workshop).isActive;

                if (isActive == 0)
                {
                    new ServicesRepository(language).SendNotiAllDevices(user_id, Messages.GetMessage(language, TypeM.notifyM, notifyM.NOTI_TITLE),
                       Messages.GetMessage(language, TypeM.notifyM, notifyM.NOTI_BODY_SUPPLIER_NOT_ACTIVE)  , 1);
                    return new Response(false, Messages.GetMessage(language, Messages.TypeM.USER, Messages.userM.SUPPLIER_NOT_ACTIVE));
                }

                if (new OfferRepository.OfferRepository(language).InsertOffer(user_id , offer))
                {
                    OrderUser orderUser = new UserRepository.UserRepository(language).GetPersonIdOrderId(offer.order_id)[0];
                    new ServicesRepository(language).SendNotiAllDevices(orderUser.userId, Messages.GetMessage(language, TypeM.notifyM, notifyM.NOTI_TITLE),
                        Messages.GetMessage(language, TypeM.notifyM, notifyM.NOTI_NEW_OFFER) + " order " + orderUser.OrderIdentity, 1);

                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.OFFER_CREATE));
                }
                    
                else
                    throw new InsertException(language);
            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage , InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }

        public Response CreateRequest(   RequestCriteria requestCriteria)
        {
            try
            {
                
                if (new OfferRepository.OfferRepository(language).UpdateReadyToFixFlag(requestCriteria.accident_id))
                {
                     
                        try
                        {
                            new OfferRepository.OfferRepository(language).InsertPermissionCity(requestCriteria.accident_id, requestCriteria.city_id);
                        }
                        catch (InsertException InsertException)
                        {
                            return new Response(false, InsertException.RespMessage , InsertException.ErrorMessage );
                        }
                        catch (Exception ex)
                        {
                            return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR) , ex.Message );
                        }
 
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.REQUEST_CREATE) );
                }
                else
                    throw new  InsertException(language);

            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage , InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response insertOrUpdatePermission( RequestCriteria requestCriteria)
        {
            try
            { 

                    try
                    {
                        new OfferRepository.OfferRepository(language).InsertPermissionCity(requestCriteria.accident_id, requestCriteria.city_id);
                    }
                    catch (InsertException InsertException)
                    {
                        return new Response(false, InsertException.RespMessage,   InsertException.ErrorMessage );
                    }
                    catch (Exception ex)
                    {
                    return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
                }

                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.REQUEST_CREATE));
                

            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage,    InsertException.ErrorMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }


        public Response CreateRequestByAreaPermission( RequestCriteria requestCriteria)
        {
            try
            {

                if (new OfferRepository.OfferRepository(language).UpdateReadyToFixFlag(requestCriteria.accident_id))
                {
                    foreach (int area_id in requestCriteria.listArea)
                    {
                        try
                        {
                            new OfferRepository.OfferRepository(language).InsertPermissionArea(requestCriteria.accident_id, area_id);
                        }
                        catch (InsertException InsertException)
                        {

                            new OfferRepository.OfferRepository(language).DeleteIndustrialPermission(requestCriteria.accident_id);
                            new OrderRepository.OrderRepository(language).ResetReadyToFixFlag(requestCriteria.accident_id);

                            return new Response(false, InsertException.RespMessage,   InsertException.ErrorMessage   );

                        }
                        catch (Exception ex)
                        {
                            return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));

                        }

                    }
                    return new Response(true, Messages.GetMessage(language, Messages.TypeM.OFFER, Messages.offerM.REQUEST_CREATE) );

                }
                else
                    throw new InsertException(language);

            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage , InsertException.ErrorMessage  );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        
        public Response GetPersonOffers(OfferCriteria offerCriteria ,int user_id )
        {
            try
            {
                List<UserOffer> offers = new OfferRepository.OfferRepository(language).GetPrsonOffers(offerCriteria , user_id);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), offers);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException  , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR) , new List<string>());
            }
        }

        public Response GetSupplierOffers(OfferCriteria offerCriteria, int user_id)
        {
            try
            {
                List<UserOffer> offers = new OfferRepository.OfferRepository(language).GetSupplierOffers(offerCriteria, user_id);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), offers);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), new List<string>());
            }
        }


        public Response GetOffers(OfferCriteria offerCriteria)
        {
            try
            {
                List<OfferDTO> offers = new OfferRepository.OfferRepository(language).GetOffers(offerCriteria);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), offers);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException  , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR ) , new List<string>());
            }
        }

        public Response GetAllRequests( string accessToken)
        {
            try
            {
                int workshop = new UserRepository.UserRepository(language).GetSupplierIdByAccessToken(accessToken);
                List<WarshaRequestDTO> WorkshopRequests = new OfferRepository.OfferRepository(language).GetAllRequestsCityPermission(workshop);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), WorkshopRequests);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(false, EmptyViewException.RespMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

     
        public Response GetWorkshopOffers( string accessToken)
        {
            try
            {
                int workshop_id = new UserRepository.UserRepository(language).GetSupplierIdByAccessToken(accessToken);
                List<WarshaOffersCriteria> userRequestCriterias = new OfferRepository.OfferRepository(language).GetOffersByWarsha_id(workshop_id, Enums.OfferType.offerNotAccepted);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), userRequestCriterias);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), new List<string>());
            }
        }

        public Response GetWorkshopOffersAccepted( string accessToken)
        {
            try
            {
                int workshop_id = new UserRepository.UserRepository(language).GetSupplierIdByAccessToken(accessToken);
                List<WarshaOffersCriteria> WorkshopOffers = new OfferRepository.OfferRepository(language).GetOffersByWarsha_id(workshop_id, Enums.OfferType.offerAccepted);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), WorkshopOffers);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), new List<string>());
            }
        }

        public Response GetWorkshopOffersIsFixing( string accessToken)
        {
            try
            {
                int workshop_id = new UserRepository.UserRepository(language).GetSupplierIdByAccessToken(accessToken);
                List<WarshaOffersCriteria> WorkshopOffers = new OfferRepository.OfferRepository(language).GetOffersByWarsha_id(workshop_id, Enums.OfferType.offerIsFixing);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), WorkshopOffers);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException ,  new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), new List<string>());
            }
        }

        public Response GetWorkshopOffersFinishFix(  string accessToken)
        {
            try
            {
                int workshop_id = new UserRepository.UserRepository(language).GetSupplierIdByAccessToken(accessToken);
                List<WarshaOffersCriteria> WorkshopOffers = new OfferRepository.OfferRepository(language).GetOffersByWarsha_id(workshop_id, Enums.OfferType.offerFinishFixing);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), WorkshopOffers);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), new List<string>());
            }
        }

        public Response GetSupplierStats(  string accessToken)
        {
            try
            {
                int supplier_id = new UserRepository.UserRepository(language).GetSupplierIdByAccessToken(accessToken);
                Stats supplierStats = new OfferRepository.OfferRepository(language).GetSupplierStats(supplier_id);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), supplierStats);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response GetPersonStats(  string accessToken)
        {
            try
            {
                int user_id = new UserRepository.UserRepository(language).GetUserIdByAccessToken(accessToken);
               PersonStats personStats = new OfferRepository.OfferRepository(language).GetPersonStats(user_id);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), personStats);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage );
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }

        public Response GetAreaByCity(  int cityId)
        {
            try
            {
                List<IndustrialArea> industrialAreas = new OfferRepository.OfferRepository(language).GetAreaByCityId(cityId);
                return new Response(true, Messages.GetMessage(language, Messages.TypeM.DEFAULT, defaultM.DATAGOT), industrialAreas);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), new List<string>());
            }
        }

        public Response GetCity(string lang)
        {
            try
            {
                List<City> cities = new OfferRepository.OfferRepository(language).GetCity(lang);
                return new Response(true, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, defaultM.DATAGOT), cities);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true, EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), new List<string>());
            }
        }


      
       
    }
}
