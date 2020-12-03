using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class VehicleMessagesAr
    {
        public const string FIXPAPER_STORE = "تم حفظ ورقة الإصلاح";
        public const string VEHICLE_STORE = "تم حفظ بيانات المركبة";
        public const string IMAGE_NOT_FOUND = "بيانات صور السيارة غير موجودة";
        public const string VEHICLE_DATA_NOT_GOT = "بيانات  السيارة غير موجودة";


        public static string GetMessage(Enum id)
        {

            switch (id)
            {

                case vehicleM.FIXPAPER_STORE:
                    return FIXPAPER_STORE;

                case vehicleM.VEHICLE_STORE:
                    return VEHICLE_STORE;

                case vehicleM.IMAGE_NOT_FOUND:
                    return IMAGE_NOT_FOUND;

                case vehicleM.VEHICLE_DATA_NOT_GOT:
                    return VEHICLE_DATA_NOT_GOT;

                default:
                    return "";

            }

        }




    }
}
