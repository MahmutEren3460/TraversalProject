using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.MemberLayout
{
    public class _MemberLayoutSearch:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
