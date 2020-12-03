using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
    public class MessagesEn
    {
        public static string GetMessage(Enum id, TypeM type_id)
        {
            
            switch (type_id)
            {
                case TypeM.OFFER:
                    return OfferMessagesEn.GetMessage(id);
                case TypeM.PERSON:
                    return PersonMessagesEn.GetMessage(id);
                case TypeM.SERVICE:
                    return ServiceMessagesEn.GetMessage(id);
                case TypeM.VEHICLE:
                    return VehicleMessagesEn.GetMessage(id);
                case TypeM.WORKSHOP:
                    return WorkshopMessagesEn.GetMessage(id);
                case TypeM.ACCIDENT:
                    return OrderMessagesEn.GetMessage(id);
                case TypeM.USER:
                    return UserMessagesEn.GetMessage(id);
                case TypeM.DEFAULT:
                    return DefaultMessagesEn.GetMessage(id);
                case TypeM.notifyM:
                    return NotiMessagesEn.GetMessage(id);
                default:
                    return "";

            }

        }




    }
}
