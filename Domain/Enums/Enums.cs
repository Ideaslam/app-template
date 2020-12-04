using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Enums
{
    public class Enums
    {

        const string SERVER_IP  = "alexcarfix.ddns.net";
        public string APP_DIRECTORY { get; set; } = "http://alexcarfix.ddns.net:8081/";




        public enum OfferType
        {
            offerNotAccepted = 0 ,
            offerAccepted = 1,
            offerIsFixing = 2,
            offerFinishFixing = 3
        }

        public enum UserType
        {
            person = 1,
            workshop = 2,
            
        }


        public enum CreationNumberCode
        {
            order = 65,
            offer = 52,
            user  =  12,
            vehicle = 25

        }


        public enum orderStatus
        {
            REJECTED =-1,
            UNCONFIRMED = 0,
            CONFIRMED = 1,
            WAITINGTOFINISH = 2,
            FINISHED = 3,
            DELIVERED = 4,
        }


        public enum orderActive
        {
            active = 1,
            InActive = 0,
        }


        public enum RateType
        {
                    EXCELLENT = 1,
                    VERY_GOOD = 2,
                    GOOD = 3,
                    ACCEPTED = 4,
                    BAD = 5 
        }

        public enum SortOrder
        {
            asc = 1,
            desc = 2,
        }

        public enum DataLimit
        {
           limit =2 
        }


        public RateType checkRateType(double rate)
        {
            if (rate >= 4.5)
                return RateType.EXCELLENT;
            else if (rate >= 4)
                return RateType.VERY_GOOD;
            else if (rate >= 3.5)
                return RateType.GOOD;
            else if (rate >= 3)
                return RateType.ACCEPTED;
            else if (rate >= 2.5)
                return RateType.BAD;
            else
                return RateType.BAD;

        }

        public string checkRateTypeWords(double rate ,string lang)
        {

            if (lang == "ar")
            {
                if (rate >= 4.5)
                    return "ممتاز";
                else if (rate >= 4)
                    return "جيد جدا";
                else if (rate >= 3.5)
                    return "جيد";
                else if (rate >= 3)
                    return "مقبول";
                else if (rate >= 2.5)
                    return "سيء";
                else
                    return "سيء";
            }
            else
            {
                if (rate >= 4.5)
                    return "Excellent";
                else if (rate >= 4)
                    return "Very Good";
                else if (rate >= 3.5)
                    return "Good";
                else if (rate >= 3)
                    return "Accepted";
                else if (rate >= 2.5)
                    return "Bad";
                else
                    return "Bad";
            }
            
        }

    }
}
