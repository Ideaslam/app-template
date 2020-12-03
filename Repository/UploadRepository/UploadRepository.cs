﻿using Domain.Exceptions;
using Domain.Interfaces.Upload;
using Domain.Messages;
using HelperClass;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using static Domain.Messages.Messages;

namespace Repository.UploadRepository
{
  public  class UploadRepository : IUploadRepository
    {

        string language; 

        public UploadRepository( string language )
        {
            this.language = language;
        }



         public string StoreImage(string base64String, string URL_IMAGE)
        {
            byte[] imgBytes = Convert.FromBase64String(base64String);
            string url = @"../islahImages/"+ URL_IMAGE;
            try
            {
                using (var imageFile = new FileStream(url, FileMode.Create))
                {
                    imageFile.Write(imgBytes, 0, imgBytes.Length);
                    imageFile.Flush();
                }
            }
            catch (Exception ex )
            {
                url = "";
            }
          
            return url;
        }


        public bool DeleteImage(string imageName , string ImageType)
        {
            try
            {
                string filePath = @"../IslahImages/images/" + ImageType + "/"+ imageName + ".png";
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public bool DeleteImage(string uri )
        {
            try
            {
                 string filePath = @"../IslahImages/" + uri;
               

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        public async System.Threading.Tasks.Task<Response> storeOrderVideoAsync( IFormFile file  ,  int accident_id   )
        {

            List<IFormFile> files = new List<IFormFile>();
            files.Add(file);
            var filePath = "";
            try
            {


                foreach (var formFile in files)
                {
                    int MaxContentLength = 1024 * 1024 * 10; //Size = 10 MB  
                    IList<string> AllowedFileExtensions = new List<string> { ".mp4" };

                  //  var ext = formFile.FileName.Substring(formFile.FileName.LastIndexOf('.'));
                   // var extension = ext.ToLower();
                    string path = ("videos" + @"/" + "accidents" + @"/" + accident_id + ".mp4");

                    filePath = ("../islahImages/" + path);


                    if (!AllowedFileExtensions.Contains(".mp4"))
                    {
                        var message = string.Format("Please Upload image of type .mp4");
                        return new Response(false, message, message);
                    }
                    else if (formFile.Length > MaxContentLength)
                    {
                        var message = string.Format("Please Upload a file upto 10 mb.");
                        return new Response(false, message, message);
                    }
                    else
                    {
                        if (formFile.Length > 0)
                        {
                            using (var stream = new System.IO.FileStream(filePath, System.IO.FileMode.Create))
                            {
                                try
                                {
                                    await formFile.CopyToAsync(stream);
                                }
                                catch (InsertException  )
                                {
                                    throw new InsertException(language,"ERROR IN STORING VIDEO ");
                                }
                                  

                                new OrderRepository.OrderRepository(language).InsertOrderPictureUrl(path, 2, accident_id);
                                  return new Response(true, "ACCIDENT VIDEO IS STORED"); 
                            }
                        

                        }
                        else
                        {
                            var message = string.Format("length is Zero");
                            return new Response(false, message, message);
                        }
                    }
                }

                return new Response(false, "THERE IS NO VIDEO ", "THERE IS NO VIDEO");
            }
            catch (InsertException InsertException)
            {
                return new Response(false, InsertException.ErrorMessage ,InsertException.ErrorMessage);
            }
            catch (Exception ex)
            {
                return new Response(false, Messages.GetMessage(language, TypeM.DEFAULT , defaultM.UNEXPERROR));
            }

         
        }



     


        // public Response storeAccidentVideo()
        //{
        //    try
        //    {
        //        var file = Request.Form.Files["video"];
        //        var folderName = Path.Combine("Resources", "Images");
        //        var pathToSave = @"../IslahtestImages\videos\accidents";//Path.Combine(Directory.GetCurrentDirectory(), folderName);

        //        if (file.Length > 0)
        //        {
        //            var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
        //            var fullPath = Path.Combine(pathToSave, fileName);
        //            var dbPath = Path.Combine(folderName, fileName);

        //            using (var stream = new FileStream(fullPath, FileMode.Create))
        //            {
        //                file.CopyTo(stream);
        //            }

        //            return new Response(true, "VIDEO IS STORED");
        //        }
        //        else
        //        {
        //            return new Response(false, "VIDEO HAS NO LENGTH");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return new Response(false, ex.Message);
        //    }
        //}
}
}