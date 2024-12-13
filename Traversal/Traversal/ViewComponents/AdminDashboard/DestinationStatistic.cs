using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.AdminDashboard
{
	public class DestinationStatistic:ViewComponent
	{
		public IViewComponentResult Invoke(int id)
		{
			return View();
		}
	}
}
