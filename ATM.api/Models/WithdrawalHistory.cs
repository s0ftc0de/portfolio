namespace ATM.api.Models
{
    public class WithdrawalHistory
    {
        public int Id { get; set; }

        public int AccountId { get; set; }

        public decimal Amount { get; set; }

        public decimal BalanceAfter { get; set; }

        public DateTime WithdrawnAt { get; set; } = DateTime.UtcNow;

        public Account? Account { get; set; }
    }
}