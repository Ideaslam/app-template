using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class ServiceMessagesAr
    {

        public const string SERVICE_CREATE  = "تم طلب الخدمة بنجاح";
        public const string SERVICE_CANCEL  = "تم إلغاء الخدمة بنجاح";
        public const string SERVICE_FINISH  = "تم إنهاء الخدمة";
        public const string SERVICE_CHANGE_PASSWORD  = "تم تغيير الرقم السري بنجاح";
        public const string SERVICE_NOT_CHANGE_PASSWORD = "لم يتم تغيير الرقم السري بنجاح";
        public const string SERVICE_COMPARE_PASSNOTFOUND  = "رقمك السري غير صحيح";

        public const string SERVICE_CHANGE_PHONE   = "تم تغيير رقم الجوال بنجاح";
        public const string SERVICE_NOTI_SENT = "تم إرسال الإشعار";
        public const string SERVICE_NOTI_NOT_SENT = "لم يتم إرسال الإشعار"; 




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
