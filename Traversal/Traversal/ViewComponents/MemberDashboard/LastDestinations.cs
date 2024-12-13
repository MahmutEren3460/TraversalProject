using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.MemberDashboard
{

    public class LastDestinations : ViewComponent
    {
        private readonly IDestinationService _destinationService;

        public LastDestinations(IDestinationService destinationService)
        {
            _destinationService = destinationService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _destinationService.TGetList().OrderByDescending(x => x.DestinationID).Take(4).ToList();
            return View(values);
        }
    }
}
