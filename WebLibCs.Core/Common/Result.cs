namespace WebLibCs.Core.Common;

public abstract record Result(
    bool IsSuccess,
    string Message = "",
    ICollection<string>? Errors = null)
{
    public ICollection<string> ErrorsList { get; init; } = Errors ?? [];


}

public record SuccessResult(string Message = "") : Result(true, Message);

public record FailureResult(string Message, ICollection<string>? Errors = null) : Result(false, Message, Errors)
{
    public FailureResult(ICollection<string> errors) : this("", errors) { }
}

public record Result<T>(
    bool IsSuccess,
    T? Data = default,
    string Message = "",
    ICollection<string>? Errors = null
) : Result(IsSuccess, Message, Errors);

public static class ResultFactory
{
    public static Result Success(string message = "")
    {
        return new SuccessResult(message);
    }

    public static Result Failure(string message)
    {
        return new FailureResult(message);
    }

    public static Result Failure(ICollection<string> errors)
    {
        return new FailureResult(errors);
    }

    public static Result<T> Success<T>(T data, string message = "")
    {
        return new Result<T>(true, data, message);
    }

    public static Result<T> Failure<T>(string message)
    {
        return new Result<T>(false, default, message);
    }

    public static Result<T> Failure<T>(ICollection<string> errors)
    {
        return new Result<T>(false, default, "", errors);
    }
}