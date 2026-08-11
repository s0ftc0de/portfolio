using ATM.api.Models;
using Microsoft.EntityFrameworkCore;

namespace ATM.api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(
            DbContextOptions<AppDbContext>options
            ): base(options)
        {

        }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<WithdrawalHistory> WithdrawalHistories { get; set; }
    }
}
