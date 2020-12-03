using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
    public class NotiMessagesEn
    {



        public const string NOTI_NEW_ORDER = "New Order Created";
        public const string NOTI_NEW_OFFER = "New Offer Created For your Order";
        public const string NOTI_OFFER_DELETED = "There is an Deleted Offer by Supplier";
        public const string NOTI_ORDER_CANCELED = "There is an Cancelled Order";
        public const string NOTI_OFFER_ACCEPTED = "Your Offer Was Accepted";
        public const string NOTI_OFFER_REJECTED = "Your Offer was Rejected";
        public const string NOTI_ORDER_STARTED = "Order was Started";
        public const string NOTI_ORDER_FINISHED = "Order was Finished";
        public const string NOTI_ORDER_DELIVERED = "Order was Delivered";

        public const string NOTI_TITLE = "Islah App";

        public const string NOTI_BODY_SUPPLIER_NOT_ACTIVE = "Your Account is in Activating phase"; 



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
