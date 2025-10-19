namespace WebLibCs.Core.Interfaces.Repositories;

public interface IUnitOfWork : IDisposable
{
    IAutorRepository Autores { get; }
    ILibroRepository Libros { get; }
    Task<int> SaveChangesAsync();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}