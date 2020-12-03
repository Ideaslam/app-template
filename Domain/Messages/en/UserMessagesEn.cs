using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class UserMessagesEn
    {


        public const string USER_DELETED = "Username is Deleted";
        public const string USER_NOT_DELETED = "Username is not Deleted";
        public const string USER_PASS_ABLE = "Username and Password are Available";
        public const string USER_PASS_NOT_ABLE = "Username and Password are not Available";
        public const string USER_EXISTS = "User Exists";
        public const string USER_NOT_EXIST = "User is not Exist";

        public const string PHONE_NOT_PERSON = "Phone is not Registered as Person";
        public const string PHONE_NOT_WORKSHOP = "Phone is not Registered as Workshop";

        public const string LOGOUT_CORRECT = "Logout Correctly";
        public const string LOGIN_CORRECT = "Login Correctly";

        public const string LOGOUT_ERROR = "Logut Error";
        public const string LOGIN_ERROR = "Login Error";

        public const string DEVICEID_DELETED = "Device id is Deleted";
        public const string DEVICEID_NOT_DELETED = "Device id is not Deleted";

        public const string DEVICEID_UPDATED = "Device Id is Updated";
        public const string DEVICEID_NOT_UPDATED = "Device Id is not Updeted";

        public const string PROFILE_UPDATED = "Profile is Updated";
        public const string PROFILE_NOT_UPDATED = "Profile is not Updated";

        public const string PICTURE_UPDATED = "Picture is Updated";
        public const string PICTURE_NOT_UPDATED = "Picture is not Updated";


        public const string SUPPLIER_TYPE_CHANGED = "Supplier Type is Changed";
        public const string SUPPLIER_TYPE_NOT_CHANGED = "Supplier is not Changed";

        public const string SUPPLIER_NOT_ACTIVE = "Your Account is not Active";




        public static string GetMessage(Enum id)
        {

            switch (id)
            {

                case userM.PROFILE_UPDATED:
                    return PROFILE_UPDATED;

                case userM.PROFILE_NOT_UPDATED:
                    return PROFILE_NOT_UPDATED;

                case userM.PICTURE_UPDATED:
                    return PICTURE_UPDATED;

                case userM.PICTURE_NOT_UPDATED:
                    return PICTURE_NOT_UPDATED;

                case userM.USER_DELETED:
                    return USER_DELETED;

                case userM.USER_NOT_DELETED:
                    return USER_NOT_DELETED;

                case userM.USER_PASS_ABLE:
                    return USER_PASS_ABLE;

                case userM.USER_PASS_NOT_ABLE:
                    return USER_PASS_NOT_ABLE;

                case userM.USER_EXISTS:
                    return USER_EXISTS;

                case userM.USER_NOT_EXIST:
                    return USER_NOT_EXIST;
 

               case userM.LOGOUT_CORRECT:
                    return LOGOUT_CORRECT;

               case userM.LOGIN_CORRECT:
                    return LOGIN_CORRECT;

               case userM.LOGOUT_ERROR:
                    return LOGOUT_ERROR;

               case userM.LOGIN_ERROR:
                    return LOGIN_ERROR;

               case userM.DEVICEID_DELETED:
                    return DEVICEID_DELETED;

               case userM.DEVICEID_NOT_DELETED:
                    return DEVICEID_NOT_DELETED;

                case userM.DEVICEID_UPDATED:
                    return DEVICEID_UPDATED;

                case userM.DEVICEID_NOT_UPDATED:
                    return DEVICEID_NOT_UPDATED;

                case userM.PHONE_NOT_PERSON:
                    return PHONE_NOT_PERSON;

                case userM.PHONE_NOT_WORKSHOP:
                    return PHONE_NOT_WORKSHOP;

                case userM.SUPPLIER_TYPE_CHANGED:
                    return SUPPLIER_TYPE_CHANGED;

                case userM.SUPPLIER_TYPE_NOT_CHANGED:
                    return SUPPLIER_TYPE_NOT_CHANGED;

                case userM.SUPPLIER_NOT_ACTIVE:
                    return SUPPLIER_NOT_ACTIVE;


                default:
                    return "";

            }

        }


    }
}
