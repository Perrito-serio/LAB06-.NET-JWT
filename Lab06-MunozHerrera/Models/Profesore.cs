using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab06_MunozHerrera.Models;

[Table("profesores")]
public partial class Profesore
{
    [Key]
    [Column("id_profesor")]
    public int IdProfesor { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("especialidad")]
    [StringLength(100)]
    public string? Especialidad { get; set; }

    [Column("correo")]
    [StringLength(100)]
    public string? Correo { get; set; }

    [InverseProperty("IdProfesorNavigation")]
    public virtual ICollection<Curso> Cursos { get; set; } = new List<Curso>();
}
