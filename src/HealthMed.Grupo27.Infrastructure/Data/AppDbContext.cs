using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HealthMed.Grupo27.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Drawing;

namespace HealthMed.Grupo27.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public AppDbContext() : base(){}

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Medico> Medicos { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Consulta> Consultas { get; set; }
        public DbSet<HorarioMedico> HorariosMedicos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var usr = Environment.GetEnvironmentVariable("USR_BD");
                var pwd = Environment.GetEnvironmentVariable("PWD_BD");

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                connectionString = string.Format(connectionString, usr, pwd);

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}