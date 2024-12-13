using BusinessLayer.Abstract;
using BusinessLayer.ValidationRules;
using EntityLayer.Concrete;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class GuideController : Controller
	{
		private readonly IGuideService _guideService;

		public GuideController(IGuideService guideService)
		{
			_guideService = guideService;
		}

		public IActionResult Index()
		{
			var values = _guideService.TGetList();
			return View(values);
		}
        public IActionResult Aktif(int id)
        {
           _guideService.TChangeToTrue(id);

            return RedirectToAction("Index");
        }

        public IActionResult Pasif(int id)
        {
            _guideService.TChangeToFalse(id);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult AddGuide()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddGuide(Guide p, IFormFile Image)
        {
            GuideValidator validationRules = new GuideValidator();
            ValidationResult result = validationRules.Validate(p);
            if (result.IsValid)
            {
                p.Status = true;
                if (Image != null)
                {
                    var imageExtension = Path.GetExtension(Image.FileName);
                    var newImageName = Guid.NewGuid() + imageExtension;
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImage/", newImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    p.Image = "/UserImage/" + newImageName;
                }
                _guideService.TAdd(p);
                return RedirectToAction("Index");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError(item.PropertyName, item.ErrorMessage);
                }
            }
            return View();

        }
        [HttpGet]
        public IActionResult EditGuide(int id)
        {
            var values=_guideService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult EditGuide(Guide p, IFormFile Image)
        {
            if (Image != null)
            {
                var imageExtension = Path.GetExtension(Image.FileName);
                var newImageName = Guid.NewGuid() + imageExtension;
                var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserImage/", newImageName);
                using (var stream = new FileStream(imagePath, FileMode.Create))
                {
                    Image.CopyTo(stream);
                }
                p.Image = "/UserImage/" + newImageName;
            }
            _guideService.TUpdate(p);
            return RedirectToAction("Index");
        }
    }
}
