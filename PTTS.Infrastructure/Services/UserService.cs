using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.DTOs;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;
using PTTS.Infrastructure.Credentials;

namespace ShopAllocationPortal.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly JwtSettings _jwtSettings;
    private readonly IEmailSender _emailSender;

    public UserService(UserManager<User> userManager, IEmailSender emailSender, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _jwtSettings = jwtSettings.Value;
        _emailSender = emailSender;
    }

    public async Task<Result> Register(string firstName, string lastName, string email, string password)
    {
        var user = User.Create(firstName, lastName, email);
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) return Result.BadRequest(result.Errors.Select(e => e.Description).ToList());

        // Send confirmation email
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        // Send confirmation email
        // var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = user.Id, token }, Request.Scheme);
        var confirmationLink = $"http://localhost:5085/api/Auth/confirm-email?userId={user.Id}&token={token}";
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

        var token = GenerateJwtToken(user);
        var authResponse = new AuthResponse
        {
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Token = token
        };

        return Result.Success<AuthResponse?>(authResponse);
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new[]
        {
                new Claim(JwtRegisteredClaimNames.Email,user.Email ?? String.Empty),
                new Claim(JwtRegisteredClaimNames.Sub,user.Id),
                new Claim("uid", user.Id),
                new Claim("email", user.Email ?? String.Empty),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
    };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: "YourIssuer",
            audience: "YourAudience",
            claims: claims,
            expires: DateTime.Now.AddHours(1),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
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

}