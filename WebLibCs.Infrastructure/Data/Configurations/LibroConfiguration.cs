using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLibCs.Core.Entities;

namespace WebLibCs.Infrastructure.Data.Configurations;

public record LibroConfiguration : IEntityTypeConfiguration<Libro>
{
    public void Configure(EntityTypeBuilder<Libro> builder)
    {
        builder.HasKey(l => l.Id);

        builder.Property(l => l.Titulo)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(l => l.AnioPublicacion)
            .IsRequired();

        builder.Property(l => l.AutorId)
            .IsRequired();

        builder.Property(l => l.ImagenRuta)
            .HasMaxLength(500);

        builder.Property(l => l.Id)
            .ValueGeneratedOnAdd();

        // Configure the many-to-one relationship
        builder.HasOne(l => l.Autor)
            .WithMany(a => a.Libros)
            .HasForeignKey(l => l.AutorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed data (optional - you can remove if you don't want seed data)
        builder.HasData(
            new Libro(1, "Cien años de soledad", 1967, 1),
            new Libro(2, "Harry Potter y la piedra filosofal", 1997, 2),
            new Libro(3, "El resplandor", 1977, 3)
        );
    }
}