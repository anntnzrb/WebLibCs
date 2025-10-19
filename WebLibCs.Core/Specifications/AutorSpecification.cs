using WebLibCs.Core.Entities;

namespace WebLibCs.Core.Specifications;

public record AutorSpecification : BaseSpecification<Autor>
{
    public AutorSpecification(int id) : base(autor => autor.Id == id)
    {
        AddInclude(autor => autor.Libros);
    }

    public AutorSpecification(string searchTerm) : base(autor =>
        string.IsNullOrEmpty(searchTerm) || autor.Nombre.Contains(searchTerm))
    {
    }

    public AutorSpecification() : base()
    {
    }
}