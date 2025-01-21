using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using PTTS.Core.Domain.UserAggregate;
using PTTS.Core.Domain.UserAggregate.DTOs;

namespace PTTS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly IEmailSender _emailSender;

    public AuthController(UserManager<User> userManager, IEmailSender emailSender)
    {
        _userManager = userManager;
        _emailSender = emailSender;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = Core.Domain.UserAggregate.User.Create(registerDto.FirstName, registerDto.LastName, registerDto.Email);
        user.UserName = registerDto.Email;
        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);

        // Send confirmation email
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        var confirmationLink = Url.Action(nameof(ConfirmEmail), "Auth", new { userId = user.Id, token }, Request.Scheme);
        await _emailSender.SendEmailAsync(registerDto.Email, "Confirm your email", $"Click the link to confirm your email: {confirmationLink}");

        return Ok("User registered successfully. Please check your email to confirm your account.");
    }

    [HttpGet("confirm-email")]
    public async Task<IActionResult> ConfirmEmail(string userId, string token)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user == null) return NotFound("User not found.");

        var result = await _userManager.ConfirmEmailAsync(user, token);
        if (!result.Succeeded) return BadRequest("Email confirmation failed.");

        return Ok("Email confirmed successfully.");
    }
}

