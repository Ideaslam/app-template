using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Vehicle
    {
        public int id { get; set; } =0;
        // public string plateNumber { get; set; }
        public string brandId { get; set; } = "";
        public string brandName { get; set; } = "";
        public string modelId { get; set; } = "";
        public string modelName { get; set; } = "";
        public string colorId { get; set; } = "";
        public string colorName { get; set; } = "";
        //  public int registrationType { get; set; }
        public string foundDate { get; set; } = "";
        public string accessToken { get; set; } = "";

    }
}
