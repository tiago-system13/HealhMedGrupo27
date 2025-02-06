﻿using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telemedicina.Domain.Entities;

namespace Telemedicina.Infrastructure.EntityConfigurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.HasKey(c => c.IdConsulta);
            builder.Property(c => c.DtHoraInicio).IsRequired();
            builder.Property(c => c.DtHoraFim).IsRequired();
            builder.HasOne(c => c.StatusConsulta).WithMany().HasForeignKey(c => c.IdStatusConsulta);
            builder.HasOne(c => c.Medico).WithMany().HasForeignKey(c => c.IdMedico);
            builder.HasOne(c => c.Paciente).WithMany().HasForeignKey(c => c.IdPaciente);
        }
    }
}