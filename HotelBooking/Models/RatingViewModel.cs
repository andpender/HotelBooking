using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class RatingViewModel
    {
        public int Score { get; set; }

        public string Content { get; set; }

        public int HotelId { get; set; }
    }
}