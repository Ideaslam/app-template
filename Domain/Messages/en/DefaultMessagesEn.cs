using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class DefaultMessagesEn
    {
        // DATA GET
        public const string NODATA = "THERE IS NO DATA";
        public const string DATAGOT = "DATA GOT CORRECTLY";

        //Operation Error
        public const string INSERT_ERROR = "Error In Insert Data";
        public const string UPDATE_ERROR = "Error In Update Data";
        public const string DELETE_ERROR = "Error In Delete Data";
        public const string UNEXPERROR = "UNEXPECTED ERROR";

        //Operation Correct
        public const string INSERT_CORRECT = "Data Inserted Correctly";
        public const string UPDATE_CORRECT = "Data Updated Correctly";
        public const string DELETE_CORRECT = "Data Deleted Correclty";

        
        
        // AccessToken
        public const string WRONG_ACCESS_TOKEN = "Wrong AccessToken";

        //PhoneNumber
        public const string PHONE_EXISTS = "Phone Number Exists";
        public const string PHONE_NOT_EXISTS = "Phone Number is not Exist";
        public const string PHONE_CHANGED = "PhoneNumber is Changed";
        public const string PHONE_VERIFIED = "PhoneNumber is verified";
        public const string PHONE_NOT_VERIFIED = "PhoneNumber is not verified";
        public const string PHONE_ANOTHER_USER = "phonenumber is registered for another user";


        // Code 
        public const string PHONE_CODE_SENT = "Code is Sent";
        public const string PHONE_CODE_NOT_SENT = "Code is not sent ";

        //Password
        public const string PASS_CORRECT = "Password is Correct";
        public const string PASS_WRONG = "Password is Wrong";

        //Pictures
        public const string PIC_STORE_CORRECT = "تم تخزين الصورة بنجاح";
        public const string PIC_URL_STORE_ERROR = "خطأ في تخزين عنوان الصورة";
        public const string PIC_STORE_ERROR = "خطأ في تخزين الصورة";


        public static string GetMessage(Enum id)
        {

            switch (id)
            {
                case defaultM.NODATA:
                    return NODATA;

                case defaultM.INSERT_ERROR:
                    return INSERT_ERROR;

                case defaultM.UPDATE_ERROR:
                    return UPDATE_ERROR;

                case defaultM.DELETE_ERROR:
                    return DELETE_ERROR;

                case defaultM.INSERT_CORRECT:
                    return INSERT_CORRECT;

                case defaultM.UPDATE_CORRECT:
                    return UPDATE_CORRECT;

                case defaultM.DELETE_CORRECT:
                    return DELETE_CORRECT;

                case defaultM.DATAGOT:
                    return DATAGOT;

                case defaultM.UNEXPERROR:
                    return UNEXPERROR;

                case defaultM.WRONG_ACCESS_TOKEN:
                    return WRONG_ACCESS_TOKEN;

                case defaultM.PHONE_EXISTS:
                    return PHONE_EXISTS;

                case defaultM.PHONE_NOT_EXISTS:
                    return PHONE_NOT_EXISTS;

                case defaultM.PHONE_CHANGED:
                    return PHONE_CHANGED;

                case defaultM.PHONE_VERIFIED:
                    return PHONE_VERIFIED;

                case defaultM.PHONE_NOT_VERIFIED:
                    return PHONE_NOT_VERIFIED;

                case defaultM.PHONE_CODE_SENT:
                    return PHONE_CODE_SENT;

                case defaultM.PHONE_CODE_NOT_SENT:
                    return PHONE_CODE_NOT_SENT;

                case defaultM.PHONE_ANOTHER_USER:
                    return PHONE_ANOTHER_USER;

                case defaultM.PASS_CORRECT:
                    return PASS_CORRECT;

                case defaultM.PASS_WRONG:
                    return PASS_WRONG;

                case defaultM.PIC_STORE_CORRECT:
                    return PIC_STORE_CORRECT;

                case defaultM.PIC_URL_STORE_ERROR:
                    return PIC_URL_STORE_ERROR;

                case defaultM.PIC_STORE_ERROR:
                    return PIC_STORE_ERROR;

                default:
                    return "";

            }

        }





    }
}
