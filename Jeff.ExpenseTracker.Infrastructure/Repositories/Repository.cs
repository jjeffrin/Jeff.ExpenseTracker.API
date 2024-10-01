using Jeff.ExpenseTracker.Contracts.Data.Entities;
using Jeff.ExpenseTracker.Contracts.Data.Repositories;
using Jeff.ExpenseTracker.DAL;
using Microsoft.EntityFrameworkCore;

namespace Jeff.ExpenseTracker.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : EntityBase
    {
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            this._dbSet = context.Set<T>();
        }

        public IQueryable<T> GetQueryable()
        {
            IQueryable<T> query = this._dbSet;
            return query;
        }

        public IEnumerable<T> GetAll()
        {
            return this._dbSet.AsEnumerable();
        }

        public async Task<T?> GetById(int id)
        {
            var entity = await this._dbSet.FindAsync(id);
            return entity;
        }

        public IEnumerable<T> GetByEmailId(string emailId)
        {
            IQueryable<T> query = this._dbSet;
            var records = query.Where(x => x.UpdatedBy == emailId);
            return records;
        }

        public async Task<int> Add(T entity, string emailId)
        {
            this.UpdateBaseEntity(entity, emailId, true);
            var tracker = await this._dbSet.AddAsync(entity);
            return tracker.Entity.Id;
        }

        public int Delete(T entity)
        {
            var tracker = this._dbSet.Remove(entity);
            return tracker.Entity.Id;
        }

        public void UpdateBaseEntity(T entity, string emailId, bool isNew = false)
        {
            if (isNew)
            {
                entity.CreatedOn = entity.UpdatedOn = DateTime.Now;
            }
            entity.UpdatedOn = DateTime.Now;
            entity.UpdatedBy = emailId;
        }
    }
}
