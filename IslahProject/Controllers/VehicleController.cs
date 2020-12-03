using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelperClass;
using Domain.Entities;
using Service.VehicleService;
using Service.UserService;
using Login;
using Domain.Messages;
using static Domain.Messages.Messages;

namespace IslahProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {

        [HttpPost]
        [ActionName("StoreVehicle")]
        public ActionResult<Response> StoreVehicleData( [FromQuery(Name ="lang")] string lang ,  [FromBody] Vehicle vehicle)
        {
            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(vehicle.accessToken); 
            if (user_id != null)
                return new VehicleService(lang).StoreVehicleData(vehicle , user_id);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));
        }

        [HttpPost]
        [ActionName("GetVechiles")]
        public ActionResult<Response> GetVechiles([FromQuery(Name ="lang")] string lang ,[FromBody] GetCriteria getCriteria)
        {
            //check AccessToken 
            string user_id = new UserService(lang).CheckAccessTokenUser(getCriteria.accessToken);
            if (user_id != null)
                return new VehicleService(lang).GetMyVehicles( user_id , lang);
            else 
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());

        }
        

        [HttpGet]
        [ActionName("GetCarYears")]
        public ActionResult<Response> GetCarYears([FromQuery(Name = "lang")] string lang)
        {
           
                return new Response(true, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.DATAGOT),
                    Enumerable.Range( 1970 , DateTime.Today.Year-1970+1).Reverse().ToList());

        }


        [HttpGet]
        [ActionName("GetColors")]
        public ActionResult<Response> GetColors([FromQuery(Name ="lang")] string lang)
        {
            return new VehicleService(lang).GetColors(lang) ;
          
        }

        [HttpGet]
        [ActionName("GetBrands")]
        public ActionResult<Response> GetBrands([FromQuery(Name = "lang")] string lang)
        {
                return new VehicleService(lang).GetBrands(lang);  
        }

        [HttpGet]
        [ActionName("GetModels")]
        public ActionResult<Response> GetModels([FromQuery(Name = "lang")] string lang , [FromQuery(Name = "brandId")] int brandId)
        {

            return new VehicleService(lang).GetModels(lang,brandId);

        }



        [HttpPost]
        [ActionName("GetWorkshopCars")]
        public ActionResult<Response> GetWorkshopCars([FromQuery(Name = "lang")] string lang, GetCriteria getCriteria)
        {
            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new VehicleService(lang).GetWorkshopCars(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());

        }


        [HttpPost]
        [ActionName("GetPaper")]
        public ActionResult<Response> GetFixPaper([FromQuery(Name = "lang")] string lang, [FromBody] GetCriteria getCriteria)
        {


            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new VehicleService(lang).GetFixPaper(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());

        }

        [HttpPost]
        [ActionName("StorePaper")]
        public ActionResult<Response> StoreFixPaper([FromQuery(Name = "lang")] string lang, [FromBody] FixPaper fixPaper)
        {

            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(fixPaper.accessToken))
                return new VehicleService(lang).StoreFixPaper(fixPaper);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN));

        }


        [HttpPost]
        [ActionName("CheckFixPaperByPlateNumber")]
        public ActionResult<Response> CheckFixPaperByPlateNumber([FromQuery(Name = "lang")] string lang, [FromBody] FixPaper fixPaper)
        {


            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(fixPaper.accessToken))
                return new VehicleService(lang).CheckFixPaperByPlateNumber(fixPaper.car_plateNumber);
            else
                return new Response(false, Messages.GetMessage(lang, TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());

        }

        [HttpPost]
        [ActionName("CheckFixPaperByAccident")]
        public ActionResult<Response> CheckFixPaper([FromQuery(Name = "lang")] string lang, [FromBody] Accident accident)
        {


            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(accident.accessToken))
                return new VehicleService(lang).CheckFixPaperByAccidentId(accident.accident_id);
            else
                return new Response(false, Messages.GetMessage(lang ,   TypeM.DEFAULT , defaultM.WRONG_ACCESS_TOKEN ), new List<string>());

        }
    }
}
