using BusinessLayer.Concrete;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using Traversal.Models;

namespace Traversal.ViewComponents.MemberLayout
{
    public class _MemberLayoutNavbar:ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;

		public _MemberLayoutNavbar(UserManager<AppUser> userManager)
		{
			_userManager = userManager;
		}

		public async Task <IViewComponentResult> InvokeAsync()
        {
			var user = await _userManager.FindByNameAsync(User.Identity.Name);
			ViewBag.userName = user.Name + " " + user.Surname;
			ViewBag.userImage = user.ImageURL;
			return View();
        }
    }
}
