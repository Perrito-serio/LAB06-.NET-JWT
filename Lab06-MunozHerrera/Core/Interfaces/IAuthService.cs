using Lab06_MunozHerrera.DTOs.Request;
    
namespace Lab06_MunozHerrera.Core.Interfaces
{
    public interface IAuthService
    {
        Task<string> Login(LoginRequestDto loginRequest);
        
        Task<bool> Register(RegisterRequestDto registerRequest);
    }
}