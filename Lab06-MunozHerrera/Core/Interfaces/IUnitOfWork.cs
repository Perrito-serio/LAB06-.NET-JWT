using Lab06_MunozHerrera.Models;

namespace Lab06_MunozHerrera.Core.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        // Aquí podrías añadir otros repositorios si los necesitaras
        // IGenericRepository<Estudiante> EstudianteRepository { get; }
    
        Task<int> CompleteAsync();
    }
}