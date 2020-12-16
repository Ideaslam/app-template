using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using HelperClass;
using Repository.UploadRepository;
using Repository.HelperRepository;
using Login;
using Domain.Messages;
using static Domain.Messages.Messages;
 

namespace IslahProject.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        [AllowAnonymous]


        [ActionName("UploadFiles")]
        [HttpPost("{fileType}/{imgType}/{imgName}")]
        public async Task<Response> Uploadfile(IFormFile file, string fileType, string imgType, string imgName)
        {

            List<IFormFile> files = new List<IFormFile>();
            files.Add(file);
            var filePath=""; 
            try
            {


                foreach (var formFile in files)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 1 MB  
                    IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" , ".mp4" };

                    var ext = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();

                        filePath = ("../dealxImages/" + fileType + @"/" + imgType + @"/" + imgName + extension);


                    if (!AllowedFileExtensions.Contains(extension))
                    {
                        var message = string.Format("Please Upload image of type .jpg,.gif,.png.");
                        return new Response(false, message);
                    }
                    else if (formFile.Length > MaxContentLength)
                    {
                        var message = string.Format("Please Upload a file upto 10 mb.");
                        return new Response(false, message);
                    }
                    else
                    {
                        if (formFile.Length > 0)
                        {
                            using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                            {
                                await formFile.CopyToAsync(stream);
                                return new Response(true, "correctly Inserted");
                            }
                        }
                        else
                        {
                            var message = string.Format("length is Zero");
                            return new Response(false, message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response(false, ex.Message +"---//---"+ filePath);
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return new Response(false, "wrong");
        }



        [ActionName("UploadAccidentVideo")]
        [HttpPost]
        public async Task<Response> UploadAccidentVideo(IFormFile file ,[FromQuery(Name ="lang")] string lang ,  [FromQuery(Name = "accident_id")] int accident_id)
        {

            List<IFormFile> files = new List<IFormFile>();
            files.Add(file);
            var filePath = "";
            try
            {


                foreach (var formFile in files)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 1 MB  
                    IList<string> AllowedFileExtensions = new List<string> {".mp4" };

                    var ext = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.'));
                    var extension = ext.ToLower();
                    string  path = ("videos" + @"/" + "accidents" + @"/" + accident_id + extension) ; 

                    filePath = ("../dealxImages/" + path);


                    if (!AllowedFileExtensions.Contains(extension))
                    {
                        var message = string.Format("Please Upload image of type .mp4");
                        return new Response(false, message);
                    }
                    else if (formFile.Length > MaxContentLength)
                    {
                        var message = string.Format("Please Upload a file upto 10 mb.");
                        return new Response(false, message);
                    }
                    else
                    {
                        if (formFile.Length > 0)
                        {
                            using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                            {
                                 await formFile.CopyToAsync(stream);

                                new OrderRepository.OrderRepository(lang).InsertOrderPictureUrl(path , 2, accident_id); 
                                return new Response(true,    GetMessage(lang , TypeM.DEFAULT, defaultM.INSERT_CORRECT));
                            }
                        }
                        else
                        {
                            var message = string.Format("length is Zero");
                            return new Response(false, message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return new Response(false, GetMessage(lang, TypeM.DEFAULT, defaultM.UNEXPERROR) ,ex.Message + "---//---" + filePath);
            }
            // process uploaded files
            // Don't rely on or trust the FileName property without validation.
            return new Response(false, GetMessage(lang, TypeM.DEFAULT, defaultM.UNEXPERROR));
        }


        [ActionName("Error")]
        [HttpGet]
        public ActionResult<string> Error()
        {
            Domain.Entities.ExceptionHandling error = new Domain.Entities.ExceptionHandling();
            error.main();
            return "aa";
        }






        [ActionName("DeleteImage")]
        [HttpGet("{name}")]
        public ActionResult<bool> DeleteImage([FromQuery(Name = "lang")]string lang,  string name)
        {
            if (new UploadRepository(lang).DeleteImage(name, "AccidentImages"))
            {
                return true;

            }
            else
                return false; 
        }


        [ActionName("DeleteImage2")]
        [HttpPost]
        public ActionResult<bool> DeleteImage2([FromQuery(Name ="lang")]string lang , GetCriteria uri)
        {
            if (new UploadRepository(lang).DeleteImage(uri.username))
            {
                return true;

            }
            else
                return false;
        }
    }
}
