using Lab06_MunozHerrera.Models;

namespace Lab06_MunozHerrera.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> Repository<T>() where T : class;
    
        Task<int> CompleteAsync();
    }
}