namespace ATM.web.Models
{
    public class WithdrawResponseViewModel
    {
        public string Message { get; set; } = string.Empty;

        public string HolderName { get; set; } = string.Empty;

        public string AccountNumber { get; set; } = string.Empty;

        public decimal Amount { get; set; }

        public decimal Balance { get; set; }
    }
}