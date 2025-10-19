namespace WebLibCs.Core.DTOs;

public record LibroDto(
    int Id,
    string Titulo = "",
    int AnioPublicacion = 0,
    int AutorId = 0,
    string AutorNombre = "",
    string? ImagenRuta = null
);