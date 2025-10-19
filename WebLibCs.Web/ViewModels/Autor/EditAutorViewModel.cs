using System.ComponentModel.DataAnnotations;

namespace WebLibCs.Web.ViewModels.Autor;

public record EditAutorViewModel(
    int Id,
    [property: Required(ErrorMessage = "El nombre es obligatorio")]
    [property: MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    string Nombre = "",

    string? ImagenRutaActual = null,
    Microsoft.AspNetCore.Http.IFormFile? ImagenArchivo = null
);