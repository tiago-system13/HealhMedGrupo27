using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HealthMed.Grupo27.Domain.Entities;

namespace HealthMed.Grupo27.Infrastructure.EntityConfigurations
{
    public class HorarioMedicoConfiguration : IEntityTypeConfiguration<HorarioMedico>
    {
        public void Configure(EntityTypeBuilder<HorarioMedico> builder)
        {
            builder.HasKey(h => h.IdHorario);
            builder.Property(h => h.DiaSemana).IsRequired();
            builder.Property(h => h.HoraInicio).IsRequired();
            builder.Property(h => h.HoraFim).IsRequired();
            builder.Property(h => h.Status).IsRequired();
            builder.HasOne(h => h.Medico).WithMany().HasForeignKey(h => h.IdMedico);
        }
    }
}
