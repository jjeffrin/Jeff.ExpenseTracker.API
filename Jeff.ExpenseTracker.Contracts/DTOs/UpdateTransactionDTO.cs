using Jeff.ExpenseTracker.Contracts.Enums;

namespace Jeff.ExpenseTracker.Contracts.DTOs
{
    public class UpdateTransactionDTO : UserSpecificDTO
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public decimal Cost { get; set; }
        public TransactionType Type { get; set; }
        public DateTime TransactionOn { get; set; }
    }
}
