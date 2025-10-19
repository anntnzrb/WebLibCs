using WebLibCs.Core.DTOs;

namespace WebLibCs.Web.ViewModels.Autor;

public record DetailsAutorViewModel(
    int Id,
    string Nombre = "",
    string? ImagenRuta = null,
    IEnumerable<LibroDto>? Libros = null
)
{
    public IEnumerable<LibroDto> LibrosList => Libros ?? [];
}