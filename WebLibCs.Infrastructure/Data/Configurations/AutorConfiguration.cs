using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebLibCs.Core.Entities;

namespace WebLibCs.Infrastructure.Data.Configurations;

public record AutorConfiguration : IEntityTypeConfiguration<Autor>
{
    public void Configure(EntityTypeBuilder<Autor> builder)
    {
        builder.HasKey(a => a.Id);

        builder.Property(a => a.Nombre)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(a => a.Id)
            .ValueGeneratedOnAdd();

        // Configure the one-to-many relationship
        builder.HasMany(a => a.Libros)
            .WithOne(l => l.Autor)
            .HasForeignKey(l => l.AutorId)
            .OnDelete(DeleteBehavior.Cascade);

        // Seed data (optional - you can remove if you don't want seed data)
        builder.HasData(
            new Autor(1, "Gabriel García Márquez"),
            new Autor(2, "J.K. Rowling"),
            new Autor(3, "Stephen King")
        );
    }
}