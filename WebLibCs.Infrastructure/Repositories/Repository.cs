using Microsoft.EntityFrameworkCore;
using WebLibCs.Core.Interfaces.Repositories;
using WebLibCs.Infrastructure.Data;

namespace WebLibCs.Infrastructure.Repositories;

public record Repository<T>(AppDbContext Context) : IRepository<T> where T : class
{
    protected DbSet<T> DbSet { get; } = Context.Set<T>();

    public virtual async Task<T?> GetByIdAsync(int id)
    {
        return await DbSet.FindAsync(id).ConfigureAwait(false);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await DbSet.ToListAsync().ConfigureAwait(false);
    }

    public virtual async Task<T> AddAsync(T entity)
    {
        await DbSet.AddAsync(entity).ConfigureAwait(false);
        return entity;
    }

    public virtual Task UpdateAsync(T entity)
    {
        Context.Entry(entity).State = EntityState.Modified;
        return Task.CompletedTask;
    }

    public virtual Task DeleteAsync(T entity)
    {
        DbSet.Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task<bool> ExistsAsync(int id)
    {
        return await DbSet.FindAsync(id).ConfigureAwait(false) != null;
    }
}