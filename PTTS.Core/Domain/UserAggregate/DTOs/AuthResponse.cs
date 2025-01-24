namespace PTTS.Core.Domain.UserAggregate.DTOs;

public class AuthResponse
{
    public required string FirstName { get; set; }

    public required string LastName { get; set; }

    public required string Email { get; set; }

    public required string Token { get; set; }

    // public required string Role { get; set; }
}