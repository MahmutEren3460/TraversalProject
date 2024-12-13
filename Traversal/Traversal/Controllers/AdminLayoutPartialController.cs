using Microsoft.AspNetCore.Mvc;

namespace Traversal.Controllers
{
    public class AdminLayoutPartialController : Controller
    {
        public PartialViewResult AdminHeaderPartial()
        {
            return PartialView();
        }
        public PartialViewResult AdminLayoutNavbar1()
        {
            return PartialView();
        }
        public PartialViewResult AdminLayoutNavbar2()
        {
            return PartialView();
        }
        public PartialViewResult AdminLayoutFooter()
        {
            return PartialView();
        }
        public PartialViewResult AdminLayoutScript() 
        {
            return PartialView(); 
        }
    }
}
