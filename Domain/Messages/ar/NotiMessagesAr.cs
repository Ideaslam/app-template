using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
    public class NotiMessagesAr
    {



        public const string NOTI_NEW_ORDER = "يوجد طلب جديد";
        public const string NOTI_NEW_OFFER = "يوجد عرض جديد لطبلك";
        public const string NOTI_OFFER_DELETED = "تم مسح عرض من قبل مقدم الخدمة";
        public const string NOTI_ORDER_CANCELED = "تم إلغاء طلب";
        public const string NOTI_OFFER_ACCEPTED = "تم قبول عرضك";
        public const string NOTI_OFFER_REJECTED = "تم رفض عرضك";
        public const string NOTI_ORDER_STARTED = "تم البدء في تقديم الخدمة";
        public const string NOTI_ORDER_FINISHED = "تم تنفيذ الخدمة";
        public const string NOTI_ORDER_DELIVERED = "تم إستلام السيارة";

        public const string NOTI_TITLE = "تطبيق ديلx";

        public const string NOTI_BODY_SUPPLIER_NOT_ACTIVE = "جاري تفعيل حسابك";






        public static string GetMessage (Enum id )
        {
             
                switch (id)
                {
                case notifyM.NOTI_NEW_ORDER:
                    return NOTI_NEW_ORDER;

                case notifyM.NOTI_NEW_OFFER:
                    return NOTI_NEW_OFFER;

                case notifyM.NOTI_OFFER_DELETED:
                    return NOTI_OFFER_DELETED;

                case notifyM.NOTI_ORDER_CANCELED:
                    return NOTI_ORDER_CANCELED;

                case notifyM.NOTI_OFFER_ACCEPTED:
                    return NOTI_OFFER_ACCEPTED;

                case notifyM.NOTI_OFFER_REJECTED:
                    return NOTI_OFFER_REJECTED;

                case notifyM.NOTI_ORDER_STARTED:
                    return NOTI_ORDER_STARTED;

                case notifyM.NOTI_ORDER_FINISHED:
                    return NOTI_ORDER_FINISHED;

                case notifyM.NOTI_ORDER_DELIVERED:
                    return NOTI_ORDER_DELIVERED;

                case notifyM.NOTI_TITLE:
                    return NOTI_TITLE;

                case notifyM.NOTI_BODY_SUPPLIER_NOT_ACTIVE:
                    return NOTI_BODY_SUPPLIER_NOT_ACTIVE;
                default:
                    return "";

            }
           
        }




    }
}
