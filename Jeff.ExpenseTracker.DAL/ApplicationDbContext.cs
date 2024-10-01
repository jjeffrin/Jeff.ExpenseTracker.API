using Jeff.ExpenseTracker.Contracts.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Jeff.ExpenseTracker.DAL
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<RecurringTransactionEntity> RecurringTransactions { get; set; }
    }
}
