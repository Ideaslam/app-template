using Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;
using static Domain.Messages.Messages;

namespace Domain.Messages
{
    public static class MessagesAr 
    {

        public static string GetMessage(Enum id, TypeM type_id)
        {

            switch (type_id)
            {
                case TypeM.OFFER:
                    return OfferMessagesAr.GetMessage(id);
                case TypeM.PERSON:
                    return PersonMessagesAr.GetMessage(id);
                case TypeM.SERVICE:
                    return ServiceMessagesAr.GetMessage(id);
                case TypeM.VEHICLE:
                    return VehicleMessagesAr.GetMessage(id);
                case TypeM.WORKSHOP:
                    return WorkshopMessagesAr.GetMessage(id);
                case TypeM.ACCIDENT:
                    return orderMessagesAr.GetMessage(id);
                case TypeM.USER:
                    return UserMessagesAr.GetMessage(id);
                case TypeM.DEFAULT:
                    return DefaultMessagesAr.GetMessage(id);
                case TypeM.notifyM:
                    return NotiMessagesAr.GetMessage(id);
                default:
                    return "";
            }
        }

    }
}
