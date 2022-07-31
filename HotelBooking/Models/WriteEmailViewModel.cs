using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace HotelBooking.Models
{
    public class WriteEmailViewModel
    {
        [Required]
        [Display(Name = "To Emails")]
        public string ToEmails { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Body { get; set; }

        [Display(Name = "Email Attachment")]
        public string EmailAttachmentPath { get; set; }

        public HttpPostedFileBase AttachmentFile { get; set; }
    }
}