using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Infrastructure.Persistence.Configuration;

public class TipoDocumentoConfiguration : IEntityTypeConfiguration<TipoDocumento>
{
    public void Configure(EntityTypeBuilder<TipoDocumento> builder)
    {
        builder.ToTable("TipoDocumento", "dbo"); 
        builder.HasKey(t => t.TipoDocumentoId);
        builder.Property(t => t.Tipo).IsRequired().HasMaxLength(50);
    }
}