using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Infrastructure.Persistence.Configuration;

public class PacienteConfiguration : IEntityTypeConfiguration<Paciente>
{
    public void Configure(EntityTypeBuilder<Paciente> builder)
    {
        builder.ToTable("Paciente", "dbo");
        builder.HasKey(p => p.PacienteId);
        builder.Property(p => p.Nombre).IsRequired().HasMaxLength(100);
        builder.Property(p => p.Direccion).IsRequired().HasMaxLength(200);
        builder.Property(p => p.Telefono).IsRequired().HasMaxLength(20);
        builder.HasOne(p => p.TipoDocumento)
            .WithMany(t => t.Pacientes)
            .HasForeignKey(p => p.TipoDocumentoId)
            .OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(p => p.Genero).WithMany(g => g.Pacientes).HasForeignKey(p => p.GeneroId);
    }
}