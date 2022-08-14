using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class Room
    {
        public int Id { get; set; }

        [Required]
        public int NumBeds { get; set; }

        public Availability Availability { get; set; }

        public virtual Hotel Hotel { get; set; }
    }

}