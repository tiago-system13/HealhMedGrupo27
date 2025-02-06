using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telemedicina.Domain.Entities;

namespace Telemedicina.Infrastructure.EntityConfigurations
{
    public class MedicoConfiguration : IEntityTypeConfiguration<Medico>
    {
        public void Configure(EntityTypeBuilder<Medico> builder)
        {
            builder.HasKey(m => m.IdMedico);
            builder.Property(m => m.Nome).HasMaxLength(40).IsRequired();
            builder.Property(m => m.Especialidade).HasMaxLength(20).IsRequired();
            builder.Property(m => m.CRM).HasMaxLength(6).IsRequired();
            builder.Property(m => m.Email).HasMaxLength(30).IsRequired();
            builder.Property(m => m.CPF).HasMaxLength(14).IsRequired();
            builder.Property(m => m.Telefone).HasMaxLength(11).IsRequired();
        }
    }
}