using Lab06_MunozHerrera.Core.Interfaces;
using Lab06_MunozHerrera.Data.Repositories;
using Lab06_MunozHerrera.Models;

namespace Lab06_MunozHerrera.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversidadDbContext _context;
        public IGenericRepository<User> UserRepository { get; private set; }
    
        public UnitOfWork(UniversidadDbContext context)
        {
            _context = context;
            UserRepository = new GenericRepository<User>(_context);
        }
    
        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }
    
        public void Dispose()
        {
            _context.Dispose();
        }
    }
}