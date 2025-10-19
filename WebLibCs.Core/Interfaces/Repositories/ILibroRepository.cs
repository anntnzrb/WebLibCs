using WebLibCs.Core.Common;
using WebLibCs.Core.Entities;

namespace WebLibCs.Core.Interfaces.Repositories;

public interface ILibroRepository : IRepository<Libro>
{
    Task<PagedResult<Libro>> GetPagedAsync(int page, int pageSize, string? searchTerm = null);
    Task<Libro?> GetByIdWithAutorAsync(int id);
    Task<IEnumerable<Libro>> GetByAutorIdAsync(int autorId);
    Task<IEnumerable<Libro>> SearchByTitleAsync(string searchTerm);
    Task<IEnumerable<Libro>> GetLatestBooksAsync(int count);
}