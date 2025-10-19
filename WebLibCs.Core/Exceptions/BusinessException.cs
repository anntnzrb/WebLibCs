namespace WebLibCs.Core.Exceptions;

public class BusinessException : Exception
{
    public BusinessException() : base("A business rule violation occurred.")
    {
    }

    public BusinessException(string message) : base(message)
    {
    }

    public BusinessException(string message, Exception innerException) : base(message, innerException)
    {
    }
}