using Jeff.ExpenseTracker.Contracts.Enums;

namespace Jeff.ExpenseTracker.Contracts.DTOs
{
    public class CreateRecurringTransactionDTO : UserSpecificDTO
    {
        public string Content { get; set; }
        public decimal Cost { get; set; }
        public RecurringTransactionType Type { get; set; }
        public FrequencyType Frequency { get; set; }
        public DateTime EffectiveFrom { get; set; }
        public DateTime EffectiveTo { get; set; }
    }
}
