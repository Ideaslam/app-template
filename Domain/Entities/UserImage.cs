using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class UserImage
    {
        public int accidentId { get; set; }
        public string imageUrl { get; set; }
        public int imageType { get; set; }
        public string accessToken { get; set; }

    }
}
