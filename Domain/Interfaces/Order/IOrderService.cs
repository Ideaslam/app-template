using System;
using System.Collections.Generic;
using System.Text;

using Domain.Entities;
using Domain.Entities.Order;
using HelperClass;


namespace Domain.Interfaces.Order
{
    public interface IOrderService
    {
        Response StoreOrderData(CarInfoOrder carInfoOrder, string user_id);
        
        Response DeleteImage(string image_url);
     
    }
}
