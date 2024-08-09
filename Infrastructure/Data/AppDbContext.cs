using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;
public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<Usuario> Usuario { get; set; }
    public DbSet<Regiao> Regiao { get; set; }
    public DbSet<Contato> Contato { get; set; }
    public DbSet<DDD> DDD { get; set; }
    public DbSet<TipoTelefone> TipoTelefone { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
