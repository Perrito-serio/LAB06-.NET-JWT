using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab06_MunozHerrera.Models;

[Table("asistencias")]
public partial class Asistencia
{
    [Key]
    [Column("id_asistencia")]
    public int IdAsistencia { get; set; }

    [Column("id_estudiante")]
    public int? IdEstudiante { get; set; }

    [Column("id_curso")]
    public int? IdCurso { get; set; }

    [Column("fecha")]
    public DateOnly? Fecha { get; set; }

    [Column("estado")]
    [StringLength(20)]
    public string? Estado { get; set; }

    [ForeignKey("IdCurso")]
    [InverseProperty("Asistencia")]
    public virtual Curso? IdCursoNavigation { get; set; }

    [ForeignKey("IdEstudiante")]
    [InverseProperty("Asistencia")]
    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
