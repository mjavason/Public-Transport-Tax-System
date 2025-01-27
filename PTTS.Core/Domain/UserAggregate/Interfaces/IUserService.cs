using Microsoft.AspNetCore.Identity;
using PTTS.Core.Domain.UserAggregate.DTOs;
using PTTS.Core.Shared;

namespace PTTS.Core.Domain.UserAggregate.Interfaces;

public interface IUserService
{
    public Task<Result<AuthResponse?>> Login(string email, string password);
    public Task<Result> Register(string firstName, string lastName, string email, string password);
    public Task<Result> ConfirmEmail(string userId, string token);
    public Task<Result> ForgotPassword(string email);
    public Task<Result> ResetPassword(string userId, string token, string password);
    public Task<Result> CreateRole(string roleName);
    public Task<Result> AddUserToRole(string userId, string roleName);
    public Task<Result> RemoveUserFromRole(string userId, string roleName);
    public Task<Result> DeleteRole(string roleName);
    public Task<Result> UpdateRole(string roleName, string newRoleName);
    public Task<IList<IdentityRole>> GetRoles();
    public Task<IList<User>> GetUsersInRole(string roleName);
    public Task<IList<User>> GetUsersNotInRole(string roleName);
    public Task<IList<string>> GetUserRoles(string userId);
    public Task<Result> GetUserProfile(string userId);
    public Task<Result> GetUserById(string userId);
    public Task<Result> SeedDb();
}