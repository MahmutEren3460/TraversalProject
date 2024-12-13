using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminDestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public AdminDestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IActionResult Index()
        {
            var values = _destinationService.TGetList();
            return View(values);
        }

        [HttpGet]
        public IActionResult AddDestination()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddDestination(Destination p, IFormFile Image, IFormFile CoverImage, IFormFile Image2)
        {
            if (ModelState.IsValid)
            {
                if (Image != null)
                {
                    var imageExtension = Path.GetExtension(Image.FileName);
                    var newImageName = Guid.NewGuid() + imageExtension;
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    p.Image = "/images/" + newImageName;
                }

                if (CoverImage != null)
                {
                    var coverImageExtension = Path.GetExtension(CoverImage.FileName);
                    var newCoverImageName = Guid.NewGuid() + coverImageExtension;
                    var coverImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newCoverImageName);
                    using (var stream = new FileStream(coverImagePath, FileMode.Create))
                    {
                        CoverImage.CopyTo(stream);
                    }
                    p.CoverImage = "/images/" + newCoverImageName;
                }

                if (Image2 != null)
                {
                    var image2Extension = Path.GetExtension(Image2.FileName);
                    var newImage2Name = Guid.NewGuid() + image2Extension;
                    var image2Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newImage2Name);
                    using (var stream = new FileStream(image2Path, FileMode.Create))
                    {
                        Image2.CopyTo(stream);
                    }
                    p.Image2 = "/images/" + newImage2Name;
                }

                
                _destinationService.TAdd(p);

                return RedirectToAction("Index");
            }
            return View(p);
        }
        public IActionResult DeleteDestination(int id)
        {
            var values = _destinationService.TGetById(id);
            _destinationService.TDelete(values);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult UpdateDestination(int id)
        {
            var values = _destinationService.TGetById(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateDestination(Destination destination, IFormFile Image, IFormFile CoverImage, IFormFile Image2)
        {
            if (ModelState.IsValid)
            {
                
                if (Image != null)
                {
                    var imageExtension = Path.GetExtension(Image.FileName);
                    var newImageName = Guid.NewGuid() + imageExtension;
                    var imagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newImageName);
                    using (var stream = new FileStream(imagePath, FileMode.Create))
                    {
                        Image.CopyTo(stream);
                    }
                    destination.Image = "/images/" + newImageName;
                }

                if (CoverImage != null)
                {
                    var coverImageExtension = Path.GetExtension(CoverImage.FileName);
                    var newCoverImageName = Guid.NewGuid() + coverImageExtension;
                    var coverImagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newCoverImageName);
                    using (var stream = new FileStream(coverImagePath, FileMode.Create))
                    {
                        CoverImage.CopyTo(stream);
                    }
                    destination.CoverImage = "/images/" + newCoverImageName;
                }

                if (Image2 != null)
                {
                    var image2Extension = Path.GetExtension(Image2.FileName);
                    var newImage2Name = Guid.NewGuid() + image2Extension;
                    var image2Path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images/", newImage2Name);
                    using (var stream = new FileStream(image2Path, FileMode.Create))
                    {
                        Image2.CopyTo(stream);
                    }
                    destination.Image2 = "/images/" + newImage2Name;
                }

                
                _destinationService.TUpdate(destination);

                return RedirectToAction("Index");
            }

            return View(destination);
        }

    }
}
