using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.AdminDashboard
{
	public class Cards2Statistics:ViewComponent
	{
		public IViewComponentResult Invoke(int id)
		{
			return View();
		}
	}
}
