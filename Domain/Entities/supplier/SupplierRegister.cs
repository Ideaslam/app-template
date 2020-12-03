using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.supplier
{
   public class SupplierRegister
    {
        public string phoneNumber { set; get; } = "";
        public int country_code { set; get; } = 0;
        public string password { set; get; } = "";
        public string supplierName { set; get; } = "";
        public string storeName { set; get; } = "";
        public int storeNo { set; get; } = 0;
        public int supplierType { set; get; } = 0;
        public int city { set; get; } = 0;
        public string address { set; get; } = "";
        public double locationX { set; get; } = 0;
        public double locationY { set; get; } = 0;
        public string image { set; get; } = "";
    }
}
