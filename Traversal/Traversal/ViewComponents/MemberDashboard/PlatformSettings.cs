using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.MemberDashboard
{
    public class PlatformSettings:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
