using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class OfferMessagesAr  
    {

        

        public const string OFFER_CREATE_AR = "تم تقديم العرض بنجاح";
        public const string OFFER_ACCEPT_AR = "تمت الموافقة على العرض";
        public const string OFFER_REJECT_AR = "تم  رفض العرض";
        public const string CONFIRM_REPAIR_AR = "تم بدء الإصلاح";
        public const string FINISH_REPAIR_AR = "تم الإنتهاء من الإصلاح";
        public const string CONFIRM_DELIVER_AR = "تم إستلام المركبة";
        public const string OFFER_DELETE_AR = "تم مسح العرض";
        public const string REQUEST_CREATE_AR = "تم إنشاء طلب";
        public const string OFFER_FOUND = "تم إسترجاع بيانات العرض"; 
        public const string OFFER_NOT_FOUND = "لا يوجد عروض";
        



        public static string GetMessage (Enum id )
        {
             
                switch (id)
                {
                    case offerM.OFFER_CREATE :
                        return OFFER_CREATE_AR;

                    case offerM.OFFER_ACCEPT :
                        return OFFER_ACCEPT_AR;

                    case offerM.OFFER_REJECT :
                        return OFFER_REJECT_AR;

                    case offerM.CONFIRM_REPAIR :
                        return CONFIRM_REPAIR_AR;

                    case offerM.FINISH_REPAIR :
                        return FINISH_REPAIR_AR;

                    case offerM.CONFIRM_DELIVER  :
                        return CONFIRM_DELIVER_AR;

                    case offerM.OFFER_DELETE :
                        return OFFER_DELETE_AR;

                    case offerM.REQUEST_CREATE :
                        return REQUEST_CREATE_AR;

                case offerM.OFFER_FOUND:
                    return OFFER_FOUND;

                case offerM.OFFER_NOT_FOUND:
                    return OFFER_NOT_FOUND;

                
                default:
                        return "";

                }
           
        }




    }
}
