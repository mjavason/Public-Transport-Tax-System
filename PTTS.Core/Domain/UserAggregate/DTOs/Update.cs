namespace PTTS.Core.Domain.UserAggregate.DTOs;

public class UpdateUserProfileDto
{
    public string? FullName { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}