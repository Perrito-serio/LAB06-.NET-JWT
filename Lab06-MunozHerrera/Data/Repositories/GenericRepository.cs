using System.Linq.Expressions;
using Lab06_MunozHerrera.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lab06_MunozHerrera.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly UniversidadDbContext _context;
        private readonly DbSet<T> _dbSet;
    
        public GenericRepository(UniversidadDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }
    
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }
    
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }
    
        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }
    
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }
    
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }
    
        public async Task<T> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.FirstOrDefaultAsync(predicate);
        }
    }
}