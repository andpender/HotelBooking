using HotelBooking.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace HotelBooking.Controllers
{
    public class EmailController : Controller
    {
        // GET: Email

        [Authorize(Roles = "Admin,Employee")]
        public ActionResult WriteEmail()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin,Employee")]
        public async Task<ActionResult> WriteEmail([Bind(Include = "ToEmails,Subject,Body,AttachmentFile")] WriteEmailViewModel writeEmailViewModel)
        {
            if (ModelState.IsValid)
            {
                var apiKey = Constants.SendGridKey;
                var client = new SendGridClient(apiKey);
                string base64String = null;

                if (null != writeEmailViewModel.AttachmentFile)
                {
                    var fs = writeEmailViewModel.AttachmentFile.InputStream;
                    var br = new BinaryReader(fs);
                    var bytes = br.ReadBytes((Int32)fs.Length);
                    base64String = Convert.ToBase64String(bytes, 0, bytes.Length);
                }

                foreach (string recipient in writeEmailViewModel.ToEmails.Split(';'))
                {
                    var msg = MailHelper.CreateSingleEmail(new EmailAddress(Constants.FromEmail), new EmailAddress(recipient), writeEmailViewModel.Subject, writeEmailViewModel.Body, null);
                    if (!String.IsNullOrEmpty(base64String))
                    {
                        msg.AddAttachment(writeEmailViewModel.AttachmentFile.FileName, base64String);
                    }
                    var response = await client.SendEmailAsync(msg);
                }
            }

            return View();
        }
    }
}