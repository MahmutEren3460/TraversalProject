using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.AdminDashboard
{
	public class DestinationStatistic2:ViewComponent
	{
		public IViewComponentResult Invoke(int id)
		{
			return View();
		}
	}
}
