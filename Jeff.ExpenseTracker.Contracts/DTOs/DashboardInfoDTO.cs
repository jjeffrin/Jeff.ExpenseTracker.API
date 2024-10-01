namespace Jeff.ExpenseTracker.Contracts.DTOs
{
    public class DashboardInfoDTO
    {
        //public string CurrentMonth { get; set; }
        //public IEnumerable<DateTime> AvailableSalaryMonths { get; set; }
        //public bool IsCurrentMonthDataAvailable { get; set; }
        public decimal MonthlySalary { get; set; }
        public decimal UsableMonthlySalary { get; set; }
        public decimal RemainingUsableMonthlySalary { get; set; }
        public IEnumerable<TransactionDTO> RecentTransactions { get; set; }
        public IEnumerable<TransactionDTO> TopTransactions { get; set; }
        public IEnumerable<TopTransactionCategoryDTO> TopTransactionCategories { get; set; }

    }
}
