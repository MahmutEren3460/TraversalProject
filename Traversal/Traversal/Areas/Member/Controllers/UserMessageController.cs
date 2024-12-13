using Microsoft.AspNetCore.Mvc;

namespace Traversal.Areas.Member.Controllers
{
    public class UserMessageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
