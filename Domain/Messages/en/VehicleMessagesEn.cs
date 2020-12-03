using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
   public class VehicleMessagesEn
    {

        public const string FIXPAPER_STORE  = "Fix Paper Stored";
        public const string VEHICLE_STORE = "vehicle Data Stored";
        public const string IMAGE_NOT_FOUND = "Vehicle images are not found";
        public const string VEHICLE_DATA_NOT_GOT = "vehicle Data are not found";




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
