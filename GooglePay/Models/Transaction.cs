namespace GooglePay.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public AccountHolder? Sender { get; set; }
        public int SenderId { get; set; }
        public AccountHolder? Receiver { get; set; }
        public int ReceiverId { get; set; } // Corrected property name
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
    }
}
