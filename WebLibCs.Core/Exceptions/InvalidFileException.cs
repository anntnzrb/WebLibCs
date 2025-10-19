namespace WebLibCs.Core.Exceptions;

public class InvalidFileException : Exception
{
    public string? FileName { get; }
    public long? FileSize { get; }

    public InvalidFileException() : base("The file is invalid.")
    {
    }

    public InvalidFileException(string message) : base(message)
    {
    }

    public InvalidFileException(string message, string fileName, long? fileSize = null)
        : base(message)
    {
        FileName = fileName;
        FileSize = fileSize;
    }

    public InvalidFileException(string message, Exception innerException) : base(message, innerException)
    {
    }
}