using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HealthMed.Grupo27.Infrastructure.EntityConfigurations
{
    public class ConsultaConfiguration : IEntityTypeConfiguration<Consulta>
    {
        public void Configure(EntityTypeBuilder<Consulta> builder)
        {
            builder.HasKey(c => c.IdConsulta);
            builder.Property(c => c.HoraInicio).IsRequired();
            builder.Property(c => c.HoraFim).IsRequired();
            builder.HasOne(c => c.StatusConsulta).WithMany().HasForeignKey(c => c.IdStatusConsulta);
            builder.HasOne(c => c.IdMedico).WithMany().HasForeignKey(c => c.IdMedico);
            builder.HasOne(c => c.IdPaciente).WithMany().HasForeignKey(c => c.IdPaciente);
        }
    }
}