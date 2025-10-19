using Microsoft.EntityFrameworkCore.Storage;
using WebLibCs.Core.Interfaces.Repositories;
using WebLibCs.Infrastructure.Data;

namespace WebLibCs.Infrastructure.Repositories;

public record UnitOfWork(AppDbContext Context) : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _context = Context;
    private IDbContextTransaction? _transaction;
    private IAutorRepository? _autores;
    private ILibroRepository? _libros;
    private bool _disposed;

    public IAutorRepository Autores => _autores ??= new AutorRepository(_context);
    public ILibroRepository Libros => _libros ??= new LibroRepository(_context);

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync().ConfigureAwait(false);
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _context.Database.BeginTransactionAsync().ConfigureAwait(false);
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync().ConfigureAwait(false);
            await _transaction.DisposeAsync().ConfigureAwait(false);
            _transaction = null;
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync().ConfigureAwait(false);
            await _transaction.DisposeAsync().ConfigureAwait(false);
            _transaction = null;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed && disposing)
        {
            _transaction?.Dispose();
            _context.Dispose();
            _disposed = true;
        }
    }
}