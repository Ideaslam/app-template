using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.supplier
{
    public class WarshaDTO
    {
        public int id { get; set; }
        public int user_id { get; set; }
        public string recordNumber { get; set; }
        public DateTime expiryDate { get; set; }
        public string shopNumber { get; set; }
        public string shopName { get; set; }
        public int industrialAreaId { get; set; }
        public string industrialAreaName { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public string fullName { get; set; }
        public double rating { get; set; }
        public string email { get; set; }

    }
}
