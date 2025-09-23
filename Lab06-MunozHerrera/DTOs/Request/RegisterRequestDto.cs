using System.ComponentModel.DataAnnotations;

namespace Lab06_MunozHerrera.DTOs.Request
{
    public class RegisterRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string Role { get; set; } // "Admin", "Profesor", "Vendedor", etc.
    }
}