using Microsoft.EntityFrameworkCore;
using OficinasHSD.Domain.Models;

namespace OficinasHSD.Infrastructure.Persistence.Context;
public class OficinasHSDDbContext : DbContext
{
    public OficinasHSDDbContext(DbContextOptions<OficinasHSDDbContext> options)
            : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<OficinaMecanica> Oficinas { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(OficinasHSDDbContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}
