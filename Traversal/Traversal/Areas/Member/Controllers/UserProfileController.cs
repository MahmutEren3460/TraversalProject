using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Traversal.Areas.Member.Models;

namespace Traversal.Areas.Member.Controllers
{
    [Area("Member")]
    public class UserProfileController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public UserProfileController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        [HttpGet]
        public async Task <IActionResult> Index()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            UserEditViewModel user = new UserEditViewModel();
            user.name = values.Name;
            user.Surname = values.Surname;
            user.phonenumber = values.PhoneNumber;
            user.mail = values.Email;
            return View(user);
        }
        [HttpPost]
        public async Task<IActionResult> Index(UserEditViewModel p)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (p.Image != null)
            {
                var resource=Directory.GetCurrentDirectory();
                var extension = Path.GetExtension(p.Image.FileName);
                var imagename = Guid.NewGuid() + extension;
                var savelocation = resource + "/wwwroot/UserImage/" + imagename;
                var stream = new FileStream(savelocation, FileMode.Create);
                await p.Image.CopyToAsync(stream);
                user.ImageURL=imagename;
            }
            user.Name = p.name;
            user.Surname=p.Surname;
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, p.password);
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("SignIn", "Login", new { area = "" });

            }
            return View();
        }
    }
}
