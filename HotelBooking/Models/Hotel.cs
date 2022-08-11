using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class Hotel
    {
        public int Id { get; set; }

        [Required]
        [MinLength(5)]
        public string Title { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Required]
        public string Address { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        public virtual ICollection<Room> Rooms { get; set; }

        public Hotel()
        {
            CreatedDate = DateTime.Now.ToUniversalTime();
        }
    }
}