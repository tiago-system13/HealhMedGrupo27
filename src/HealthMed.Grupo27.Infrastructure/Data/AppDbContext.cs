﻿using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using HealthMed.Grupo27.Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.Drawing;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using HealthMed.Grupo27.Infrastructure.Entities;

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
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();

                var srv = Environment.GetEnvironmentVariable("SRV_BD");
                var usr = Environment.GetEnvironmentVariable("USR_BD");
                var pwd = Environment.GetEnvironmentVariable("PWD_BD");

                var connectionString = configuration.GetConnectionString("DefaultConnection");
                connectionString = string.Format(connectionString, srv, usr, pwd);

                optionsBuilder.UseSqlServer(connectionString);
            }
        }
    }
}