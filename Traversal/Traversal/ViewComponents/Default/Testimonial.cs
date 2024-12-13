using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Default
{
	public class Testimonial:ViewComponent
	{
		TestimonialManager manager=new TestimonialManager(new EfTestimonialDal());
		public IViewComponentResult Invoke()
		{
			var values = manager.TGetList();
			return View(values);
		}
	}
}
