using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Lab06_MunozHerrera.Models;

[Table("materias")]
public partial class Materia
{
    [Key]
    [Column("id_materia")]
    public int IdMateria { get; set; }

    [Column("id_curso")]
    public int? IdCurso { get; set; }

    [Column("nombre")]
    [StringLength(100)]
    public string Nombre { get; set; } = null!;

    [Column("descripcion")]
    public string? Descripcion { get; set; }

    [ForeignKey("IdCurso")]
    [InverseProperty("Materia")]
    public virtual Curso? IdCursoNavigation { get; set; }
}
