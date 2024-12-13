using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Default
{
	public class SubAbout:ViewComponent
	{
		SubAboutManager manager=new SubAboutManager(new EfSubAboutDal());
		public IViewComponentResult Invoke()
		{
			var values=manager.TGetList();
			return View(values);
		}
	}
}
