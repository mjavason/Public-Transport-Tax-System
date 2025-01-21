using System.ComponentModel.DataAnnotations;

namespace PTTS.Core.Domain.UserAggregate.DTOs;

public class RegisterDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }

    [Required]
    [MinLength(6)]
    public required string Password { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}