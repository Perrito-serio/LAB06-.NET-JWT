using Lab06_MunozHerrera.Core.Interfaces;
using Lab06_MunozHerrera.DTOs.Request;
using Lab06_MunozHerrera.DTOs.Response;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Lab06_MunozHerrera.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginRequestDto loginRequest)
        {
            var token = await _authService.Login(loginRequest);

            if (token == null)
            {
                return Unauthorized("Credenciales inválidas.");
            }

            return Ok(new LoginResponseDto { Token = token });
        }
        
        // --- INICIO DEL NUEVO MÉTODO DE REGISTRO ---
        [HttpPost("register")]
        [Authorize(Roles = "Admin")] // <-- ¡SOLO los usuarios con rol "Admin" pueden acceder a este endpoint!
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var result = await _authService.Register(registerRequest);

            if (!result)
            {
                // Si el servicio devuelve 'false' (ej. el usuario ya existe), enviamos un error.
                return BadRequest("No se pudo registrar al usuario. Es posible que el nombre de usuario ya exista.");
            }

            // Si todo fue bien, enviamos una respuesta exitosa.
            return Ok(new { message = "Usuario registrado exitosamente." });
        }
        // --- FIN DEL NUEVO MÉTODO DE REGISTRO ---

        [HttpGet("admin-data")]
        [Authorize(Roles = "Admin")]
        public IActionResult GetAdminData()
        {
            return Ok("Estos son datos súper secretos solo para administradores.");
        }
        
        [HttpGet("public-data")]
        [AllowAnonymous]
        public IActionResult GetPublicData()
        {
            return Ok("Estos son datos públicos para todo el mundo.");
        }
    }
}

