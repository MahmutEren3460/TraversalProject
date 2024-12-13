using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Traversal.Models;
using System.Linq;

namespace Traversal.Controllers
{
    [AllowAnonymous]
    public class LoginController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LoginController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // SignUp GET: Kullanıcı Kayıt Formu
        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        // SignUp POST: Kullanıcı Kayıt İşlemi
        [HttpPost]
        public async Task<IActionResult> SignUp(UserRegisterViewModel p)
        {
            if (ModelState.IsValid)
            {
                // Yeni kullanıcıyı oluşturuyoruz
                AppUser appUser = new AppUser()
                {
                    Name = p.Name,
                    Surname = p.Surname,
                    UserName = p.Username,
                    Email = p.Mail
                };

                // Şifre ve onay şifresi eşleşiyorsa
                if (p.Password == p.ConfirmPassword)
                {
                    var result = await _userManager.CreateAsync(appUser, p.Password);

                    if (result.Succeeded)
                    {
                        // Kullanıcıyı oluşturduktan sonra başarılı ise login sayfasına yönlendiriyoruz
                        return RedirectToAction("SignIn", "Login");
                    }
                    else
                    {
                        foreach (var item in result.Errors)
                        {
                            ModelState.AddModelError("", item.Description); // Hata mesajlarını gösteriyoruz
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Şifreler uyuşmuyor.");
                }
            }
            return View(p);
        }

        // SignIn GET: Kullanıcı Giriş Formu
        [HttpGet]
        public IActionResult SignIn()
        {
            return View();
        }

        // SignIn POST: Kullanıcı Giriş İşlemi
        [HttpPost]
        public async Task<IActionResult> SignIn(UserSignInViewModel p)
        {
            if (ModelState.IsValid)
            {
                // Kullanıcıyı şifre ile giriş yapmaya çalışıyoruz
                var result = await _signInManager.PasswordSignInAsync(p.username, p.password, false, true);

                if (result.Succeeded)
                {
                    // Giriş başarılıysa, kullanıcının rollerini alıyoruz
                    var user = await _userManager.FindByNameAsync(p.username);
                    var roles = await _userManager.GetRolesAsync(user);

                    // Admin rolü varsa, Admin area içindeki Dashboard'a yönlendir
                    if (roles.Contains("Admin"))
                    {
                        return RedirectToAction("Index", "AdminDashboard", new { Area = "Admin" });
                    }
                    // Member rolü varsa, Member area içindeki Profile sayfasına yönlendir
                    else if (roles.Contains("Member"))
                    {
                        return RedirectToAction("Index", "UserProfile", new { Area = "Member" });
                    }
                    else
                    {
                        ModelState.AddModelError("", "Geçersiz rol.");
                    }
                }
                else
                {
                    // Giriş başarısızsa hata mesajı ekliyoruz
                    ModelState.AddModelError("", "Geçersiz giriş denemesi.");
                }
            }
            return View(p);
        }

        // Kullanıcı Çıkış Yapma (Logout)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("SignIn", "Login");
        }
    }
}
