using System;
using System.Collections.Generic;
using Lab06_MunozHerrera.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab06_MunozHerrera.Data;

public partial class UniversidadDbContext : DbContext
{
    public UniversidadDbContext()
    {
    }

    public UniversidadDbContext(DbContextOptions<UniversidadDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asistencia> Asistencias { get; set; }

    public virtual DbSet<Curso> Cursos { get; set; }

    public virtual DbSet<Estudiante> Estudiantes { get; set; }

    public virtual DbSet<Evaluacione> Evaluaciones { get; set; }

    public virtual DbSet<Materia> Materias { get; set; }

    public virtual DbSet<Matricula> Matriculas { get; set; }

    public virtual DbSet<Profesore> Profesores { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=universidad_db;Username=postgres;Password=la1vida1es1oro");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asistencia>(entity =>
        {
            entity.HasKey(e => e.IdAsistencia).HasName("asistencias_pkey");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Asistencia).HasConstraintName("asistencias_id_curso_fkey");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Asistencia).HasConstraintName("asistencias_id_estudiante_fkey");
        });

        modelBuilder.Entity<Curso>(entity =>
        {
            entity.HasKey(e => e.IdCurso).HasName("cursos_pkey");

            entity.HasOne(d => d.IdProfesorNavigation).WithMany(p => p.Cursos).HasConstraintName("fk_profesor");
        });

        modelBuilder.Entity<Estudiante>(entity =>
        {
            entity.HasKey(e => e.IdEstudiante).HasName("estudiantes_pkey");
        });

        modelBuilder.Entity<Evaluacione>(entity =>
        {
            entity.HasKey(e => e.IdEvaluacion).HasName("evaluaciones_pkey");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Evaluaciones).HasConstraintName("evaluaciones_id_curso_fkey");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Evaluaciones).HasConstraintName("evaluaciones_id_estudiante_fkey");
        });

        modelBuilder.Entity<Materia>(entity =>
        {
            entity.HasKey(e => e.IdMateria).HasName("materias_pkey");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Materia).HasConstraintName("materias_id_curso_fkey");
        });

        modelBuilder.Entity<Matricula>(entity =>
        {
            entity.HasKey(e => e.IdMatricula).HasName("matriculas_pkey");

            entity.HasOne(d => d.IdCursoNavigation).WithMany(p => p.Matriculas).HasConstraintName("matriculas_id_curso_fkey");

            entity.HasOne(d => d.IdEstudianteNavigation).WithMany(p => p.Matriculas).HasConstraintName("matriculas_id_estudiante_fkey");
        });

        modelBuilder.Entity<Profesore>(entity =>
        {
            entity.HasKey(e => e.IdProfesor).HasName("profesores_pkey");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.IdUser).HasName("users_pkey");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
