using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telemedicina.Domain.Entities;

namespace Telemedicina.Infrastructure.EntityConfigurations
{
    public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
    {
        public void Configure(EntityTypeBuilder<Paciente> builder)
        {
            builder.HasKey(p => p.IdPaciente);
            builder.Property(p => p.Nome).HasMaxLength(40).IsRequired();
            builder.Property(p => p.Email).HasMaxLength(30).IsRequired();
            builder.Property(p => p.CPF).HasMaxLength(14).IsRequired();
            builder.Property(p => p.Telefone).HasMaxLength(11).IsRequired();
        }
    }
}