using System.ComponentModel.DataAnnotations;

namespace WebLibCs.Core.Entities;

public record Autor(
    int Id,

    [property: Required(ErrorMessage = "El nombre es obligatorio")]
    [property: MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    string Nombre = ""
)
{
    // Relación 1-N
    public virtual ICollection<Libro> Libros { get; init; } = [];

    [property: MaxLength(500, ErrorMessage = "La ruta de la imagen no puede exceder 500 caracteres")]
    public string? ImagenRuta { get; set; } = null;

    [System.ComponentModel.DataAnnotations.Schema.NotMapped]
    public Microsoft.AspNetCore.Http.IFormFile? ImagenArchivo { get; init; }
}