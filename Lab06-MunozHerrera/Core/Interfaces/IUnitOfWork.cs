using Lab06_MunozHerrera.Models;

namespace Lab06_MunozHerrera.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
    
        Task<int> CompleteAsync();
    }
}