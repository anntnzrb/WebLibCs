using WebLibCs.Core.Entities;

namespace WebLibCs.Core.Interfaces.Repositories;

public interface IAutorRepository : IRepository<Autor>
{
    Task<IEnumerable<Autor>> SearchByNameAsync(string searchTerm);
    Task<Autor?> GetWithLibrosAsync(int id);
}