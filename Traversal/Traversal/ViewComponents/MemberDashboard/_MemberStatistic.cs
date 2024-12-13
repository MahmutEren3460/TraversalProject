using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.MemberDashboard
{
    public class _MemberStatistic:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}

