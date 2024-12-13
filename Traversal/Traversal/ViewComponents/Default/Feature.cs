using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.ViewComponents.Default
{
	public class Feature:ViewComponent
	{
		FeatureManager feature = new FeatureManager(new EfFeatureDal());
		public IViewComponentResult Invoke()
		{
			var values =feature.TGetList();
			return View(values);
		}
	}
}
