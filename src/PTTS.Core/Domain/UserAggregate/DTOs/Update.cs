namespace PTTS.Core.Domain.UserAggregate.DTOs;

public class UpdateUserDto
{
	// public required string UserId { get; set; }
	public string? FullName { get; set; }
	public string? FirstName { get; set; }
	public string? LastName { get; set; }
}
