using WebLibCs.Core.Entities;

namespace WebLibCs.Core.Specifications;

public record LibroSpecification : BaseSpecification<Libro>
{
    public LibroSpecification(int id) : base(libro => libro.Id == id)
    {
        AddInclude(libro => libro.Autor);
    }

    public LibroSpecification(string? searchTerm = null) : base(libro =>
        string.IsNullOrEmpty(searchTerm) || libro.Titulo.Contains(searchTerm))
    {
        AddInclude(libro => libro.Autor);
    }

    public LibroSpecification(int autorId, string? searchTerm = null) : base(libro =>
        libro.AutorId == autorId &&
        (string.IsNullOrEmpty(searchTerm) || libro.Titulo.Contains(searchTerm)))
    {
        AddInclude(libro => libro.Autor);
    }

    public LibroSpecification(int page, int pageSize, string? searchTerm = null) : base(libro =>
        string.IsNullOrEmpty(searchTerm) || libro.Titulo.Contains(searchTerm))
    {
        AddInclude(libro => libro.Autor);
        ApplyPaging((page - 1) * pageSize, pageSize);
    }
}