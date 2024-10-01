using Jeff.ExpenseTracker.Contracts.Data.Entities;
using Jeff.ExpenseTracker.Contracts.Data.Repositories;
using Jeff.ExpenseTracker.DAL;

namespace Jeff.ExpenseTracker.Infrastructure.Repositories
{
    public class TransactionRepository : Repository<TransactionEntity>, ITransactionRepository
    {
        public TransactionRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
