using BusinessLayer.Abstract.AbstractUow;
using DTOLayer.DTOs.AccountDTOs;
using EntityLayer.Concrete;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Traversal.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var receivers = _accountService.GetAllReceivers().Select(p => new SelectListItem
            {
                Value = p.ID.ToString(),
                Text = p.Name
            }).ToList();
            ViewBag.Receivers = receivers;
            return View();
        }
        [HttpPost]
        public IActionResult Index(AccountDTO account)
        {
            var valuesSender = _accountService.TGetByID(account.SenderID);
            var valuesReciever = _accountService.TGetByID(account.ReceiverID);

            valuesSender.Balance -= account.Amount;
            valuesReciever.Balance += account.Amount;

            List<Account> modifiedAccounts = new List<Account>()
            {
                valuesSender,
                valuesReciever
            };

            _accountService.TMultiUpdate(modifiedAccounts);
            TempData["Message"] = "Transfer işlemi başarıyla gerçekleştirildi.";

            return RedirectToAction("Index");
        }
    }
}
