using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class UserMessagesAr
    {

        public const string USER_DELETED  = "تم مسح المستخدم";
        public const string USER_NOT_DELETED = "لم يتم مسح المستخدم";
        public const string USER_PASS_ABLE = "اسم مستخدم وكلمة مرور متاحيين";
        public const string USER_PASS_NOT_ABLE = "اسم مستخدم وكلمة غير مرور متاحيين";
        public const string USER_EXISTS = "اسم المستخدم موجود مسبقا ";
        public const string USER_NOT_EXIST = "اسم المستخدم غير موجود مسبقا ";

        public const string PHONE_NOT_PERSON = "رقم الجوال ليس لمستخدم شخصي";
        public const string PHONE_NOT_WORKSHOP = "رقم الجوال غير مسجل كمقدم خدمة";


        public const string LOGOUT_CORRECT = "تم تسجيل الخروج بنجاح";
        public const string LOGIN_CORRECT = "تم تسجيل الدخول بنجاح";

        public const string LOGOUT_ERROR = "خطأ في تسجيل الخروج";
        public const string LOGIN_ERROR = "خطأ في تسجيل الدخول";

        public const string DEVICEID_DELETED = "تم مسح رقم الجهاز";
        public const string DEVICEID_NOT_DELETED = "لم يتم مسح رقم الجهاز";

        public const string DEVICEID_UPDATED = "تم تحديث رقم الجهاز";
        public const string DEVICEID_NOT_UPDATED = "لم يتم تحديث رقم الجهاز";

        public const string PROFILE_UPDATED = "تم تحديث الملف الشخصي";
        public const string PROFILE_NOT_UPDATED = "لم يتم تحديث الملف الشخصي";


        public const string PICTURE_UPDATED = "تم تحديث الصورة الشخصية";
        public const string PICTURE_NOT_UPDATED = "لم يتم تحديث الصورة الشخصية";


        public const string SUPPLIER_TYPE_CHANGED = "تم تغيير نوع مقدم الخدمة";
        public const string SUPPLIER_TYPE_NOT_CHANGED = "لم يتم تغيير مقدم الخدمة";
        public const string SUPPLIER_NOT_ACTIVE = "جاري تفعيل حسابك";



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

