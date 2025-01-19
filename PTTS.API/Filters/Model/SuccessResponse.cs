namespace PTTS.API.Filters.Model;

public class SuccessResponse
{
    public int Status { get; set; } = 200;
    
    public string Message { get; set; } = string.Empty;
}

public class SuccessResponse<T> : SuccessResponse
{
    public T? Data { get; set; }
}
