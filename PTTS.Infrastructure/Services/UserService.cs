using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;
using PTTS.Infrastructure.Credentials;

namespace ShopAllocationPortal.Infrastructure.Services;

public class UserService : IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;
    private readonly JwtSettings _jwtSettings;

    public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task<Result> Register(string firstName, string lastName, string email, string password)
    {
        var user = User.Create(firstName, lastName, email);
        user.UserName = email;
        var result = await _userManager.CreateAsync(user, password);
        if (!result.Succeeded) return Result.BadRequest(result.Errors.Select(e => e.Description).ToList());

        // Send confirmation email
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        return Result.Success(token);
    }

    public async Task<Result> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return Result.NotFound(["User not found"]);

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded) return Result.BadRequest(["Email confirmation failed"]);

        return Result.Success();
    }

    public async Task<Result> Login(string email, string password)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
            return Result.Unauthorized(["Invalid email or password"]);

        var result = await _userManager.CheckPasswordAsync(user, password);
        if (!result)
            return Result.Unauthorized(["Invalid email or password"]);

        var token = GenerateJwtToken(user);
        return Result.Success(token);
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

}