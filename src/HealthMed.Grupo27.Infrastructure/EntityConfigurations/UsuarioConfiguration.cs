using HealthMed.Grupo27.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Telemedicina.Domain.Entities;

namespace Telemedicina.Infrastructure.EntityConfigurations
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.HasKey(u => u.IdUsuario);
            builder.Property(u => u.Senha).HasMaxLength(40).IsRequired();

            builder.HasMany(u => u.Medicos)
                   .WithMany(m => m.Usuarios)
                   .UsingEntity(j => j.ToTable("UsuarioMedico"));

            builder.HasMany(u => u.Pacientes)
                   .WithMany(p => p.Usuarios)
                   .UsingEntity(j => j.ToTable("UsuarioPaciente"));
        }
    }
}