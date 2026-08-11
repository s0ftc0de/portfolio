using ATM.web.Models;
using ATM.web.Services;
using Microsoft.AspNetCore.Mvc;

namespace ATM.web.Controllers
{
    public class ATMController : Controller
    {
        private readonly AccountApiService _accountApiService;

        public ATMController(AccountApiService accountApiService)
        {
            _accountApiService = accountApiService;
        }

        public async Task<IActionResult> Index()
        {
            var accounts = await _accountApiService.GetAccountsAsync();

            return View(accounts);
        }

        [HttpGet]
        public IActionResult Withdraw()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Withdraw(WithdrawViewModel model)
        {
            var result = await _accountApiService.WithdrawAsync(model);

            if (result != null)
            {
                TempData["Success"] =
                    $"You withdrew {result.Amount:N0} MMK from {result.HolderName}'s account.";

                return RedirectToAction("Index");
            }

            TempData["Error"] = "Withdrawal failed.";

            return View(model);
        }

        [HttpPost]
        [ActionName("History")]
        public IActionResult HistoryPost(string accountNumber)
        {
            return RedirectToAction("History", new
            {
                accountNumber = accountNumber
            });
        }

        [HttpGet]
        public async Task<IActionResult> History(string accountNumber)
        {
            if (string.IsNullOrWhiteSpace(accountNumber))
            {
                return View();
            }

            var history = await _accountApiService
                .GetHistoryAsync(accountNumber);

            return View(history);
        }
    }
}