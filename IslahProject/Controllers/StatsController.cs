using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Messages;
using HelperClass;
using Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.StatService;
using Service.UserService;
using static Domain.Messages.Messages;

namespace IslahProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        // GET: api/Stats
        [HttpPost]
        [ActionName("GetUserBills")]
        public ActionResult<Response> GetUserBills([FromQuery(Name ="lang")] string lang ,  GetCriteria getCriteria)
        {
            //check AccessToken 
            if (new UserService(lang).CheckAccessToken(getCriteria.accessToken))
                return new StatService(lang).GetWorkshopBills(getCriteria.accessToken);
            else
                return new Response(false, Messages.GetMessage(lang ,   TypeM.DEFAULT, defaultM.WRONG_ACCESS_TOKEN), new List<string>());
        }




       
    }
}
