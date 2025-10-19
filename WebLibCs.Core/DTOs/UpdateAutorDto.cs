using System.ComponentModel.DataAnnotations;

namespace WebLibCs.Core.DTOs;

public record UpdateAutorDto(
    int Id,

    [property: Required(ErrorMessage = "El nombre es obligatorio")]
    [property: MaxLength(100, ErrorMessage = "El nombre no puede exceder 100 caracteres")]
    string Nombre = ""
);