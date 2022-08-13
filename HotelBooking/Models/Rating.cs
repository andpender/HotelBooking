using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class Rating
    {
        public int Id { get; set; }

        [Required]
        public int Score { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string Content { get; set; }

        public virtual Hotel Hotel { get; set; }
    }
}