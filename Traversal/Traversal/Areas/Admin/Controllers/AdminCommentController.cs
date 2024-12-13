using BusinessLayer.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class AdminCommentController : Controller
	{
		private readonly ICommentService _commentService;

		public AdminCommentController(ICommentService commentService)
		{
			_commentService = commentService;
		}

		public IActionResult Index()
		{
			var values= _commentService.TGetListCommentWithDestination();
			return View(values);
		}
		public IActionResult DeleteComment(int id)
		{
			var values = _commentService.TGetById(id);
			_commentService.TDelete(values);
			return RedirectToAction("Index");
		}
	}
}
