using Microsoft.EntityFrameworkCore;
using WebLibCs.Core.Common;
using WebLibCs.Core.Entities;
using WebLibCs.Core.Interfaces.Repositories;
using WebLibCs.Infrastructure.Data;

namespace WebLibCs.Infrastructure.Repositories;

public record LibroRepository(AppDbContext Context) : Repository<Libro>(Context), ILibroRepository
{
    public async Task<PagedResult<Libro>> GetPagedAsync(int page, int pageSize, string? searchTerm = null)
    {
        var query = DbSet.Include(l => l.Autor).AsQueryable();

        if (!string.IsNullOrWhiteSpace(searchTerm))
        {
            query = query.Where(l => l.Titulo.Contains(searchTerm));
        }

        var totalCount = await query.CountAsync().ConfigureAwait(false);
        var items = await query
            .OrderBy(l => l.Titulo)
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync().ConfigureAwait(false);

        return new PagedResult<Libro>(
            items,
            page,
            pageSize,
            totalCount
        );
    }

    public async Task<Libro?> GetByIdWithAutorAsync(int id)
    {
        return await DbSet
            .Include(l => l.Autor)
            .FirstOrDefaultAsync(l => l.Id == id).ConfigureAwait(false);
    }

    public async Task<IEnumerable<Libro>> GetByAutorIdAsync(int autorId)
    {
        return await DbSet
            .Where(l => l.AutorId == autorId)
            .OrderBy(l => l.Titulo)
            .ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Libro>> SearchByTitleAsync(string searchTerm)
    {
        if (string.IsNullOrWhiteSpace(searchTerm))
            return await GetAllAsync().ConfigureAwait(false);

        return await DbSet
            .Include(l => l.Autor)
            .Where(l => l.Titulo.Contains(searchTerm))
            .OrderBy(l => l.Titulo)
            .ToListAsync().ConfigureAwait(false);
    }

    public async Task<IEnumerable<Libro>> GetLatestBooksAsync(int count)
    {
        return await DbSet
            .Include(l => l.Autor)
            .OrderByDescending(l => l.Id) // Assuming ID correlates with recency
            .Take(count)
            .ToListAsync().ConfigureAwait(false);
    }
}