using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class ContactUsController : Controller
	{
		private readonly IContactUsService _contactUsService;

		public ContactUsController(IContactUsService contactUsService)
		{
			_contactUsService = contactUsService;
		}

		public IActionResult Index()
		{
			var values = _contactUsService.TGetList();
			return View(values);
		}
        public IActionResult Details(int id)
        {
            var message = _contactUsService.TGetById(id);
            return View(message);
        }
        public IActionResult Delete(int id)
        {
            var message = _contactUsService.TGetById(id);
            _contactUsService.TDelete(message);
            return RedirectToAction("Index");
        }
    }
}
