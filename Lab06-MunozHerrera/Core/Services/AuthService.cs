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

            public AuthService(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            
            public string Login(LoginRequestDto loginRequest)
            {
                // PASO 1: Validar las credenciales (versión simplificada).
                // TODO: Reemplazar esto con la validación contra la base de datos.
                if (loginRequest.Username != "admin" || loginRequest.Password != "admin")
                {
                    return null; // Credenciales incorrectas
                }

                // PASO 2: Crear los 'claims' (información que irá dentro del token).
                var claims = new[]
                {
                    new Claim(ClaimTypes.Name, loginRequest.Username),
                    new Claim(ClaimTypes.Role, "Admin") // Asignamos el rol directamente
                }; 
                
                // PASO 3: Obtener la clave secreta desde appsettings.json.
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                
                // PASO 4: Crear las credenciales de firma.
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // PASO 5: Crear el token JWT.
                var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30), // El token expira en 30 minutos
                    signingCredentials: creds
                );

                // PASO 6: Escribir el token como un string.
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }