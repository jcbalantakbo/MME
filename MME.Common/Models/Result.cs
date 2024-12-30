namespace MME.Common.Models;

public class Result<T>
{
    public bool IsSuccess { get; private set; }
    public T? Data { get; private set; }
    public Error? Error { get; private set; }

    // Private constructor to enforce usage of factory methods
    private Result(bool isSuccess, T? data = default, Error? error = null)
    {
        IsSuccess = isSuccess;
        Data = data;
        Error = error;
    }

    // Static factory method for success
    public static Result<T> Success(T data) => new Result<T>(true, data);

    // Static factory method for failure
    public static Result<T> Failure(Error error) => new Result<T>(false, default, error);
}
