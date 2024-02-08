namespace GooglePay.DTOs
{
    public class CreditDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal Amount { get; set; }
    }
}
