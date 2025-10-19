using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace WebLibCs.Core.Entities;

public record Libro(
    int Id,

    [property: Required(ErrorMessage = "El título es obligatorio")]
    [property: MaxLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
    string Titulo = "",

    [property: Display(Name = "Año de publicación")]
    [property: Range(1000, 2100, ErrorMessage = "El año debe estar entre 1000 y 2100")]
    int AnioPublicacion = 0,

    // FK y navegación
    [property: Display(Name = "Autor")]
    [property: Required(ErrorMessage = "Debe seleccionar un autor")]
    int AutorId = 0,

    [property: Display(Name = "Portada")]
    [property: MaxLength(500, ErrorMessage = "La ruta de la imagen no puede exceder 500 caracteres")]
    string? ImagenRuta = null
)
{
    [ForeignKey("AutorId")]
    public virtual Autor Autor { get; init; } = null!;

    [NotMapped]
    public IFormFile? ImagenArchivo { get; init; }
}