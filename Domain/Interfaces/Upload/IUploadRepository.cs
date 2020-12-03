using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Upload
{
   public  interface IUploadRepository 
    {

        string StoreImage(string base64String, string URL_IMAGE);

    }
}
