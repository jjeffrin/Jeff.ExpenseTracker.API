using Jeff.ExpenseTracker.Contracts.Data.Repositories;

namespace Jeff.ExpenseTracker.Contracts
{
    public interface IUnitOfWork
    {
        ITransactionRepository TransactionRepository { get; }
        IRecurringTransactionRepository RecurringTransactionRepository { get; }
        Task<int> CommitChangesAsync();
    }
}
