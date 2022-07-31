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
        public string NumBeds { get; set; }

        [Required]
        public string Address { get; set; }

        public Hotel Hotel { get; set; }
        public Availability Availability { get; set; }
    }

}