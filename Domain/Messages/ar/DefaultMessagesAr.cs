using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
    public class DefaultMessagesAr
    {
        // Data GET
        public const string NODATA = "لا توجد بيانات";
        public const string DATAGOT = "تم إستدعاء البيانات بنجاح";

        // operation Error
        public const string INSERT_ERROR = "خطأ في إدخال البيانات";
        public const string UPDATE_ERROR = "خطأ في تعديل البيانات";
        public const string DELETE_ERROR = "خطأ في حذف البيانات";
        public const string UNEXPERROR = "خطأ غير متوقع";
        //Operation Correct
        public const string INSERT_CORRECT = "تم إدخال البيانات بنجاح";
        public const string UPDATE_CORRECT = "تم تعديل البيانات بنجاح";
        public const string DELETE_CORRECT = "تم مسح البيانات بنجاح";
       

        // accessToekn 
        public const string WRONG_ACCESS_TOKEN = "رقم مستخدم غير صحيح";

        //PhoneNumber 
        public const string PHONE_EXISTS = "رقم الجوال موجود مسبقا";
        public const string PHONE_NOT_EXISTS = "رقم الجوال غير موجود مسبقا";
        public const string PHONE_CHANGED = "تم تغيير الرقم بنجاح";
        public const string PHONE_VERIFIED = "تم تأكيد الرقم";
        public const string PHONE_NOT_VERIFIED = "لم يتم تأكيد الرقم";
        public const string PHONE_ANOTHER_USER = "الرقم متاح لمسجل لمستخدم آخر";

        // Code 
        public const string PHONE_CODE_SENT = "تم إرسال الكود";
        public const string PHONE_CODE_NOT_SENT = "لم يتم إرسال الكود";

        //Password
        public const string PASS_CORRECT = "الرقم السري صحيح";
        public const string PASS_WRONG = "الرقم السري خطأ";

        // Pictures 
        public const string PIC_STORE_CORRECT = "تم تخزين الصورة بنجاح";
        public const string PIC_URL_STORE_ERROR = "خطأ في تخزين عنوان الصورة";
        public const string PIC_STORE_ERROR = "خطأ في تخزين الصورة";



        public static string GetMessage(Enum id)
        {

            switch (id)
            {
                case defaultM.NODATA:
                    return NODATA;

                case defaultM.INSERT_ERROR:
                    return INSERT_ERROR;

                case defaultM.UPDATE_ERROR:
                    return UPDATE_ERROR;

                case defaultM.DELETE_ERROR:
                    return DELETE_ERROR;

                case defaultM.INSERT_CORRECT:
                    return INSERT_CORRECT;

                case defaultM.UPDATE_CORRECT:
                    return UPDATE_CORRECT;

                case defaultM.DELETE_CORRECT:
                    return DELETE_CORRECT;

                case defaultM.DATAGOT:
                    return DATAGOT;

                case defaultM.UNEXPERROR:
                    return UNEXPERROR;

                case defaultM.WRONG_ACCESS_TOKEN:
                    return WRONG_ACCESS_TOKEN;

                case defaultM.PHONE_EXISTS:
                    return PHONE_EXISTS;

                case defaultM.PHONE_NOT_EXISTS:
                    return PHONE_NOT_EXISTS;  

               case defaultM.PHONE_CHANGED:
                    return PHONE_CHANGED;

                case defaultM.PHONE_VERIFIED:
                    return PHONE_VERIFIED;

                case defaultM.PHONE_NOT_VERIFIED:
                    return PHONE_NOT_VERIFIED;
 
                case defaultM.PHONE_CODE_SENT:
                    return PHONE_CODE_SENT;

                case defaultM.PHONE_CODE_NOT_SENT:
                    return PHONE_CODE_NOT_SENT;

                case defaultM.PHONE_ANOTHER_USER:
                    return PHONE_ANOTHER_USER;

                case defaultM.PASS_CORRECT:
                    return PASS_CORRECT;

                case defaultM.PASS_WRONG:
                    return PASS_WRONG;

                case defaultM.PIC_STORE_CORRECT:
                    return PIC_STORE_CORRECT;

                case defaultM.PIC_URL_STORE_ERROR:
                    return PIC_URL_STORE_ERROR;

                case defaultM.PIC_STORE_ERROR:
                    return PIC_STORE_ERROR;

                default:
                    return "";

            }

        }




    }
}
