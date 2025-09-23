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