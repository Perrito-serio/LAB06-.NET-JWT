using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab06_MunozHerrera.Core.Interfaces;
using Lab06_MunozHerrera.DTOs.Request;
using Microsoft.IdentityModel.Tokens;

namespace Lab06_MunozHerrera.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork; // <-- Inyectamos el Unit of Work

        // Modificamos el constructor para recibir IUnitOfWork
        public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }
            
        // El método ahora debe ser asíncrono
        public async Task<string> Login(LoginRequestDto loginRequest)
        {
            // PASO 1: Buscar al usuario por su username
            var user = await _unitOfWork.UserRepository.FindAsync(u => u.Username == loginRequest.Username);

            if (user == null)
            {
                return null; // Usuario no encontrado
            }
            
            // PASO 2: Verificar la contraseña encriptada
            // Usamos BCrypt para comparar la contraseña enviada con el hash guardado
            if (!BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                return null; // Contraseña incorrecta
            }

            // PASO 3: Crear los 'claims' con la información del usuario de la BD
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role) // ¡Usamos el rol de la base de datos!
            }; 
            
            // El resto del método para generar el token es igual...
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
