using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.AdminDashboard
{
	public class DashboardBanner:ViewComponent
	{
		
		public IViewComponentResult Invoke(int id)
		{			
			return View();
		}
	}
}
