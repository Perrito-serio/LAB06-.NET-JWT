using System.Collections;
using Lab06_MunozHerrera.Core.Interfaces;
using Lab06_MunozHerrera.Data.Repositories;

namespace Lab06_MunozHerrera.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly UniversidadDbContext _context;
        // Usaremos un Hashtable para guardar una instancia de cada repositorio que se pida.
        private Hashtable _repositories;

        public UnitOfWork(UniversidadDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (_repositories == null)
            {
                _repositories = new Hashtable();
            }

            var type = typeof(T).Name;

            if (!_repositories.ContainsKey(type))
            {
                var repositoryType = typeof(GenericRepository<>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(T)), _context);
                _repositories.Add(type, repositoryInstance);
            }

            return (IGenericRepository<T>)_repositories[type];
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