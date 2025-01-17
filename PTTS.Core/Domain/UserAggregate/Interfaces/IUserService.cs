using PTTS.Core.Domain.UserAggregate.DTOs;
using PTTS.Core.Shared;

namespace PTTS.Core.Domain.UserAggregate.Interfaces;

public interface IUserService
{
    public Task<Result<AuthResponse?>> AuthenticateUserAsync(string email, string password);
}