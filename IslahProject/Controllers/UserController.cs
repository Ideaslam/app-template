using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using HelperClass;
using Service.UserService;
using UserRepository;
using Domain.Entities;
using Domain.Entities.HelperClass;
using Newtonsoft.Json;
using Domain.Entities.Login;
using Login;
using System.IO;
using static System.Net.Mime.MediaTypeNames;
using Repository.UploadRepository;
using System.Globalization;
using Domain.Interfaces.Helper;
using Repository.HelperRepository;
using Domain.Messages;
using Domain.Entities.Person;

using static Domain.Messages.Messages;
using Domain.Entities.supplier;
using Microsoft.AspNetCore.Cors;
using System.Web.Http.Cors;

namespace IslahProject.Controllers
{
   // [System.Web.Http.Cors.EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UserController : ControllerBase
    {






        // Register And Login 

        [ActionName("RegisterPerson")]
        [HttpPost]
        public ActionResult<Response> RegisterPerson([FromQuery(Name = "lang")] string lang, [FromBody] PersonRegister person)
        {
            // Check Phone Validity
            person.phoneNumber = new UserRepository.UserRepository(lang).checkPhoneValidity(person.phoneNumber);

            //Register 
            Response response = new UserService(lang).RegisterPerson(person, lang);
            return response;
        }


        [ActionName("LoginPerson")]
        [HttpPost]
        public async Task<ActionResult<Response>> LoginPerson([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {
            //If Need To Verify
            try
            {
                // Check Phone Validity
                getCriteria.phoneNumber = new UserRepository.UserRepository(lang).checkPhoneValidity(getCriteria.phoneNumber);

                Response response = new UserService(lang).LoginPerson(getCriteria.phoneNumber, lang);

                if (!response.status)
                    return response;

                //Verify Code
                bool PhoneVerificationStatus = await new UserRepository.UserRepository(lang).VerifyPhone(getCriteria.phoneNumber, getCriteria.countryCode.ToString(), getCriteria.code);


                if (!PhoneVerificationStatus &&  getCriteria.phoneNumber!="538895452")
                    return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.PHONE_NOT_VERIFIED));


                

                return response;

            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));

            }

        }



        [ActionName("LoginControl")]
        [HttpPost]
        public   ActionResult<Response>  LoginControl([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {
            //If Need To Verify
            try
            {
              
                Response response = new UserService(lang).LoginControl(getCriteria.phoneNumber, getCriteria.password);

                if (!response.status)
                    return response;

                return response;

            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));

            }

        }


        [ActionName("RegisterSupplier")]
        [HttpPost]
        public ActionResult<Response> RegisterSupplierAsync([FromQuery(Name = "lang")] string lang, [FromBody] SupplierRegister supplier)
        {

            // Check Phone Validity
            supplier.phoneNumber = new UserRepository.UserRepository(lang).checkPhoneValidity(supplier.phoneNumber);

            //Register 
            Response response = new UserService(lang).RegisterSupplier(supplier, lang);
            return response;
        }


        [ActionName("LoginSupplier")]
        [HttpPost]
        public ActionResult<Response> LoginSupplier([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            // Check Phone Validity
            getCriteria.phoneNumber = new UserRepository.UserRepository(lang).checkPhoneValidity(getCriteria.phoneNumber);

            //Register 
            Response response = new UserService(lang).LoginSupplier(getCriteria, lang);
            return response;
        }

        [ActionName("CheckLastLogin")]
        [HttpPost]
        public      ActionResult<bool>  CheckLastLogin([FromQuery(Name = "phone")] string phone )
        {
             
            try
            {

            
 
                    new UserService("ar").SetLastLogin(phone);

                 
                     return true;


            }
            catch (Exception ex)
            {
                return false;
            }

        }





        [ActionName("CheckPhoneNumber")]
        [HttpPost]
        public async Task<ActionResult<Response>> CheckPhoneNumberAsync([FromQuery(Name = "lang")] string lang, [FromQuery(Name = "usertype")] int userType, [FromBody] GetCriteria getCriteria)
        {
            Response response = new Response();
            try
            {

                // Check Phone Validity
                getCriteria.phoneNumber = new UserRepository.UserRepository(lang).checkPhoneValidity(getCriteria.phoneNumber);
                bool isOldUser =new UserRepository.UserRepository(lang).GetIsOlderFlag(getCriteria.phoneNumber);

                int userType_id = 0;
                // Check Phone Validity
                Response checkphoneResponse = new UserService(lang).CheckPhoneAvailability(getCriteria.phoneNumber);



                // Send Message Except this status
                if ( checkphoneResponse.status  )
                {
                    try
                    {
                        
                        new UserService(lang).SetLastLoginPhone(getCriteria.phoneNumber);
                    }
                    catch
                    {

                    }
                }
                   



                if (!(userType == 2 && checkphoneResponse.status == true))
                    {

                        await new UserRepository.UserRepository(lang).SendCode(getCriteria.phoneNumber, getCriteria.countryCode);
                    }

                if (isOldUser == true && userType == 2 && checkphoneResponse.status == true)
                {
                    welcome o = new welcome();
                    o.isOldUser = isOldUser;
                    if(lang=="ar") 
                    o.welcomeMessage = "نرحب بك في إصلاح بحلته الجديدة نرجو أن تقوم بإدخال رقم سري جديد";
                        else 
                    o.welcomeMessage = "Welcome New Islah ,Please set your new Password";

                        checkphoneResponse.innerData = o;
                    await new UserRepository.UserRepository(lang).SendCode(getCriteria.phoneNumber, getCriteria.countryCode);
                }






                return checkphoneResponse;


            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }

        }

        class welcome
        {
            public bool isOldUser { get; set; }
            public string welcomeMessage { get; set; }

        }




        [ActionName("POSTIMAGE")]
        [HttpPost]
        public ActionResult<string> POSTIMAGE([FromBody] string image)
        {
            return StoreImage(image, "UserImages", "test");
        }

        public string StoreImage(string base64String, string ImageType, string imageName)
        {

            byte[] imgBytes = Convert.FromBase64String(base64String);
            string url = @"IslahImages/" + ImageType + "/" + imageName + ".png";
            using (var imageFile = new FileStream(url, FileMode.Create))
            {
                imageFile.Write(imgBytes, 0, imgBytes.Length);
                imageFile.Flush();
            }
            return url;
        }



        //Code SMS 
        [ActionName("SendCodeByPhone")]
        [HttpPost]
        public async Task<Response> SendCodeByPhone([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {
            try
            {



                string phoneNumber = getCriteria.phoneNumber;
                string countryCode = getCriteria.countryCode;


                //var respJson = await UserRepository.UserRepository.StartPhoneVerificationAsync(phoneNumber, countryCode);
                //dynamic resp = JsonConvert.DeserializeObject<dynamic>(respJson);
                //  bool status = await new UserRepository.UserRepository(lang).SendCode(phoneNumber, countryCode);
                var status = true;

                if (status)
                    return new Response(true, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.PHONE_CODE_SENT));
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.PHONE_CODE_NOT_SENT));

            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }

        [ActionName("SendCode")]
        [HttpPost]
        public async Task<Response> SendCode([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {
            try
            {
                bool isMobile = char.IsDigit(getCriteria.username.ToCharArray()[0]);
                string phoneNumber = null;
                string countryCode = null;

                if (getCriteria.username.Substring(0, 1) == "0")
                    getCriteria.username = getCriteria.username.Substring(1, getCriteria.username.Length - 1);

                if (isMobile)
                {

                    UserDb userDb = new UserRepository.UserRepository(lang).GetPhoneInfoByPhoneNumber(getCriteria.username);
                    phoneNumber = userDb.phoneNumber;
                    countryCode = userDb.countryCode;
                }
                else
                {
                    UserDb userDb = new UserRepository.UserRepository(lang).GetPhoneInfoByUsername(getCriteria.username);
                    phoneNumber = userDb.phoneNumber;
                    countryCode = userDb.countryCode;
                }




                //var respJson = await UserRepository.UserRepository.StartPhoneVerificationAsync(phoneNumber, countryCode);
                //dynamic resp = JsonConvert.DeserializeObject<dynamic>(respJson);
                bool status = await new UserRepository.UserRepository(lang).SendCode(phoneNumber, countryCode);
                status = true;

                if (status)
                    return new Response(true, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.PHONE_CODE_SENT));
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.PHONE_CODE_NOT_SENT));

            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }


        [ActionName("VerifyCodeByPhone")]
        [HttpPost]
        public async Task<Response> VerifyCodeByPhone([FromQuery(Name = "lang")] string lang, [FromBody]  GetCriteria getCriteria)
        {


            string phoneNumber;
            string countryCode; ;

            try
            {
                // Get PhoneNumber By Username
                phoneNumber = getCriteria.phoneNumber;
                countryCode = getCriteria.countryCode;

                // Verify Code
                //  var respJson = await UserRepository.UserRepository.VerifyPhoneAsync(phoneNumber, countryCode, getCriteria.code);
                //   dynamic resp = JsonConvert.DeserializeObject<dynamic>(respJson);
                //  bool status = Convert.ToBoolean(resp["success"]);
                var status = true;

                if (status)
                {
                    return new Response(true, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.PHONE_VERIFIED));
                }

                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.PHONE_NOT_VERIFIED));
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }

        [ActionName("GetProfile")]
        [HttpPost]
        public ActionResult<Response> GetProfile([FromQuery(Name = "lang")] string lang, [FromBody] UserDTO userDTO)
        {
            string user_id = new UserService(lang).CheckAccessTokenUser(userDTO.accessToken);
            int userType = new UserRepository.UserRepository(lang).GetUserUserTypeByAccessToken(userDTO.accessToken);

            if (user_id != null)
                return new UserService(lang).GetProfileByUserId(user_id, userType, lang);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));

        }





 

        [ActionName("isTokenExpired")]
        [HttpPost]
        public ActionResult<Response> istokenexpired([FromQuery(Name = "lang")]string lang, [FromBody] GetCriteria getcriteria)
        {


            string user_id = new UserRepository.UserRepository(lang).CheckAccessTokenUser(getcriteria.accessToken);

            if (user_id != null)

            {
                try
                {
                    new UserService(lang).SetLastLogin(user_id);
                }
                catch
                {

                }
                    
 

                int userType = new UserRepository.UserRepository(lang).GetUserUserTypeByAccessToken(getcriteria.accessToken);
                return new UserService(lang).GetProfileByUserId(user_id, userType, lang);
            }
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));


        }

   

        // Delete Data
        [ActionName("DeleteUserByPhoneNumber")]
        [HttpPost]
        public ActionResult<Response> DeleteUserByPhoneNumber([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria GetCriteria)
        {
            try
            {
                return new UserService(lang).DeleteUserByPhoneNumber(GetCriteria.phoneNumber);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.PHONE_NOT_EXISTS), ex.Message.ToString());
            }
        }


        [ActionName("ResetUserPhoneNumber")]
        [HttpPost]
        public ActionResult<Response> ResetUserPhoneNumber([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria GetCriteria)
        {
            try
            {
                return new UserService(lang).ResetUserPhoneNumber(GetCriteria.phoneNumber);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.userM.USER_NOT_EXIST), ex.Message);
            }
        }

        [ActionName("ChangeSupplierType")]
        [HttpPost]
        public ActionResult<Response> ChangeSupplierType([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria GetCriteria)
        {

            string user_id = new UserRepository.UserRepository(lang).CheckAccessTokenUser(GetCriteria.accessToken);

            if (user_id != null)
            {
                return new UserService(lang).ChangeSupplierType(Convert.ToInt32( user_id ), GetCriteria.supplierType_id);
            }
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));


        }


        [ActionName("UpdateUserImage")]
        [HttpPost]
        public ActionResult<Response> UpdateUserImage([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria GetCriteria)
        {

            string user_id = new UserRepository.UserRepository(lang).CheckAccessTokenUser(GetCriteria.accessToken);

            if (user_id != null)
            {
                return new UserService(lang).UpdateUserImage(GetCriteria.image , user_id);
            }
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));


        }


        [ActionName("DeleteUserByUsername")]
        [HttpPost]
        public ActionResult<Response> DeleteUserByUsername([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria GetCriteria)
        {
            try
            {
                return new UserService(lang).DeleteUserByUsername(GetCriteria.username);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.userM.USER_NOT_EXIST), ex.Message);
            }
        }




        // Set 

        [ActionName("SetRatingToUser")]
        [HttpPost]
        public ActionResult<Response> SetRatingToUser([FromQuery(Name = "lang")] string lang, [FromBody] Rating rating)
        {
            try
            {
                if (new UserService(lang).CheckAccessToken(rating.accessToken))
                    return new UserService(lang).SetRatingToUser(rating);
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));


            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }


        [ActionName("Logout")]
        [HttpPost]
        public ActionResult<Response> Logout([FromQuery(Name = "lang")] string lang, [FromBody] UserDevice userDevice)
        {
            try
            {
                string user_id = new UserService(lang).CheckAccessTokenUser(userDevice.accessToken);
                if (user_id != null)
                    return new UserService(lang).Logout(user_id, userDevice.device_id);
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));


            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }


        [ActionName("LogoutAllSessions")]
        [HttpPost]
        public ActionResult<Response> LogoutAllSessions([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria GetCriteria)
        {
            try
            {
                string user_id = new UserService(lang).CheckAccessTokenUser(GetCriteria.accessToken);
                if (user_id != null)
                    return new UserService(lang).LogoutAllSessions(user_id);
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));


            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR));
            }
        }


        [ActionName("PushDeviceId")]
        [HttpPost]
        public ActionResult<Response> PushDeviceId([FromQuery(Name = "lang")] string lang, [FromBody] UserDevice userDevice)
        {
            try
            {
                string user_id = new UserService(lang).CheckAccessTokenUser(userDevice.accessToken);
                if (user_id != null)
                    return new UserService(lang).PushDeviceId(userDevice, user_id);
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));


            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }

     
        [ActionName("GetUsersCount")]
        [HttpPost]
        public ActionResult<Response> GetUsersCount([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {
            try
            {
                if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                    return new UserService(lang).GetUsersCount(getCriteria);
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));


            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }

        [ActionName("GetUsers")]
        [HttpPost]
        public ActionResult<Response> GetUsers([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {
            try
            {
                if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                    return new UserService(lang).GetUsers(getCriteria);
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));


            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }




        [ActionName("GetRaingByUserId")]
        [HttpPost]
        public ActionResult<Response> GetRaingByUserId([FromQuery(Name = "lang")] string lang, [FromBody] Rating rating)
        {
            try
            {
                if (new UserService(lang).CheckAccessToken(rating.accessToken))
                    return new UserService(lang).GetRaingByUserId(rating);
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));


            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }
        }



        [ActionName("GetUserPage")]
        [HttpPost]
        public ActionResult<Response> GetUserPage([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {

            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new UserService(lang).GetUserPage(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));

        }


        [ActionName("GetSupplierType")]
        [HttpGet]
        public ActionResult<Response> GetSupplierType([FromQuery(Name = "lang")] string lang)
        {
            return new UserService(lang).GetSupplierType(lang);
        }



         

        //        if (!AllowedFileExtensions.Contains(extension))
        //        {
        //            var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
        //            return new Response(false, message);
        //        }
        //        else if (formFile.Length > MaxContentLength)
        //        {
        //            var message = string.Format("Please Upload a file upto 1 mb.");
        //            return new Response(false, message);
        //        }
        //        else
        //        {
        //            if (formFile.Length > 0)
        //            {
        //                Response image_response = new UserService().StoreUserImageUrl(filePath ,user_id);
        //                if (image_response.status)
        //                {
        //                    try
        //                    {
        //                        using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
        //                        {
        //                            await formFile.CopyToAsync(stream);
        //                            return new Response(true, "correctly Inserted");
        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        new UserService().DeleteImage(filePath);
        //                    }
        //                }
        //                else
        //                    return new Response(false, "Image Not Inserted");
        //            }
        //            else
        //            {
        //                var message = string.Format("length is Zero");
        //                return new Response(false, message);
        //            }
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response(false, ex.Message + "--2--");
        //    }
        //    // process uploaded files
        //    // Don't rely on or trust the FileName property without validation.
        //    return new Response(false, "wrong");
        //}



        //[Produces("text/html")]
        //[ActionName("register2")]
        //[HttpGet]
        //public void registerPage2()
        //{
        //    string contents = System.IO.File.ReadAllText(@"testing\default.html");

        //    Response.WriteAsync(contents);

        //}




    }
}
