using System.ComponentModel.DataAnnotations;

namespace Lab06_MunozHerrera.DTOs.Estudiante
{
    public class EstudianteRequestDto
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public int Edad { get; set; }
        public string? Direccion { get; set; }
        public string? Telefono { get; set; }
        public string? Correo { get; set; }
    }
}