namespace ATM.web.Models
{
    public class AccountViewModel
    {
        public int Id { get; set; }
        public string AccountNumber { get; set; }
        public string HolderName { get; set; }
        public decimal Balance { get; set; }
        public bool IsActive { get; set; }
    }
}
