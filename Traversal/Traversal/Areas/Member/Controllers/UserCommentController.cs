using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Areas.Member.Controllers
{
     [AllowAnonymous]
     [Area("Member")]
    public class UserCommentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
