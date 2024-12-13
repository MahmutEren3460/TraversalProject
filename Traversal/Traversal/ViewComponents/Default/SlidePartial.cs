using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Default
{
	public class SlidePartial:ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
