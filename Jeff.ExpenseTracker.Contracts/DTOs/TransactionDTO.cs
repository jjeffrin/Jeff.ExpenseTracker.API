using Jeff.ExpenseTracker.Contracts.Enums;

namespace Jeff.ExpenseTracker.Contracts.DTOs
{
    public class TransactionDTO
    {
        public int Id { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime UpdatedOn { get; set; }
        public string Content { get; set; }
        public decimal Cost { get; set; }
        public TransactionType Type { get; set; }
        public DateTime TransactionOn { get; set; }
    }
}
