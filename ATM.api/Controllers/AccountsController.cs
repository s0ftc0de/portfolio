using Microsoft.AspNetCore.Mvc;
using ATM.api.Data;
using Microsoft.EntityFrameworkCore;

namespace ATM.api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountsController : ControllerBase
    {
        private readonly AppDbContext _context;
        public AccountsController (AppDbContext context)
        {
               _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> GetAccounts()
        {
            var accounts = await _context.Accounts.ToListAsync();
            return Ok(accounts);
        }

        [HttpGet("{accountNumber}")]
        public async Task <IActionResult> GetAccount(string accountNumber)
        {
            var account = await _context.Accounts
                .FirstOrDefaultAsync(
                    x => x.AccountNumber == accountNumber
                );
            if (account == null)
            {
                return NotFound(new
                {
                    message = "Account not found"
                });

            }
            return Ok(account);
        }
    }
}
