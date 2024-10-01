using Jeff.ExpenseTracker.Contracts.Enums;

namespace Jeff.ExpenseTracker.Contracts.DTOs
{
    public class CreateTransactionDTO : UserSpecificDTO
    {
        public TransactionType Type { get; set; }
        public string Content { get; set; }
        public decimal Cost { get; set; }
        public DateTime TransactionOn { get; set; }
    }
}
