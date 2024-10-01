using Jeff.ExpenseTracker.Contracts;
using Jeff.ExpenseTracker.Contracts.Data.Repositories;
using Jeff.ExpenseTracker.DAL;
using Jeff.ExpenseTracker.Infrastructure.Repositories;

namespace Jeff.ExpenseTracker.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext context;

        public UnitOfWork(ApplicationDbContext context)
        {
            this.context = context;
        }

        public ITransactionRepository TransactionRepository => new TransactionRepository(context);
        public IRecurringTransactionRepository RecurringTransactionRepository => new RecurringTransactionRepository(context);

        public async Task<int> CommitChangesAsync()
        {
            return await this.context.SaveChangesAsync();
        }
    }
}
