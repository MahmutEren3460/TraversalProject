using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Traversal.Models;
using MimeKit.Text;
using DTOLayer.DTOs.MailDTOs;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MailController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Index(MailRequest mailRequest)
        {
            try
            {
                MimeMessage mimeMessage = new MimeMessage();
                MailboxAddress mailboxFrom = new MailboxAddress("Admin", "denemet054@gmail.com");
                mimeMessage.From.Add(mailboxFrom);
                MailboxAddress mailboxTo = new MailboxAddress("User", mailRequest.ReceiverMail);
                mimeMessage.To.Add(mailboxTo);
                var bodyBuilder = new BodyBuilder
                {
                    TextBody = mailRequest.Body
                };
                mimeMessage.Body = bodyBuilder.ToMessageBody();
                mimeMessage.Subject = mailRequest.Subject;

                using (SmtpClient client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("denemet054@gmail.com", "yfah evmu mjlp oybo ");
                    client.Send(mimeMessage);
                    client.Disconnect(true);
                }
                return View();
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Mail gönderiminde bir hata oluştu: " + ex.Message;
                return View();
            }
        }
    }
}
