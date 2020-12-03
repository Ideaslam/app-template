using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Messages
{
   public static  class Messages  
    {


        public enum TypeM
        {
            USER,
            OFFER,
            PERSON,
            SERVICE,
            VEHICLE,
            WORKSHOP,
            ACCIDENT,
            DEFAULT,
            EMPTYEXC,
            INSERTEXC,
            UPDATEEXC,
            DELETEEXC,
            notifyM


        }


        public enum language
        {
            ar,
            en
        }

        // Messages Ids 

       

       
     

        public enum defaultM
        {
         NODATA ,
         INSERT_ERROR ,
         UPDATE_ERROR ,
         DELETE_ERROR,
         INSERT_CORRECT ,
         UPDATE_CORRECT ,
         DELETE_CORRECT,
         DATAGOT ,
         UNEXPERROR ,
         WRONG_ACCESS_TOKEN ,
         PHONE_EXISTS ,
         PHONE_NOT_EXISTS ,
         PHONE_CHANGED ,
         PHONE_VERIFIED,
         PHONE_NOT_VERIFIED,
         PHONE_CODE_SENT,
         PHONE_CODE_NOT_SENT,
         PHONE_ANOTHER_USER,

            PASS_CORRECT ,
         PASS_WRONG  ,
         PIC_STORE_CORRECT,
         PIC_STORE_ERROR,
         PIC_URL_STORE_ERROR 
        }




        public enum orderM
        {
         IMAGE_DELETED  ,
         REQUEST_DEACTIVE  ,
         REQUEST_DELETED  ,
         REQUEST_NOT_DELETED,
            REQUEST_ACTIVE  ,
         IMAGE_Order_STORE  ,
            IMAGE_Order_ERROR,

            VIDEO_Order_STORE,
            VIDEO_Order_ERROR,

            Order_ERROR,
            Order_DATA_FOUND,
            Order_DATA_NOT_FOUND
        }

        public enum offerM
        {
            OFFER_CREATE,
            OFFER_ACCEPT,
            OFFER_REJECT,
            CONFIRM_REPAIR,
            FINISH_REPAIR,
            CONFIRM_DELIVER,
            OFFER_DELETE,
            REQUEST_CREATE,
            OFFER_FOUND,
            OFFER_NOT_FOUND,
            Supplier_InActive
        }

        public enum serviceM
        {
         SERVICE_CREATE ,
         SERVICE_CANCEL ,
         SERVICE_FINISH,
         SERVICE_CHANGE_PASSWORD ,
         SERVICE_NOT_CHANGE_PASSWORD,
         SERVICE_COMPARE_PASSNOTFOUND ,
         SERVICE_CHANGE_PHONE  ,
         SERVICE_NOTI_SENT,
         SERVICE_NOTI_NOT_SENT,
        }


        public enum vehicleM
        {
         FIXPAPER_STORE ,
         VEHICLE_STORE,
         IMAGE_NOT_FOUND,
         VEHICLE_DATA_NOT_GOT


        }


        public enum userM
        {
            USER_DELETED,
            USER_NOT_DELETED,
            USER_PASS_ABLE,
            USER_PASS_NOT_ABLE,
            USER_EXISTS,
            USER_NOT_EXIST,
            PHONE_NOT_PERSON ,
            PHONE_NOT_WORKSHOP,

            LOGOUT_CORRECT,
            LOGIN_CORRECT,
            LOGOUT_ERROR,
            LOGIN_ERROR,

            DEVICEID_DELETED,
            DEVICEID_NOT_DELETED,

            DEVICEID_UPDATED,
            DEVICEID_NOT_UPDATED,
            PROFILE_UPDATED ,
            PICTURE_UPDATED ,
            PROFILE_NOT_UPDATED,
            PICTURE_NOT_UPDATED,
            SUPPLIER_TYPE_CHANGED,
            SUPPLIER_TYPE_NOT_CHANGED,
            SUPPLIER_NOT_ACTIVE

        }


        public enum notifyM
        {
           NOTI_NEW_ORDER,
           NOTI_NEW_OFFER,
           NOTI_OFFER_DELETED,
           NOTI_ORDER_CANCELED,
           NOTI_OFFER_ACCEPTED,
           NOTI_OFFER_REJECTED,
           NOTI_ORDER_STARTED,
           NOTI_ORDER_FINISHED,
           NOTI_ORDER_DELIVERED,
           NOTI_TITLE,
           NOTI_TITLE_SUPPLIER_NOT_ACTIVE,
           NOTI_BODY_SUPPLIER_NOT_ACTIVE

        }
 






        public static string GetMessage(string lang , TypeM type_id , Enum id ) 
        {
            
            if(lang == language.ar.ToString())
            {
                return   MessagesAr.GetMessage(id , type_id);
            }
            else
            {
                return   MessagesEn.GetMessage(id, type_id);

            }

           
        }

       



    }
}
