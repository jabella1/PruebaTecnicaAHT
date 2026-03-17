using Microsoft.EntityFrameworkCore;

namespace PruebaTecnicaAHT.Infrastructure.Persistence;

public partial class PruebaTecnicaAhtContext : DbContext
{
    public PruebaTecnicaAhtContext()
    {
    }
    
    public PruebaTecnicaAhtContext(DbContextOptions<PruebaTecnicaAhtContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
