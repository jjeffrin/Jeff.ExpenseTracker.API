namespace Jeff.ExpenseTracker.Contracts.Data.Repositories
{
    public interface IRepository<T>
    {
        IQueryable<T> GetQueryable();
        IEnumerable<T> GetAll();
        Task<T?> GetById(int id);
        IEnumerable<T> GetByEmailId(string emailId);
        Task<int> Add(T entity, string emailId);
        int Delete(T entity);
        void UpdateBaseEntity(T entity, string emailId, bool isNew = false);
    }
}
