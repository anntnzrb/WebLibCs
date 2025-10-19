namespace WebLibCs.Web.ViewModels.Autor;

public record DeleteAutorViewModel(
    int Id,
    string Nombre = "",
    int LibrosCount = 0,
    string? ImagenRuta = null
);