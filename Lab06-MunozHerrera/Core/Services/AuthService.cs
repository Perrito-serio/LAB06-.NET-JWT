using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Lab06_MunozHerrera.Core.Interfaces;
using Lab06_MunozHerrera.DTOs.Request;
using Lab06_MunozHerrera.Models;
using Microsoft.IdentityModel.Tokens;

namespace Lab06_MunozHerrera.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IConfiguration configuration, IUnitOfWork unitOfWork)
        {
            _configuration = configuration;
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Login(LoginRequestDto loginRequest)
        {
            // Paso 1: Buscar al usuario en la base de datos por su username.
            var user = await _unitOfWork.UserRepository.FindAsync(u => u.Username == loginRequest.Username);

            // Paso 2: Verificar si el usuario existe Y si la contraseña es correcta.
            // Usamos BCrypt.Verify para comparar la contraseña enviada con el hash guardado.
            if (user == null || !BCrypt.Net.BCrypt.Verify(loginRequest.Password, user.PasswordHash))
            {
                return null; // Si el usuario no existe o la contraseña no coincide, devolvemos null.
            }

            // Si la validación es exitosa, procedemos a crear el token.
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, user.Role) // Usamos el rol de la base de datos
            };
            
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

        public async Task<bool> Register(RegisterRequestDto registerRequest)
        {
            var userExists = await _unitOfWork.UserRepository.FindAsync(u => u.Username == registerRequest.Username);
            if (userExists != null)
            {
                return false;
            }

            var passwordHash = BCrypt.Net.BCrypt.HashPassword(registerRequest.Password);

            var newUser = new User
            {
                Username = registerRequest.Username,
                PasswordHash = passwordHash,
                Role = registerRequest.Role
            };

            await _unitOfWork.UserRepository.AddAsync(newUser);
            await _unitOfWork.CompleteAsync();

            return true;
        }
    }
}

