using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Telemedicina.Domain.Entities;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Medico> Medicos { get; set; }
    public DbSet<Paciente> Pacientes { get; set; }
    public DbSet<Consulta> Consultas { get; set; }
    public DbSet<StatusConsulta> StatusConsultas { get; set; }
    public DbSet<HorarioMedico> HorariosMedicos { get; set; }
}