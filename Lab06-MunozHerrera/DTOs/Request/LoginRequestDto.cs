using System.ComponentModel.DataAnnotations;

namespace Lab06_MunozHerrera.DTOs.Request
{
    public class LoginRequestDto
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}