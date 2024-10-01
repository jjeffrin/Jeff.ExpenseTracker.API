using Jeff.ExpenseTracker.Contracts.Data.Entities;
using Jeff.ExpenseTracker.Contracts.Data.Repositories;
using Jeff.ExpenseTracker.DAL;

namespace Jeff.ExpenseTracker.Infrastructure.Repositories
{
    public class RecurringTransactionRepository : Repository<RecurringTransactionEntity>, IRecurringTransactionRepository
    {
        public RecurringTransactionRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
