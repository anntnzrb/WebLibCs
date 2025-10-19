namespace WebLibCs.Web.ViewModels.Libro;

public record LibroDetailsViewModel(
    int Id,
    string Titulo = "",
    int AnioPublicacion = 0,
    string AutorNombre = "",
    string? ImagenRuta = null
);