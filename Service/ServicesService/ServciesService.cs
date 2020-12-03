using HelperClass;
using Domain.Entities.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Repository.ServicesRepository;
using Domain.Messages;
using Domain.Exceptions;
using Login;
using System.Threading.Tasks;
using static Domain.Messages.Messages;

namespace Service.ServicesService
{
   public class ServciesService : Domain.Interfaces.Services.IServcieService
    {

        string language = "en"; 

        public ServciesService(string language)
        {
            this.language = language; 
        }


        public Response CreateService(serviceRequest serviceRequest)
        {
            try
            {
                int user_id = new UserRepository.UserRepository(language).GetUserIdByAccessToken(serviceRequest.accessToken);
                if (new ServicesRepository(language).InsertService(serviceRequest, user_id))
                    return new Response(true, Messages.GetMessage(language, TypeM.SERVICE, serviceM.SERVICE_CREATE));
                else
                    throw new InsertException(language);
            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR)  , ex.Message.ToString());
            }
        }
        public Response  SendNotification(Notification notification )
        {
            try
            {
                
                if (new ServicesRepository(language).SendNotification(notification))
                    return new Response(true, Messages.GetMessage(language, TypeM.SERVICE, serviceM.SERVICE_NOTI_SENT));
                else
                    throw new InsertException(language);
            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }


        public Response CancelService(int service_id)
        {
            try
            {
                if (new ServicesRepository(language).CancelServiceRequest(service_id ))
                    return new Response(true, Messages.GetMessage(language, TypeM.SERVICE, serviceM.SERVICE_CANCEL));
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false,  UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR)  ,  ex.Message.ToString());
            }
        }


        public Response FinishService(serviceRequest serviceRequest)
        {
            try
            {
                if (new ServicesRepository(language).FinishServiceRequest(serviceRequest.id , serviceRequest.RATE))
                    return new Response(true, Messages.GetMessage(language, TypeM.SERVICE, serviceM.SERVICE_CANCEL));
                else
                    throw new UpdateException(language);
            }
            catch (UpdateException UpdateException)
            {
                return new Response(false,   UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR)  , ex.Message.ToString());
            }
        }

        public Response InsertBill(BillFix billFix)
        {
            try
            {
                if (new ServicesRepository(language).InsertBill(billFix))
                    return new Response(true, Messages.GetMessage(language, TypeM.SERVICE, serviceM.SERVICE_CANCEL));
                else
                    throw new UpdateException(language);
            }
            catch (InsertException InsertException)
            {
                return new Response(false,   InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR)  , ex.Message.ToString());
            }
        }

        public Response ChangePassword(GetCriteria getCriteria)
        {
            try
            {
                bool isPasswordCorrect;
                int user_id = new UserRepository.UserRepository(language).GetUserIdByAccessToken(getCriteria.accessToken);

                try
                {
                    isPasswordCorrect=  PasswordHash.ValidatePassword(getCriteria.oldPassword, 
                        new ServicesRepository(language).ComparePassword(getCriteria.oldPassword , user_id));

                }catch (EmptyViewException EmptyViewException)
                {
                    return new Response(false, Messages.GetMessage(language ,TypeM.SERVICE, serviceM.SERVICE_COMPARE_PASSNOTFOUND));
                }


                if(!isPasswordCorrect)
                    return new Response(false, Messages.GetMessage(language, TypeM.SERVICE, serviceM.SERVICE_COMPARE_PASSNOTFOUND));




                if (new ServicesRepository(language).ChangePassword(PasswordHash.CreateHash(getCriteria.password ), user_id) )
                    return new Response(true, Messages.GetMessage(language, TypeM.SERVICE, serviceM.SERVICE_CHANGE_PASSWORD));
                else
                    throw new UpdateException(language);



            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.RespMessage, InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }




        public   Response   ChangePhoneNumber(string phonenumber , string user_id )
        {
            try
            {
               
              int newPhoneUser=   new UserRepository.UserRepository(language).GetUserIdByPhoneNumber(phonenumber);

                if ( newPhoneUser > 0 && newPhoneUser != Convert.ToInt32( user_id )  )
                {
                    return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.PHONE_ANOTHER_USER));
                }

                if (new ServicesRepository(language).ChangePhoneNumber(phonenumber, Convert.ToInt32( user_id)))
                    return new Response(true, Messages.GetMessage(language, TypeM.SERVICE, serviceM.SERVICE_CHANGE_PHONE));
                else
                    throw new UpdateException(language);


            }
            catch (UpdateException UpdateException)
            {
                return new Response(false,  UpdateException.RespMessage, UpdateException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex.Message.ToString());
            }
        }

        

       public Response GetUserBills(GetCriteria getCriteria)
           {
            try
            {
                int user_id = new UserRepository.UserRepository(language).GetUserIdByAccessToken(getCriteria.accessToken);
                List<UserBill> userBills = new ServicesRepository(language).GetUserBills(user_id);
                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), userBills);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true,   EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex , new List<string>());
            }
        }



        public Response GetWorkshopBills(GetCriteria getCriteria)
        {
            try
            {
                int workshop_id = new UserRepository.UserRepository(language).GetSupplierIdByAccessToken(getCriteria.accessToken);
                List<UserBill> userBills = new ServicesRepository(language).GetWorkshopBills(workshop_id);
                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), userBills);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true,   EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR), ex , new List<string>());
            }
        }


        public Response GetServiceTypes()
        {
            try
            {
                List<ServiceTypes> serviceTypes = new ServicesRepository(language).GetServiceTypes();
                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), serviceTypes);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true,   EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.UNEXPERROR) , ex , new List<string>());
            }
        }


        public Response GetService(int id )
        {
            try
            {
                List<Services> services = new ServicesRepository(language).GetServiceByTypeId(id);
                return new Response(true, Messages.GetMessage(language, TypeM.DEFAULT, defaultM.DATAGOT), services);
            }
            catch (EmptyViewException EmptyViewException)
            {
                return new Response(true,  EmptyViewException.RespMessage, EmptyViewException , new List<string>());
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language,    TypeM.DEFAULT , defaultM.UNEXPERROR )  , ex , new List<string>());
            }
        }


      


    }
}
