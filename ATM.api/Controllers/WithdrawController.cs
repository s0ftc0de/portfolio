using ATM.api.Data;
using ATM.api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ATM.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class WithdrawController : ControllerBase
    {
        private readonly AppDbContext _context;

        public WithdrawController(AppDbContext context)
        {
            _context = context;
        }

        // POST: api/withdraw
        [HttpPost]
        public async Task<IActionResult> Withdraw(
            [FromBody] WithdrawRequest request)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(x =>
                    x.AccountNumber == request.AccountNumber);

            if (account == null)
            {
                return NotFound(new
                {
                    message = "Account not found."
                });
            }

            if (!account.IsActive)
            {
                return BadRequest(new
                {
                    message = "Account is inactive."
                });
            }

            if (request.Amount <= 0)
            {
                return BadRequest(new
                {
                    message = "Amount must be greater than zero."
                });
            }

            if (account.Balance < request.Amount)
            {
                return BadRequest(new
                {
                    message = "Insufficient balance."
                });
            }

            // Reduce account balance
            account.Balance -= request.Amount;

            // Save withdrawal history
            var history = new WithdrawalHistory
            {
                AccountId = account.Id,
                Amount = request.Amount,
                BalanceAfter = account.Balance
            };

            _context.WithdrawalHistories.Add(history);

            await _context.SaveChangesAsync();

            return Ok(new
            {
                message = "Withdrawal successful.",
                holderName = account.HolderName,
                accountNumber = account.AccountNumber,
                amount = request.Amount,
                balance = account.Balance
            });
        }

        // GET: 
        [HttpGet("history/{accountNumber}")]
        public async Task<IActionResult> History(string accountNumber)
        {
            var history = await _context.WithdrawalHistories
                .Where(x =>
                    x.Account!.AccountNumber == accountNumber)
                .OrderByDescending(x => x.WithdrawnAt)
                .Select(x => new
                {
                    x.Id,
                    x.Amount,
                    x.BalanceAfter,
                    x.WithdrawnAt
                })
                .ToListAsync();

            return Ok(history);
        }
    }

    public class WithdrawRequest
    {
        public string AccountNumber { get; set; } = string.Empty;

        public decimal Amount { get; set; }
    }
}