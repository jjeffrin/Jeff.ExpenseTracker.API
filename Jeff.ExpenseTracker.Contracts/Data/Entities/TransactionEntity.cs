using Jeff.ExpenseTracker.Contracts.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jeff.ExpenseTracker.Contracts.Data.Entities
{
    [Table(Constants.TransactionTable)]
    public class TransactionEntity : EntityBase
    {
        public string Content { get; set; }
        public decimal Cost { get; set; }
        public TransactionType Type { get; set; }
        [Column(TypeName = "datetime2(0)")]
        public DateTime TransactionOn { get; set; }
    }
}
