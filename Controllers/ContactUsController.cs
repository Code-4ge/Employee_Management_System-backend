using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using EMS_Web_API.ModelDTO;
using MimeKit.Text;

namespace EMS_Web_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactUsController : ControllerBase
    {
        [HttpPost]
        public IActionResult SendEmail([FromBody] EmailServiceDTO mail)
        {
            var email = new MimeMessage();

            email.From.Add(MailboxAddress.Parse("ranusinghrajput20@gmail.com")); 
            email.To.Add(MailboxAddress.Parse("ranusinghrajput75@gmail.com")); 
            email.Subject = "EMS Query by " + mail.Name; 
            email.Body = new TextPart(TextFormat.Html) { Text = "UserName: " + mail.Email + "\nContact: " + mail.Phone + "\n" + mail.Body};

            using var smtp = new SmtpClient(); 
            smtp.Connect("smtp.gmail.com", 465, true); 
            smtp.Authenticate("ranusinghrajput20@gmail.com", "mibpigtfxpcmebss"); 
            smtp.Send(email);
            smtp.Disconnect(true);


            return Ok("Mail Sent Sucessfully");
        }

    }
}
