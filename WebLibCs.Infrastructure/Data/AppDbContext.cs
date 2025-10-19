using Microsoft.EntityFrameworkCore;
using WebLibCs.Core.Entities;
using WebLibCs.Infrastructure.Data.Configurations;

namespace WebLibCs.Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Libro> Libros { get; set; } = null!;
    public DbSet<Autor> Autores { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Apply configurations
        modelBuilder.ApplyConfiguration(new AutorConfiguration());
        modelBuilder.ApplyConfiguration(new LibroConfiguration());
    }
}