using WebLibCs.Core.DTOs;

namespace WebLibCs.Web.ViewModels.Autor;

public record AutorIndexViewModel(
    IEnumerable<AutorDto>? Autores = null,
    string? SearchTerm = null,
    int TotalCount = 0
)
{
    public IEnumerable<AutorDto> AutoresList => Autores ?? [];
}