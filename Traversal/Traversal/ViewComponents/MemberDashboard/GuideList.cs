using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.MemberDashboard
{
    public class GuideList:ViewComponent
    {
        GuideManager guideManager=new GuideManager(new EFGuideDal());

        public IViewComponentResult Invoke()
        {
            var values=guideManager.TGetList().OrderByDescending(x=>x.GuideID).Take(5).ToList();
            return View(values);
        }
    }
}
