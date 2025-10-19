namespace WebLibCs.Core.Exceptions;

public class EntityNotFoundException : Exception
{
    public Type EntityType { get; }
    public object Id { get; }

    public EntityNotFoundException() : base("Entity was not found.")
    {
        EntityType = typeof(object);
        Id = string.Empty;
    }

    public EntityNotFoundException(Type entityType, object id)
        : base($"Entity of type {entityType.Name} with id {id} was not found.")
    {
        EntityType = entityType;
        Id = id;
    }

    public EntityNotFoundException(string message) : base(message)
    {
        EntityType = typeof(object);
        Id = string.Empty;
    }

    public EntityNotFoundException(string message, Exception innerException) : base(message, innerException)
    {
        EntityType = typeof(object);
        Id = string.Empty;
    }
}