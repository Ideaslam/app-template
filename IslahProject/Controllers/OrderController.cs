using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelperClass;
using Service.UserService;
using UserRepository;
using Service.OrderService;
using Domain.Entities;
using Domain.Entities.Order;
using Login;
using Domain.Messages;
using static Domain.Messages.Messages;
using Repository.UploadRepository;
using Newtonsoft.Json;
using System.IO;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Http.Internal;
 
using Domain.Interfaces.Order;
using Domain.Exceptions;
using Domain.Enums;
using Repository.HelperRepository;
using Repository.ServicesRepository;

namespace IslahProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        // POST: api/Accident
        [ActionName("StoreOrder")]
        [HttpPost]
        public    ActionResult<Response>  StoreOrderDataAsync([FromQuery(Name = "lang")] string lang, [FromForm] string accessToken, [FromForm] string carId,
          [FromForm] string orderType, [FromForm] string cityId, [FromForm] string locationX, [FromForm] string locationY,[FromForm] string orderDetailsIds , [FromForm] string note, [FromForm] string images )
        {


            List<string> imgs = new List<string>();
            List<int> orderassignIds = new List<int>();

            // Check If List Is NULL
            if (images != null)
                imgs = JsonConvert.DeserializeObject<List<string>>(images);

            ////NO car Service
            //if (carId == null)
            //    carId = "-1";


            // Check If List Is NULL
            if (orderDetailsIds != null)
                orderassignIds = JsonConvert.DeserializeObject<List<int>>(orderDetailsIds);



            // MAPPING DATA 
            CarInfoOrder carInfoOrder = new CarInfoOrder();
            carInfoOrder.accessToken = accessToken;


            if (carId != null && carId != "" && carId != "0")
                carInfoOrder.carId =   Convert.ToInt32(carId) ;
            else
            {
                carInfoOrder.carId = null;
            }

           
            if (cityId != null && cityId != "" && cityId != "0" && cityId != "-1")
            {
                carInfoOrder.cityId = Convert.ToInt32(cityId);
 
            }
                
            else
            {
                carInfoOrder.cityId = 1;
            }


            if (orderType != null)
                carInfoOrder.orderType = Convert.ToInt32(orderType);
            else
            {
                carInfoOrder.orderType = 1;
            }

            carInfoOrder.locationX = Convert.ToDouble(locationX);
            carInfoOrder.locationY = Convert.ToDouble(locationY);

            if (orderassignIds == null)
                carInfoOrder.orderDetailsIds = new List<int>();
            else 
                carInfoOrder.orderDetailsIds = orderassignIds;

            carInfoOrder.note = note;

            if (imgs == null)
                carInfoOrder.images = new List<string>() ;
            else
                carInfoOrder.images = imgs;




            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(carInfoOrder.accessToken);
            carInfoOrder.OrderIdentity = new HelperRepository().generateIds((int)Enums.CreationNumberCode.order, DateTime.Now);

            if (user_id != null)
            {

                // STORE ACCIDENT DATA 
                Response res = new OrderService(lang).StoreOrderData(carInfoOrder, user_id);
                var order_id = res.innerData;
                res.innerData =new  object();


                //Send Notification To ALL Admin When New Request Created
                if (res.status)
                {
                    try
                    {
                        //Get All Suppliers Ids
                        List<int> users = new UserRepository.UserRepository(lang).GetAllAdminIds();
                        foreach (int user in users)
                        {
                            try
                            {
                                new ServicesRepository(lang).SendNotiAllDevices(user, Messages.GetMessage(lang, TypeM.notifyM, notifyM.NOTI_TITLE),
                                Messages.GetMessage(lang, TypeM.notifyM, notifyM.NOTI_NEW_ORDER), 1);
                            }
                            catch
                            {
                                ;
                            }

                        }

                    }
                    catch (Exception ex)
                    {
                        new Response(false, ex.Message, ex.Message);

                    }
                }


                // GET ACCIDENT ID
                //int order_id = new OrderRepository.OrderRepository(lang).GetOrderIdByUserId(carInfoOrder.carId);

                if (res.status)
                {
                    // Store Video Accident 
                    try
                    {
                        var file = Request.Form.Files["video"];

                        // Check if Null So , Do not Store Video 
                        if(file == null)
                            return res;


                        var folderName = Path.Combine("Resources", "Images");
                        var pathToSave = @"../IslahImages\videos\orders";//Path.Combine(Directory.GetCurrentDirectory(), folderName);

                        if (file.Length > 0)
                        {
                            var fileName =    ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"')+".mp4";
                            var fullPath = Path.Combine(pathToSave, fileName);
                            var dbPath = Path.Combine(folderName, fileName);
                            bool UrlVideoStatus = false;
                            using (var stream = new FileStream(fullPath, FileMode.Create))
                            {
                                file.CopyTo(stream);

                                UrlVideoStatus = new OrderRepository.OrderRepository(lang).InsertOrderPictureUrl("videos/orders/" + fileName, 2, Convert.ToInt32( order_id));
                               
                            }
                            if (UrlVideoStatus)
                                return res;
                            else
                                return new Response(false, Messages.GetMessage(lang, TypeM.ACCIDENT, orderM.VIDEO_Order_ERROR));

                        }
                        else
                        {
                            return new Response(false, Messages.GetMessage(lang, TypeM.ACCIDENT, orderM.VIDEO_Order_ERROR));
                        }
                    }
                    catch (Exception ex)
                    {
                        return new Response(false, Messages.GetMessage(lang, TypeM.ACCIDENT, orderM.VIDEO_Order_ERROR), ex.Message);
                    }

                }
                else
                {
                    return res;
                }
            }

            else
               return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN) );
                
            
               
        }



        [ActionName("UpdateAccidentData")]
        [HttpPost]
        public ActionResult<Response> UpdateAccidentData([FromQuery(Name = "lang")] string lang, [FromBody] CarInfoOrder carInfoAccident)
        {
            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(carInfoAccident.accessToken))
                return new OrderService(lang).UpdateAccidentData(carInfoAccident);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }


        [HttpPost]
        [ActionName("GetAllRequests")]
        public ActionResult<Response> GetAllRequests([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(getCriteria.accessToken);
            if (user_id != null)
                return new OrderService(lang).GetAllRequests(user_id, getCriteria.PageSize,getCriteria.PageNumber);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());


        }

     


        [ActionName("GetOrderLists")]
        [HttpGet]
        public ActionResult<Response> GetOrderLists([FromQuery(Name = "lang")] string lang, [FromQuery(Name = "orderType")] int orderType_id)
        {
            //check AccessToken 
         
                return new OrderService(lang).GetOrderLists(orderType_id);
 
        }


        [ActionName("CancelRequest")]
        [HttpPost]
        public ActionResult<Response> DeactiveRequest([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria  getCriteria )
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new OrderService(lang).DeleteRequest(getCriteria.order_id);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }

        [ActionName("ActiveRequest")]
        [HttpPost]
        public ActionResult<Response> ActiveRequest([FromQuery(Name = "lang")] string lang, [FromBody] Accident accident)
        {
            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(accident.accessToken))
                return new OrderService(lang).ActiveRequest(accident.accident_id);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }


        [ActionName("DeleteRequest")]
        [HttpPost]
        public ActionResult<Response> DeleteRequest([FromQuery(Name = "lang")] string lang, [FromBody] Accident accident)
        {
            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(accident.accessToken))
                return new OrderService(lang).DeleteRequest(accident.accident_id);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }




       


        [ActionName("GetDamagesType")]
        [HttpGet]
        public ActionResult<Response> GetDamagesType([FromQuery(Name = "lang")] string lang)
        {

            return new  OrderService(lang).GetDamagesType();

        }

        [ActionName("GetOrderType")]
        [HttpGet]
        public ActionResult<Response> GetOrderType([FromQuery(Name = "lang")] string lang)
        {
            return new OrderService(lang).GetOrderType(lang);
        }



        //[ActionName("GetLastWorkshopLocation")]
        //[HttpPost]
        //public ActionResult<Response> GetLastWorkshopLocation(GetCriteria getCriteria)
        //{

        //    //check AccessToken 
        //    if (new UserService().CheckAccessToken(getCriteria.accessToken))
        //        return new AccidentService().GetLastWorkshopLocation(getCriteria.accessToken);
        //    else
        //        return new Response(false, Messages.WrongAccessToken_Ar, Messages.WrongAccessToken_En);
        //}




        [ActionName("postvideo")]
        [HttpPost]
        public ActionResult<Response> postvideo([FromQuery(Name = "lang")] string lang)
        {
            try
            {
                var file = Request.Form.Files["video"];
                var folderName = Path.Combine("Resources", "Images");
                var pathToSave = @"../IslahImages\videos\accidents";//Path.Combine(Directory.GetCurrentDirectory(), folderName);

                if (file.Length > 0)
                {
                    var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
                    var fullPath = Path.Combine(pathToSave, fileName);
                    var dbPath = Path.Combine(folderName, fileName);

                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        file.CopyTo(stream);
                    }

                    return new Response(true, "VIDEO IS STORED");
                }
                else
                {
                    return new Response(false, "VIDEO HAS NO LENGTH");
                }
            }
            catch (Exception ex)
            {
                return new Response(false, ex.Message);
            }

        }









    }
}
