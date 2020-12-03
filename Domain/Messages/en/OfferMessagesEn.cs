using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
    public class OfferMessagesEn
    {

        

        public const string OFFER_CREATE_EN = "Offer Inserted Correctly";
        public const string OFFER_ACCEPT_EN = "Offer Accepted Correctly";
        public const string OFFER_REJECT_EN = "Offer Rejected";
        public const string CONFIRM_REPAIR_EN = "Confirm Repair";
        public const string FINISH_REPAIR_EN = "Order Finished";
        public const string CONFIRM_DELIVER_EN = "Delivered";
        public const string OFFER_DELETE_EN = "Offer Deleted";
        public const string REQUEST_CREATE_EN = "Request Created";

        public const string OFFER_FOUND = "Offer Data are Found";
        public const string OFFER_NOT_FOUND = "Offers are not Found";
        


        public static string GetMessage(Enum id)
        {

            switch (id)
            {
                case offerM.OFFER_CREATE:
                    return OFFER_CREATE_EN;

                case offerM.OFFER_ACCEPT:
                    return OFFER_ACCEPT_EN;

                case offerM.OFFER_REJECT:
                    return OFFER_REJECT_EN;

                case offerM.CONFIRM_REPAIR:
                    return CONFIRM_REPAIR_EN;

                case offerM.FINISH_REPAIR:
                    return FINISH_REPAIR_EN;

                case offerM.CONFIRM_DELIVER:
                    return CONFIRM_DELIVER_EN;

                case offerM.OFFER_DELETE:
                    return OFFER_DELETE_EN;

                case offerM.REQUEST_CREATE:
                    return REQUEST_CREATE_EN;

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
 
