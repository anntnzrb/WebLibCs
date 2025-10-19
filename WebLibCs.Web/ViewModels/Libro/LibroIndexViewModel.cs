using WebLibCs.Core.DTOs;

namespace WebLibCs.Web.ViewModels.Libro;

public record LibroIndexViewModel(
    IEnumerable<LibroDto>? Libros = null,
    string? SearchTerm = null,
    int CurrentPage = 1,
    int TotalPages = 0,
    bool HasPrevious = false,
    bool HasNext = false
)
{
    public IEnumerable<LibroDto> LibrosList => Libros ?? [];
}