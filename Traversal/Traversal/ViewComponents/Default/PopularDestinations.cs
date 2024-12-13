using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Default
{
	public class PopularDestinations:ViewComponent
	{
		DestinationManager DestinationManager= new DestinationManager(new EfDestinationDal());
		public IViewComponentResult Invoke()
		{
			var values = DestinationManager.TGetList().Take(8).ToList();
			return View(values);
		}
	}
}
