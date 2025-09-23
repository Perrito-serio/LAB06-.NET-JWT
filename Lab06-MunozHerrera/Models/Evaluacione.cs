using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab06_MunozHerrera.Models;

[Table("evaluaciones")]
public partial class Evaluacione
{
    [Key]
    [Column("id_evaluacion")]
    public int IdEvaluacion { get; set; }

    [Column("id_estudiante")]
    public int? IdEstudiante { get; set; }

    [Column("id_curso")]
    public int? IdCurso { get; set; }

    [Column("calificacion")]
    [Precision(5, 2)]
    public decimal? Calificacion { get; set; }

    [Column("fecha")]
    public DateOnly? Fecha { get; set; }

    [ForeignKey("IdCurso")]
    [InverseProperty("Evaluaciones")]
    public virtual Curso? IdCursoNavigation { get; set; }

    [ForeignKey("IdEstudiante")]
    [InverseProperty("Evaluaciones")]
    public virtual Estudiante? IdEstudianteNavigation { get; set; }
}
