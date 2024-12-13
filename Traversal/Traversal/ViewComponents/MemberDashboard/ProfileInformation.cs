using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.MemberDashboard
{
    public class ProfileInformation:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

        public ProfileInformation(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task <IViewComponentResult> InvokeAsync()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.MemberName = values.Name + " " + values.Surname;
            ViewBag.MemberPhone=values.PhoneNumber; 
            ViewBag.MemberEmail = values.Email;
            return View();
        }
    }
}
