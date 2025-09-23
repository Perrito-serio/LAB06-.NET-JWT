using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab06_MunozHerrera.Models;

[Table("matriculas")]
public partial class Matricula
{
    [Key]
    [Column("id_matricula")]
    public int IdMatricula { get; set; }

    [Column("id_estudiante")]
    public int? IdEstudiante { get; set; }

    [Column("id_curso")]
    public int? IdCurso { get; set; }

    [Column("semestre")]
    [StringLength(20)]
    public string? Semestre { get; set; }

    [ForeignKey("IdCurso")]
    [InverseProperty("Matriculas")]
    public virtual Curso? IdCursoNavigation { get; set; }

    [ForeignKey("IdEstudiante")]
    [InverseProperty("Matriculas")]
    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
