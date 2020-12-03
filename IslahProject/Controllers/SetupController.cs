using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Messages;
using HelperClass;
using Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.UserService;

namespace IslahProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class SetupController : ControllerBase
    {



       
        [HttpPost]
        [ActionName("GetTables")]
        public ActionResult<Response> GetAllTables([FromQuery(Name = "lang")] string lang, [FromBody] string accessToken)
        {

            try
            {
                //check AccessToken 
                if (new UserService(lang).CheckAccessToken(accessToken))
                    return new Response(true, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.DATAGOT), new SetupRepository.SetupRepository(lang).GetAllTables());
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }

        }


        [HttpPost("masterTable")]
        [ActionName("GetReference")]
        public ActionResult<Response> GetAllReferencesByMasterName([FromQuery(Name = "lang")] string lang, [FromBody] string accessToken ,string masterTable)
        {

            try
            {
                //check AccessToken 
                if (new UserService(lang).CheckAccessToken(accessToken))
                    return new Response(true, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.DATAGOT), new SetupRepository.SetupRepository(lang).GetAllReferenceByMasterName(masterTable));
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }

        }




        [HttpPost]
        [ActionName("GetAppInfo")]
        public ActionResult<Response> GetAppInfo([FromQuery(Name = "lang")] string lang  ,[FromBody] GetCriteria getCriteria)
        {

            try
            {
                    return new Response(true, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.DATAGOT), new SetupRepository.SetupRepository(lang).GetAppInfo(getCriteria.typePhone));
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }

        }



        [HttpPost("{tableName}")]
        [ActionName("GetReference")]
        public ActionResult<Response> GetColumnsOfTable([FromQuery(Name ="lang")] string lang  , [FromBody] string accessToken , string tableName)
        {

            try
            {
                //check AccessToken 
                if (new UserService(lang).CheckAccessToken(accessToken))
                    return new Response(true, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.DATAGOT), new SetupRepository.SetupRepository(lang).GetAllColumnsOfTableName(tableName));
                else
                    return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.WRONG_ACCESS_TOKEN));
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(lang, Messages.TypeM.DEFAULT, Messages.defaultM.UNEXPERROR), ex.Message);
            }

        }

    }
}
