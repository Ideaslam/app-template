using Domain.Entities;
using Domain.Entities.Order;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Interfaces.Order
{
    public interface IOrderRepository
    {
        bool InsertOrderData(CarInfoOrder carInfoAccident,string user_id);
        bool InsertOrderPictureUrl(string imageUrl, int imageType, int accidentId);
        bool DeleteImageByUrl(string image_url);
    }
}
