// using System.IdentityModel.Tokens.Jwt;
// using System.Security.Claims;
// using System.Text;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.Extensions.Options;
// using Microsoft.IdentityModel.Tokens;
// using PTTS.Core.Domain.UserAggregate;
// using PTTS.Core.Domain.UserAggregate.DTOs;
// using PTTS.Core.Domain.UserAggregate.Interfaces;
// using PTTS.Core.Shared;
// using PTTS.Infrastructure.Credentials;

// namespace ShopAllocationPortal.Infrastructure.Services;

// public class UserService : IUserService
// {
//     private readonly UserManager<User> _userManager;
//     private readonly SignInManager<User> _signInManager;
//     private readonly JwtSettings _jwtSettings;

//     public UserService(UserManager<User> userManager, SignInManager<User> signInManager, IOptions<JwtSettings> jwtSettings)
//     {
//         _userManager = userManager;
//         _signInManager = signInManager;
//         _jwtSettings = jwtSettings.Value;
//     }

//     public Task<Result<AuthResponse?>> AuthenticateUserAsync(string email, string password)
//     {
//         throw new NotImplementedException();
//     }

//     // public async Task<Result<AuthResponse?>> AuthenticateUserAsync(string email, string password)
//     // {
//     //     var user = await _userManager.FindByEmailAsync(email);
//     //     if (user == null)
//     //     {
//     //         return Result.Unauthorized<AuthResponse?>(["Invalid credentials, email or password is incorrect"]);
//     //     }

//     //     var result = await _signInManager.CheckPasswordSignInAsync(user, password, true);

//     //     if (result.IsLockedOut)
//     //     {
//     //         return Result.Unauthorized<AuthResponse?>(["Your account is locked. Please try again later or contact support."]);
//     //     }
//     //     if (result.Succeeded == false)
//     //     {
//     //         return Result.Unauthorized<AuthResponse?>(["Invalid credentials, email or password is incorrect"]);
//     //     }

//     //     var (jwtSecurityToken, userRole) = await GenerateSecurityToken(user);

//     //     var authResponse = new AuthResponse
//     //     {
//     //         Email = user.Email,
//     //         FirstName = user.FirstName,
//     //         LastName = user.LastName,
//     //         Role = userRole,
//     //         Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken)
//     //     };

//     //     return Result.Success<AuthResponse?>(authResponse);
//     // }


//     public async Task<(JwtSecurityToken, string)> GenerateSecurityToken(User user)
//     {
//         var userClaims = await _userManager.GetClaimsAsync(user);
//         var userRoles = await _userManager.GetRolesAsync(user);
//         var userId = user.Id;

//         var roleClaims = userRoles
//             .Select(q => new Claim(ClaimTypes.Role, q))
//             .ToList();
//         var claims = new[]
//             {
//                 new Claim(JwtRegisteredClaimNames.Email,user.Email),
//                 new Claim(JwtRegisteredClaimNames.Sub,user.Id),
//                 new Claim("uid",userId),
//                 new Claim("email",user.Email),
//                 new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
//                 new Claim("role", userRoles.FirstOrDefault() ?? ""),
//             }
//             .Union(roleClaims)
//             .Union(userClaims);

//         var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
//         var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

//         var jwtSecurityToken = new JwtSecurityToken(
//             issuer: _jwtSettings.Issuer,
//             audience: _jwtSettings.Audience,
//             claims: claims,
//             expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
//             signingCredentials: signingCredentials
//         );

//         return (jwtSecurityToken, userRoles.FirstOrDefault() ?? string.Empty);
//     }

// }