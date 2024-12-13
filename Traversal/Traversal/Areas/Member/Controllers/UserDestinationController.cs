

using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Areas.Member.Controllers
{
    [AllowAnonymous]
    [Area("Member")]
    public class UserDestinationController : Controller
    {
        private readonly IDestinationService _destinationService;

        public UserDestinationController(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IActionResult Index(string searchString)
        {
            ViewData["CurrentFilter"] = searchString;

            var values = _destinationService.TGetList();

            if (!string.IsNullOrEmpty(searchString))
            {
                // Şehir adında arama yapılacak
                values = values.Where(x => x.city.Contains(searchString)).ToList();
            }

            // Verileri sıralayarak geri döndür
            values = values.OrderByDescending(x => x.DestinationID).ToList();

            return View(values);
        }

    }
}
