using System.ComponentModel.DataAnnotations;
using MediatR;
using PTTS.Core.Domain.UserAggregate.DTOs;
using PTTS.Core.Shared;

namespace PTTS.Core.Features.Authentication.SignIn;

public class SignInQuery : IRequest<Result<AuthResponse?>>
{
    [Required]
    public required string Email { get; set; }

    [Required]
    public required string Password { get; set; }
}