using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.AdminDashboard
{
	public class AdminGuideList:ViewComponent
	{
		public IViewComponentResult Invoke(int id)
		{
			return View();
		}
	}
}
