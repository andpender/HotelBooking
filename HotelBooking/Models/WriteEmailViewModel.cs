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
        [Display(Name = "To Emails")]
        public string ToEmails { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        [Display(Name = "Email Attachment")]
        public string EmailAttachmentPath { get; set; }
        public HttpPostedFileBase AttachmentFile { get; set; }
    }
}