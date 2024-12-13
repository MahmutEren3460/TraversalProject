using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Destination
{
    public class GuideDeatils:ViewComponent
    {
        private readonly IGuideService _guideService;

        public GuideDeatils(IGuideService guideService)
        {
            _guideService = guideService;
        }

        public IViewComponentResult Invoke()
        {
            var values = _guideService.TGetById(1);
            return View(values);
        }
    }
}
