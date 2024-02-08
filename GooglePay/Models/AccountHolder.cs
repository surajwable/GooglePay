namespace GooglePay.Models
{
    public class AccountHolder
    {
        public int AccountHolderId {  get; set; }
        public string? AccountHolderName { get; set; }
        public byte[]? PasswordHash {  get; set; }
        public byte[]? PasswordSalt { get; set; }
        public decimal Balance { get; set; } = 10000; //by default 10000 Rs.
        public List<Transaction> SentTransactions { get; set; } = new List<Transaction>();
        public List<Transaction> RecievedTransactions { get; set; } = new List<Transaction>();
    }
}
