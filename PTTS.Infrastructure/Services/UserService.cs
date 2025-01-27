using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.DTOs;
using PTTS.Core.Domain.UserAggregate.Enums;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;
using PTTS.Infrastructure.Credentials;

namespace PTTS.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IEmailSender _emailSender;

    public UserService(RoleManager<IdentityRole> roleManager, UserManager<User> userManager, IEmailSender emailSender, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _emailSender = emailSender;
        _roleManager = roleManager;
    }

    public async Task<Result> Register(string firstName, string lastName, string email, string password)
    {
        var user = User.Create(firstName, lastName, email);
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) return Result.BadRequest(result.Errors.Select(e => e.Description).ToList());

        // Send confirmation email
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var encodedToken = Uri.EscapeDataString(token); // Ensure the token is URL-encoded

        // Send confirmation email
        // var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = user.Id, token }, Request.Scheme);
        var confirmationLink = $"http://localhost:5085/api/Auth/confirm-email?userId={user.Id}&token={encodedToken}";
        await _emailSender.SendEmailAsync(email, "Confirm your email", $"Click the link to confirm your email: {confirmationLink}");

        return Result.Success();
    }

    public async Task<Result> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.NotFound(["User not found"]);

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded) return Result.BadRequest(["Email confirmation failed"]);

        return Result.Success();
    }

    public async Task<Result<AuthResponse?>> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Result.Unauthorized<AuthResponse?>(["Invalid email or password"]);

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
            return Result.Unauthorized<AuthResponse?>(["Invalid email or password"]);

        var (token, userRole) = await GenerateJwtToken(user);

        var authResponse = new AuthResponse
        {
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = token,
            Role = userRole
        };

        return Result.Success<AuthResponse?>(authResponse);
    }

    private async Task<(string, string)> GenerateJwtToken(User user)
    {
        var userClaims = await _userManager.GetClaimsAsync(user);
        var userRoles = await _userManager.GetRolesAsync(user);
        var userRole = userRoles.FirstOrDefault();

        var roleClaims = userRoles
                    .Select(q => new Claim(ClaimTypes.Role, q))
                    .ToList();
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Email,user.Email ?? String.Empty),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim("uid", user.Id),
                new Claim("email", user.Email ?? String.Empty),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                 new Claim("role", userRoles.FirstOrDefault() ?? "")
        }
            .Union(roleClaims)
            .Union(userClaims);

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var jwtSecurityToken = new JwtSecurityToken(
          issuer: _jwtSettings.Issuer,
          audience: _jwtSettings.Audience,
          claims: claims,
          expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
          signingCredentials: creds
      );

        return (new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken), userRoles.FirstOrDefault() ?? string.Empty);
    }

    public async Task<Result> ForgotPassword(string email)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null) return Result.NotFound(["User not found"]);

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);
        var resetLink = $"http://localhost:5085/api/Auth/reset-password?userId={user.Id}&token={token}";
        await _emailSender.SendEmailAsync(email, "Reset Password", $"Click the link to reset your password: {resetLink}");

        return Result.Success();
    }

    public async Task<Result> ResetPassword(string userId, string token, string password)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.NotFound(new List<string> { "User not found" });

        var result = await _userManager.ResetPasswordAsync(user, token, password);
        if (!result.Succeeded) return Result.BadRequest(result.Errors.Select(e => e.Description).ToList());

        return Result.Success();
    }

    public async Task<Result> CreateRole(string roleName)
    {
        if (string.IsNullOrEmpty(roleName))
            return Result.BadRequest(["Role name cannot be empty."]);

        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (roleExists)
            return Result.BadRequest(["Role already exists."]);

        var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
        if (result.Succeeded)
            return Result.Success();

        return Result.BadRequest(result.Errors.Select(e => e.Description).ToList());
    }

    public async Task<Result> AddUserToRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.NotFound(new List<string> { "User not found" });

        var roleExists = await _roleManager.RoleExistsAsync(roleName);
        if (!roleExists) return Result.BadRequest(new List<string> { "Role does not exist" });

        var result = await _userManager.AddToRoleAsync(user, roleName);
        if (!result.Succeeded) return Result.BadRequest(result.Errors.Select(e => e.Description).ToList());

        return Result.Success();
    }

    public async Task<Result> RemoveUserFromRole(string userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.NotFound(new List<string> { "User not found" });

        var result = await _userManager.RemoveFromRoleAsync(user, roleName);
        if (!result.Succeeded) return Result.BadRequest(result.Errors.Select(e => e.Description).ToList());

        return Result.Success();
    }

    public async Task<Result> DeleteRole(string roleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null) return Result.NotFound(new List<string> { "Role not found" });

        var result = await _roleManager.DeleteAsync(role);
        if (!result.Succeeded) return Result.BadRequest(result.Errors.Select(e => e.Description).ToList());

        return Result.Success();
    }

    public async Task<Result> UpdateRole(string roleName, string newRoleName)
    {
        var role = await _roleManager.FindByNameAsync(roleName);
        if (role == null) return Result.NotFound(new List<string> { "Role not found" });

        role.Name = newRoleName;
        var result = await _roleManager.UpdateAsync(role);
        if (!result.Succeeded) return Result.BadRequest(result.Errors.Select(e => e.Description).ToList());

        return Result.Success();
    }

    public async Task<IList<IdentityRole>> GetRoles()
    {
        return await _roleManager.Roles.ToListAsync();
    }

    public async Task<IList<User>> GetUsersInRole(string roleName)
    {
        return await _userManager.GetUsersInRoleAsync(roleName);
    }

    public async Task<IList<User>> GetUsersNotInRole(string roleName)
    {
        var usersInRole = await _userManager.GetUsersInRoleAsync(roleName);
        var allUsers = _userManager.Users.ToList();
        return allUsers.Except(usersInRole).ToList();
    }

    public async Task<IList<string>> GetUserRoles(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user is null) return new List<string>();
        return await _userManager.GetRolesAsync(user);
    }

    public async Task<Result> GetUserProfile(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.NotFound(new List<string> { "User not found" });

        var userProfile = new UserProfileDto(
            user.Id,
            user.FirstName,
            user.LastName,
            user.Email ?? string.Empty,
            user.PhoneNumber ?? string.Empty
        );

        return Result.Success(userProfile);
    }

    public async Task<Result> GetUserById(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return Result.NotFound(new List<string> { "User not found" });

        return Result.Success(user);
    }

    public async Task<Result> SeedDb()
    {
        string role = UserRole.SuperAdmin.ToString();
        if (!await _roleManager.RoleExistsAsync(role)) await _roleManager.CreateAsync(new IdentityRole(role));

        // Seed an admin user
        var adminEmail = "testerzero@gmail.com";
        var adminUser = await _userManager.FindByEmailAsync(adminEmail);
        if (adminUser == null)
        {
            adminUser = User.Create("System", "Admin", adminEmail);
            await _userManager.CreateAsync(adminUser, "Strong@password123");
            await _userManager.AddToRoleAsync(adminUser, "Admin");
        }

        return Result.Success();
    }
}