using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class orderMessagesAr
    {

        public const string IMAGE_DELETED  = "تم مسح الصورة";
        public const string REQUEST_DEACTIVE  = "تم إلغاء الطلب";
        public const string REQUEST_DELETED  = "تم مسح الطلب";
        public const string REQUEST_NOT_DELETED = "لم يتم مسح الطلب";
        public const string REQUEST_ACTIVE  = "تم تفعيل الطلب";
        public const string IMAGE_Order_STORE = "تم تقديم الطلب بنجاح ";
        public const string IMAGE_Order_ERROR = "لم يتم إدخال الصور";
        public const string Order_ERROR  = "لم يتم إدخال الطلب";

        public const string Order_DATA_FOUND = "تم إسترجاع بيانات الطلب";
        public const string Order_DATA_NOT_FOUND = "بيانات الطلب غير موجودة";

        public const string VIDEO_Order_STORE = "تم تخزين الفيديو بنجاح";
        public const string VIDEO_Order_ERROR= "لم يتم تخزين الفيديو";



        public static string GetMessage(Enum id)
        {

            switch (id)
            {
                case orderM.IMAGE_DELETED:
                    return IMAGE_DELETED;

                case orderM.REQUEST_DEACTIVE:
                    return REQUEST_DEACTIVE;

                case orderM.REQUEST_DELETED:
                    return REQUEST_DELETED;

                case orderM.REQUEST_NOT_DELETED:
                    return REQUEST_NOT_DELETED;

                case orderM.REQUEST_ACTIVE:
                    return REQUEST_ACTIVE;

                case orderM.IMAGE_Order_STORE:
                    return IMAGE_Order_STORE;

                case orderM.IMAGE_Order_ERROR:
                    return IMAGE_Order_ERROR;

                case orderM.Order_ERROR:
                    return Order_ERROR;

                case orderM.Order_DATA_NOT_FOUND:
                    return Order_DATA_NOT_FOUND;

                case orderM.Order_DATA_FOUND:
                    return Order_DATA_FOUND;

                case orderM.VIDEO_Order_STORE:
                    return VIDEO_Order_STORE;


                case orderM.VIDEO_Order_ERROR:
                    return VIDEO_Order_ERROR;


            


                default:
                    return "";

            }

        }



    }
}
