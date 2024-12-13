using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Default
{
    public class Feature2:ViewComponent
    {
        Feature2Manager feature2 = new Feature2Manager(new EfFeature2Dal());
        public IViewComponentResult Invoke()
        {
            var values = feature2.TGetList();
            return View(values);
        }
    }
}
