using DocumentFormat.OpenXml.Spreadsheet;
using DTOLayer.DTOs.PasswordDTOs;
using EntityLayer.Concrete;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using Traversal.Models;

namespace Traversal.Controllers
{
    [AllowAnonymous]
    public class PasswordChangeController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public PasswordChangeController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ForgetPassword(ForgetPasswordDTO dTO)
        {
            if (!ModelState.IsValid)
            {
                return View(dTO);
            }

            var user = await _userManager.FindByEmailAsync(dTO.Mail);

            if (user == null)
            {
                ModelState.AddModelError("", "Kullanıcı bulunamadı.");
                return View(dTO);
            }

            string passwordResetToken = await _userManager.GeneratePasswordResetTokenAsync(user);
            var passwordResetTokenLink = Url.Action("ResetPassword", "PasswordChange", new
            {
                userId = user.Id,
                token = passwordResetToken
            }, HttpContext.Request.Scheme);

            MimeMessage mimeMessage = new MimeMessage();
            MailboxAddress mailboxFrom = new MailboxAddress("Admin", "denemet054@gmail.com");
            mimeMessage.From.Add(mailboxFrom);
            MailboxAddress mailboxTo = new MailboxAddress("User", dTO.Mail);
            mimeMessage.To.Add(mailboxTo);
            var bodyBuilder = new BodyBuilder
            {
                TextBody = passwordResetTokenLink
            };
            mimeMessage.Body = bodyBuilder.ToMessageBody();
            mimeMessage.Subject = "Şifre Değiştirme Talebi";

            using (SmtpClient client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                client.Authenticate("denemet054@gmail.com", "yfah evmu mjlp oybo ");
                client.Send(mimeMessage);
                client.Disconnect(true);
            }

            ViewBag.Message = "Şifre sıfırlama linki gönderildi.";
            return View();
        }

        [HttpGet]
        public IActionResult ResetPassword(string userid, string token)
        {
            TempData["userid"] = userid;
            TempData["token"] = token;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordDTO resetPasswordViewModel)
        {
            var userid = TempData["userid"];
            var token = TempData["token"];
            if (userid == null || token == null)
            {
                ModelState.AddModelError(string.Empty, "Token geçerliliğini yitirdi veya kullanıcı kimliği geçersiz. Lütfen yeniden deneyin.");
                return View(resetPasswordViewModel);
            }

            var user = await _userManager.FindByIdAsync(userid.ToString());
            var result = await _userManager.ResetPasswordAsync(user, token.ToString(), resetPasswordViewModel.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login");
            }
            return View();
        }


    }
}
