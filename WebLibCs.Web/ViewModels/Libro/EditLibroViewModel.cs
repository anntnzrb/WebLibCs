using System.ComponentModel.DataAnnotations;
using WebLibCs.Core.DTOs;

namespace WebLibCs.Web.ViewModels.Libro;

public record EditLibroViewModel(
    int Id,

    [property: Required(ErrorMessage = "El título es obligatorio")]
    [property: MaxLength(200, ErrorMessage = "El título no puede exceder 200 caracteres")]
    string Titulo = "",

    [property: Range(1000, 2100, ErrorMessage = "El año debe estar entre 1000 y 2100")]
    int AnioPublicacion = 0,

    [property: Required(ErrorMessage = "Debe seleccionar un autor")]
    int AutorId = 0,

    string? ImagenRutaActual = null,
    IFormFile? ImagenArchivo = null,
    IEnumerable<AutorDto>? Autores = null
)
{
    public IEnumerable<AutorDto> AutoresList => Autores ?? [];
}