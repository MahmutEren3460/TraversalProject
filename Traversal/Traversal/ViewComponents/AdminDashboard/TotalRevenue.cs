using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.AdminDashboard
{
	public class TotalRevenue:ViewComponent
	{
		public IViewComponentResult Invoke(int id)
		{
			return View();
		}
	}
}
