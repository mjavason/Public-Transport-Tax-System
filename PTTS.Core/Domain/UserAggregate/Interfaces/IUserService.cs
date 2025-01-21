using PTTS.Core.Shared;

namespace PTTS.Core.Domain.UserAggregate.Interfaces;

public interface IUserService
{
    public Task<Result> Login(string email, string password);
    public Task<Result> Register(string firstName, string lastName, string email, string password);

    public Task<Result> ConfirmEmail(string userId, string token);
}