namespace ATM.web.Models
{
    public class WithdrawalHistoryViewModel
    {
        public int Id { get; set; }

        public decimal Amount { get; set; }

        public decimal BalanceAfter { get; set; }

        public DateTime WithdrawnAt { get; set; }
    }
}