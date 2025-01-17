namespace PTTS.Core.Shared;

public class SuccessResult
{
    public SuccessResult(string? message,object? data)
    {
        Data = data;
        Message = message ?? "Successful";
    }

    public string Message { get; set; }
    public object? Data { get; set; }
}