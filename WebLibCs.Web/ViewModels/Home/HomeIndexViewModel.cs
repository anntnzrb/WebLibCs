using WebLibCs.Core.DTOs;

namespace WebLibCs.Web.ViewModels.Home;

public record HomeIndexViewModel
{
    public required IEnumerable<LibroDto> LatestBooks { get; init; }
    public required IEnumerable<AutorDto> FeaturedAuthors { get; init; }
}