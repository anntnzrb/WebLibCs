namespace WebLibCs.Core.DTOs;

public record AutorDto(
    int Id,
    string Nombre = "",
    int LibrosCount = 0,
    string? ImagenRuta = null
);