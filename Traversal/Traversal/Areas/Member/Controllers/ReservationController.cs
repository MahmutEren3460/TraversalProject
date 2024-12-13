using BusinessLayer.Abstract;
using BusinessLayer.Concrete;
using DataAccessLayer.EntityFramework;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace Traversal.Areas.Member.Controllers
{
    [Area("Member")]
    public class ReservationController : Controller
    {
        //DestinationManager manager=new DestinationManager(new EfDestinationDal());

        //ReservationManager reservation = new ReservationManager(new EfReservationDal());

        private readonly IDestinationService _manager;
        private readonly IReservationService _reservation;
        private readonly UserManager<AppUser> _userManager;

        public ReservationController(IDestinationService manager, IReservationService reservation, UserManager<AppUser> userManager)
        {
            _manager = manager;
            _reservation = reservation;
            _userManager = userManager;
        }

        public async Task <IActionResult> MyCurrentReservation()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valuesList = _reservation.GetListWithReservationByAccepted(values.Id);
            return View(valuesList);
        }
        public async Task <IActionResult> MyOldReservation()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valuesList = _reservation.GetListWithReservationByPrevious(values.Id);
            return View(valuesList);
        }
        public async Task <IActionResult> MyApprovalReservation()
        {
            var values = await _userManager.FindByNameAsync(User.Identity.Name);
            var valuesList = _reservation.GetListWithReservationByWaitApproval(values.Id); 
            return View(valuesList);
        }

        [HttpGet]
        public IActionResult NewReservation()
        {
            List<SelectListItem> values=(from x in _manager.TGetList() select new SelectListItem { Text=x.city,Value=x.DestinationID.ToString() }).ToList();
            ViewBag.v=values;
            return View();
        }
		[HttpPost]
		public IActionResult NewReservation(Reservation p)
		{
			// Oturum açmış kullanıcının kimliğini al
			var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

			if (userId != null)
			{
				// Kullanıcı ID'sini int'e çevir ve rezervasyona ata
				p.AppUserID = int.Parse(userId);
				p.Status = "Onay Bekliyor";

				// Rezervasyonu ekle
				_reservation.TAdd(p);

                // Başarılı ekleme sonrası yönlendirme
                return RedirectToAction("MyApprovalReservation");
            }

			// Eğer kullanıcı kimliği alınamazsa, model state'e hata ekle
			ModelState.AddModelError("", "Kullanıcı bilgileri alınamadı.");
			return View(p);
		}

	}
}

