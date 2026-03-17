using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PruebaTecnicaAHT.Domain.Entities;

namespace PruebaTecnicaAHT.Infrastructure.Persistence.Configuration;

public class GeneroConfiguration : IEntityTypeConfiguration<Genero>
{
    public void Configure(EntityTypeBuilder<Genero> builder)
    {
        builder.ToTable("Generos", "dbo"); 
        builder.HasKey(g => g.GeneroId);
        builder.Property(g => g.Nombre).IsRequired().HasMaxLength(50);
    }
}