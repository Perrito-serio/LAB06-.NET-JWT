using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab06_MunozHerrera.Models;

[Table("cursos")]
public partial class Curso
{
    [Key]
    [Column("id_curso")]
    public int IdCurso { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [Column("creditos")]
    public int Creditos { get; set; }

    [Column("id_profesor")]
    public int? IdProfesor { get; set; }

    [InverseProperty("IdCursoNavigation")]
    public virtual ICollection<Asistencia> Asistencia { get; set; } = new List<Asistencia>();

    [InverseProperty("IdCursoNavigation")]
    public virtual ICollection<Evaluacione> Evaluaciones { get; set; } = new List<Evaluacione>();

    [ForeignKey("IdProfesor")]
    [InverseProperty("Cursos")]
    public virtual Profesore? IdProfesorNavigation { get; set; }

    [InverseProperty("IdCursoNavigation")]
    public virtual ICollection<Materia> Materia { get; set; } = new List<Materia>();

    [InverseProperty("IdCursoNavigation")]
    public virtual ICollection<Matricula> Matriculas { get; set; } = new List<Matricula>();
}
