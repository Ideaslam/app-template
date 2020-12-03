using Domain.Interfaces.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace HelperClass
{
   public class Response
    {

        public int state { get; set; } = 0;
        public bool status { get; set; } = false;
        public string Message  { get; set; } = "";
        public string ErrorMessage { get; set; } = "";
        public object innerData { get; set; }= new object();
        // public dynamic test { get; set; }

        public Response( )
        {
            

        }

        public Response(bool status, string Message )
        {
            this.status = status;
            this.Message = Message;
            this.innerData = new object();
            this.ErrorMessage = "";

        }
        public Response(bool status, string Message , Exception exception)
        {
            this.status = status;
            this.Message = Message;
            this.innerData = new object();
            this.ErrorMessage = exception.Message;

            if (exception.GetType().Name != "EmptyViewException")
                InsertLog(ErrorMessage +"  : " + Message + " : " + DateTime.Now.ToString());


        }

        public Response(bool status,string Message,  object innerData)
        {
            this.status = status;
            this.Message = Message;
            this.innerData = innerData; 
            this.ErrorMessage = "";
 
        }

        public Response(bool status, string Message,   Exception exception ,object innerData)
        {
            this.status = status;
            this.Message = Message;
            this.innerData = innerData;
            this.ErrorMessage = exception.Message;
             
            if(exception.GetType().Name!= "EmptyViewException")
            InsertLog(ErrorMessage + "  " + DateTime.Now.ToString());

        }


        public void InsertLog(string line)
        {
            try
            {
                string path = @"..\IslahImages\logs\errorLog.txt";
                // This text is added only once to the file.
                if (!File.Exists(path))
                {
                    // Create a file to write to.
                    using (StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(line);
                    }
                }

                // This text is always added, making the file longer over time
                // if it is not deleted.
                using (StreamWriter sw = File.AppendText(path))
                {
                    sw.WriteLine(line);

                }
            }
            catch (Exception ex )  
            {

            }
          

            // Open the file to read from.
            //using (StreamReader sr = File.OpenText(path))
            //{
            //    string s = "";
            //    while ((s = sr.ReadLine()) != null)
            //    {
            //        Console.WriteLine(s);
            //    }
            //}
        }



    }
}
