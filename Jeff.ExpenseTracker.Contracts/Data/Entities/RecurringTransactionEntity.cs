using Jeff.ExpenseTracker.Contracts.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace Jeff.ExpenseTracker.Contracts.Data.Entities
{
    [Table(Constants.RecurringTransactionTable)]
    public class RecurringTransactionEntity : EntityBase
    {
        public string Content { get; set; }
        public decimal Cost { get; set; }
        public RecurringTransactionType Type { get; set; }
        public FrequencyType Frequency { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
    }
}
