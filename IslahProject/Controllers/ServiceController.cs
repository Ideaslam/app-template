using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities.Services;
using Domain.Messages;
using HelperClass;
using Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.ServicesService;
using Service.StatService;
using Service.UserService;
using static Domain.Messages.Messages;

namespace IslahProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {


        [ActionName("CreateService")]
        [HttpPost]
        public ActionResult<Response> CreateService([FromQuery(Name = "lang")] string lang, [FromBody] serviceRequest   serviceRequest)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(serviceRequest.accessToken))
                return new ServciesService(lang).CreateService(serviceRequest);   
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }

        [ActionName("CreateBill")]
        [HttpPost]
        public ActionResult<Response> CreateBill([FromQuery(Name = "lang")] string lang, [FromBody] BillFix BillFix)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(BillFix.accessToken))
                return new ServciesService(lang).InsertBill(BillFix);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }

 

        [ActionName("CancelService")]
        [HttpPost]
        public ActionResult<Response> CancelService([FromQuery(Name = "lang")] string lang, [FromBody] serviceRequest serviceRequest)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(serviceRequest.accessToken))
            {
                return new ServciesService(lang).CancelService(serviceRequest.id);
            }

            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }


        [ActionName("FinishService")]
        [HttpPost]
        public ActionResult<Response> FinishService([FromQuery(Name = "lang")] string lang, [FromBody] serviceRequest serviceRequest)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(serviceRequest.accessToken))
            {
                return new ServciesService(lang).FinishService(serviceRequest);
            }

            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }





        [HttpGet]
        [ActionName("GetServiceTypes")]
        public ActionResult<Response> GetServiceTypes([FromQuery(Name = "lang")] string lang)
        {
            return new ServciesService(lang).GetServiceTypes();
        }


        [HttpGet("{id}")]
        [ActionName("GetService")]
        public ActionResult<Response> GetService( [FromQuery(Name ="lang")] string lang ,  int id)
        {
            return new ServciesService(lang).GetService(id);
        }



        [ActionName("ChangePassword")]
        [HttpPost] 
        public ActionResult<Response> ChangePassword([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new ServciesService(lang).ChangePassword(getCriteria);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        } 

        [ActionName("ResetPassword")]
        [HttpPost]
        public ActionResult<Response> ResetPassword([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {
            Response resp = new UserService(lang).ResetPassword(getCriteria.phoneNumber, getCriteria.password);
            //check AccessToken 
               if (resp.status)
            {
                int userType = new UserRepository.UserRepository(lang).GetUserUserTypeByPhone(getCriteria.phoneNumber);
                resp.innerData=   new UserService(lang).GetProfileByPhone(getCriteria.phoneNumber, userType,lang).innerData;
            }



            return resp;
           
        }


        [ActionName("ChangePhoneNumber")]
        [HttpPost]
        public async Task<ActionResult<Response>> ChangePhoneNumberAsync([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            string user_id   = new UserService(lang).CheckAccessTokenUser(getCriteria.accessToken);
            if (user_id != null)
            {
                // Verify Code
                // bool PhoneVerificationStatus = await new UserRepository.UserRepository(lang).VerifyPhone(getCriteria.phoneNumber, getCriteria.countryCode.ToString(), getCriteria.code);
                // if (!PhoneVerificationStatus)
                // return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.PHONE_NOT_VERIFIED));

                getCriteria.phoneNumber =  new UserRepository.UserRepository(lang).checkPhoneValidity(getCriteria.phoneNumber);
                return new ServciesService(lang).ChangePhoneNumber(getCriteria.phoneNumber ,user_id);
            }
                
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }

        
        [ActionName("SendNotification")]
        [HttpPost]
        public    ActionResult<Response>  SendNotification([FromQuery(Name = "lang")] string lang, [FromBody] Notification notification)
        {

            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(notification.accessToken);
            if (user_id != null)
            { 
                return new ServciesService(lang).SendNotification(notification);
            }

            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }


        [ActionName("GetUserBills")]
        [HttpPost]
        public ActionResult<Response> GetUserBills([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new ServciesService(lang).GetUserBills(getCriteria);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }



        [ActionName("GetWorkshopBills")]
        [HttpPost]
        public ActionResult<Response> GetWorkshopBills([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new ServciesService(lang).GetWorkshopBills(getCriteria);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }


        [HttpPost]
        [ActionName("GetWorkshopBillsCosts")]
        public ActionResult<Response> GetWorkshopBillsCosts([FromQuery(Name = "lang")] string lang, GetCriteria getCriteria)
        {
            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new StatService(lang).GetWorkshopBills(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());
        }

    }
}
