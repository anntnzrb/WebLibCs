using Microsoft.EntityFrameworkCore;
using WebLibCs.Core.Entities;
using WebLibCs.Core.Interfaces.Repositories;
using WebLibCs.Infrastructure.Data;

namespace WebLibCs.Infrastructure.Repositories;

public record AutorRepository(AppDbContext Context) : Repository<Autor>(Context), IAutorRepository
{
    public async Task<IEnumerable<Autor>> SearchByNameAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync().ConfigureAwait(false);

        return await DbSet
            .Where(a => a.Nombre.Contains(searchTerm))
            .ToListAsync().ConfigureAwait(false);
    }

    public async Task<Autor?> GetWithLibrosAsync(int id)
    {
        return await DbSet
            .Include(a => a.Libros)
            .FirstOrDefaultAsync(a => a.Id == id).ConfigureAwait(false);
    }
}