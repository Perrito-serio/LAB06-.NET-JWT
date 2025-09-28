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
        private readonly IUnitOfWork _unitOfWork; // Necesario para el endpoint de prueba

        public AuthController(IAuthService authService, IUnitOfWork unitOfWork)
        {
            _authService = authService;
            _unitOfWork = unitOfWork;
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
        
        // --- SEGURIDAD RESTAURADA ---
        // Se ha vuelto a colocar [Authorize(Roles = "Admin")].
        // Ahora, solo un usuario autenticado con el rol "Admin" puede crear nuevos usuarios.
        [HttpPost("register")]
        [Authorize(Roles = "Admin")] 
        public async Task<IActionResult> Register([FromBody] RegisterRequestDto registerRequest)
        {
            var result = await _authService.Register(registerRequest);
            if (!result)
            {
                return BadRequest("No se pudo registrar al usuario. Es posible que el nombre de usuario ya exista.");
            }
            return Ok(new { message = "Usuario registrado exitosamente." });
        }

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

