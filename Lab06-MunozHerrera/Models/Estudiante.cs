using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab06_MunozHerrera.Models;

[Table("estudiantes")]
public partial class Estudiante
{
    [Key]
    [Column("id_estudiante")]
    public int IdEstudiante { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("edad")]
    public int Edad { get; set; }

    [Column("direccion")]
    [StringLength(255)]
    public string? Direccion { get; set; }

    [Column("telefono")]
    [StringLength(20)]
    public string? Telefono { get; set; }

    [Column("correo")]
    [StringLength(100)]
    public string? Correo { get; set; }

    [InverseProperty("IdEstudianteNavigation")]
    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    [InverseProperty("IdEstudianteNavigation")]
    public virtual ICollection<Evaluacione> Evaluaciones { get; set; } = new List<Evaluacione>();

    [InverseProperty("IdEstudianteNavigation")]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
