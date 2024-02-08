using GooglePay.Models;

namespace GooglePay.DTOs
{
    public class MemberDto
    {
        public int AccountHolderId { get; set; }
        public string? AccountHolderName { get; set; }
        public decimal Balance { get; set; } = 10000; //by default 10000 Rs.
        public List<Transaction> SentTransactions { get; set; } = new List<Transaction>();
        public List<Transaction> RecievedTransactions { get; set; } = new List<Transaction>();
    }
}
