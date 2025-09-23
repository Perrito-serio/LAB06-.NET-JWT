using Lab06_MunozHerrera.DTOs.Request;
    
namespace Lab06_MunozHerrera.Core.Interfaces
{
    public interface IAuthService
    {
        // Devuelve el token como string si el login es exitoso, o null si falla.
        string Login(LoginRequestDto loginRequest);
    }
}