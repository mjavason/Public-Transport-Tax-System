namespace PTTS.Core.Shared;

public class Result
{
    public bool IsSuccess { get; protected set; }
    public ErrorType ErrorType { get; protected set; } = ErrorType.InternalServerError;
    public string? Message { get; protected set; }
    public List<string> Errors { get; protected set; } = new List<string>();

    protected Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    protected Result(List<string> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }

    public static Result Success()
    {
        return new Result(true);
    }
    public static Result Success(string message)
    {
        return new Result(true) { Message = message };
    }
    public static Result Failure(List<string> errors)
    {
        return new Result(errors);
    }

    private static Result CreateFailureResult(List<string>? errors, ErrorType errorType)
    {
        return new Result(errors ?? new List<string>())
        {
            ErrorType = errorType
        };
    }

    public static Result BadRequest(List<string>? errors) => CreateFailureResult(errors, ErrorType.BadRequest);
    public static Result Unauthorized(List<string>? errors) => CreateFailureResult(errors, ErrorType.Unauthorized);
    public static Result PreconditionFailed(List<string>? errors) => CreateFailureResult(errors, ErrorType.PreconditionFailed);
    public static Result Forbidden(List<string>? errors) => CreateFailureResult(errors, ErrorType.Forbidden);
    public static Result NotFound(List<string>? errors) => CreateFailureResult(errors, ErrorType.NotFound);
    public static Result UnprocessableEntity(List<string>? errors) => CreateFailureResult(errors, ErrorType.UnprocessableEntity);
    public static Result Conflict(List<string>? errors) => CreateFailureResult(errors, ErrorType.Conflict);

    public SuccessResult GetSuccessResult(string message)
    {
        return new SuccessResult(message, null);
    }
    public static Result<T> Success<T>(T? data)
    {
        return new Result<T>(true, data ?? default!);
    }
    public static Result<T> Success<T>(T? data, string message)
    {
        return new Result<T>(true, data ?? default!)
        {
            Message = message
        };
    }

    public static Result<T> Failure<T>(List<string> errors)
    {
        return new Result<T>(errors);
    }

    private static Result<T> CreateFailureResult<T>(List<string>? errors, ErrorType errorType)
    {
        return new Result<T>(errors ?? new List<string>())
        {
            ErrorType = errorType
        };
    }

    public static Result<T> BadRequest<T>(List<string>? errors) => CreateFailureResult<T>(errors, ErrorType.BadRequest);
    public static Result<T> Unauthorized<T>(List<string>? errors) => CreateFailureResult<T>(errors, ErrorType.Unauthorized);
    public static Result<T> PreconditionFailed<T>(List<string>? errors) => CreateFailureResult<T>(errors, ErrorType.PreconditionFailed);
    public static Result<T> Forbidden<T>(List<string>? errors) => CreateFailureResult<T>(errors, ErrorType.Forbidden);
    public static Result<T> NotFound<T>(List<string>? errors) => CreateFailureResult<T>(errors, ErrorType.NotFound);
    public static Result<T> Conflict<T>(List<string>? errors) => CreateFailureResult<T>(errors, ErrorType.Conflict);
    public static Result<T> UnprocessableEntity<T>(List<string>? errors) => CreateFailureResult<T>(errors, ErrorType.UnprocessableEntity);
}

public class Result<T> : Result
{
    protected internal Result(bool isSuccess, T data) : base(isSuccess)
    {
        Data = data;
    }

    protected internal Result(List<string> errors) : base(errors)
    {
        Data = default!;
    }

    public T Data { get; private set; }

    public new SuccessResult GetSuccessResult(string message)
    {
        return new SuccessResult(message, Data);
    }
}