using System.Linq.Expressions;

namespace Lab06_MunozHerrera.Core.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task AddAsync(T entity);
        void Update(T entity);
        void Remove(T entity);
        Task<T> FindAsync(Expression<Func<T, bool>> predicate);
    }
}