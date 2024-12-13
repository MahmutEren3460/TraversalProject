using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Traversal.Controllers
{
    public class CommentController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ICommentService _commentService;
        private readonly IDestinationService _destinationService;

        public CommentController(UserManager<AppUser> userManager, ICommentService commentService, IDestinationService destinationService)
        {
            _userManager = userManager;
            _commentService = commentService;
            _destinationService = destinationService;
        }

        [HttpGet]
        public PartialViewResult AddComment(int destinationID)
        {
            ViewBag.DestinationID = destinationID;
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddComment(Comment p, int destinationID)
        {
            var user = await _userManager.GetUserAsync(User);

            if (user != null)
            {
                p.Date = DateTime.Now;
                p.State = true;
                p.AppUserID = user.Id; // Kullanıcının ID'sini ekle
                p.DestinationID = destinationID; // Yorumun yapıldığı Destination ID'yi ekle

                try
                {
                    _commentService.TAdd(p);
                    return RedirectToAction("Index", "Destination"); // Yönlendirme yap
                }
                catch (Exception ex)
                {
                    // Hata mesajı loglayın veya ekrana gösterin
                    ModelState.AddModelError("", "Yorum eklenirken bir hata oluştu: " + ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError("", "Yorum yapabilmek için giriş yapmanız gerekmektedir.");
            }

            // Hata durumunda tekrar formu görüntüleyin
            ViewBag.DestinationID = destinationID;
            return PartialView("AddComment", p);
        }
    }


}
