namespace PTTS.API.Filters.Model;

public class ErrorResponse
{
    public int Status { get; set; } 
    
    public string Message { get; set; } = string.Empty;
    
    public string Type { get; set; } = string.Empty;
    
    public IList<string>? Errors { get; set; }
}