using DataAccessLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.AdminDashboard
{
	public class Cards1Statistic:ViewComponent
	{
		Context c = new Context();
		public IViewComponentResult Invoke(int id)
		{
			ViewBag.v1 = c.Destinations.Count();
			ViewBag.v2 = c.Users.Count();
			return View();
		}
	}
}
