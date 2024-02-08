namespace GooglePay.DTOs
{
    public class TransactionDto
    {
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public decimal SenderBalance { get; set; }
        public decimal RecieverBalance { get; set; }      
    }
}
