using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class OrderMessagesEn
    {

        public const string IMAGE_DELETED  = "Image is Deleted";
        public const string REQUEST_DEACTIVE  = "Request Deactivated";
        public const string REQUEST_DELETED  = "Request Deleted";
        public const string REQUEST_NOT_DELETED = "Request is not Deleted";

        public const string REQUEST_ACTIVE  = "Request Activated";
        public const string IMAGE_Order_STORE = "Order Data inserted Correctly";
        public const string IMAGE_Order_ERROR = "Images are Not Inserted";
        public const string Order_ERROR = "Order Data are Not Inserted";
        public const string Order_DATA_FOUND = "Order Data Got";
        public const string Order_DATA_NOT_FOUND = "Order Data are not found";

        public const string VIDEO_Order_STORE = "Video is stored correctly";
        public const string VIDEO_Order_ERROR = "Video is not stored";


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
