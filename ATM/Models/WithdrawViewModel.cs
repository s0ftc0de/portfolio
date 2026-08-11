namespace ATM.web.Models
{
    public class WithdrawViewModel
    {
        public string AccountNumber { get; set; } = string.Empty;

        public decimal Amount { get; set; }
    }
}