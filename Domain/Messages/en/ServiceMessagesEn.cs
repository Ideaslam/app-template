using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class ServiceMessagesEn 
    {

        public const string SERVICE_CREATE = "Service Ordered successfully";
        public const string SERVICE_CANCEL = "Service Canceled successfully";
        public const string SERVICE_FINISH = "Service finished";
        public const string SERVICE_COMPARE_PASSNOTFOUND = "Your Password is Not Correct";
        public const string SERVICE_CHANGE_PASSWORD = "Password Changed Correctly";
        public const string SERVICE_NOT_CHANGE_PASSWORD = "Password is not Chagned";
        public const string SERVICE_CHANGE_PHONE = "Phone Changed Correctly";
        public const string SERVICE_NOTI_SENT = "Notification Sent Successfully";
        public const string SERVICE_NOTI_NOT_SENT = "Notification Not Sent";



        public static string GetMessage(Enum id)
        {

            switch (id)
            {
                case serviceM.SERVICE_CREATE:
                    return SERVICE_CREATE;

                case serviceM.SERVICE_CANCEL:
                    return SERVICE_CANCEL;

                case serviceM.SERVICE_FINISH:
                    return SERVICE_FINISH;

                case serviceM.SERVICE_CHANGE_PASSWORD:
                    return SERVICE_CHANGE_PASSWORD;

                case serviceM.SERVICE_NOT_CHANGE_PASSWORD:
                    return SERVICE_NOT_CHANGE_PASSWORD;

                case serviceM.SERVICE_COMPARE_PASSNOTFOUND:
                    return SERVICE_COMPARE_PASSNOTFOUND;

                case serviceM.SERVICE_CHANGE_PHONE:
                    return SERVICE_CHANGE_PHONE;

                default:
                    return "";

            }

        }





    }
}
