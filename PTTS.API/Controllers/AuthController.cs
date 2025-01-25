using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTTS.API.Filters.Model;
using PTTS.Application.Features.ForgotPassword;
using PTTS.Application.Features.Login;
using PTTS.Application.Features.ResetPassword;
using PTTS.Application.Features.User;
using PTTS.Core.Domain.UserAggregate.DTOs;

namespace PTTS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ApiBaseController
{
    public AuthController(IMediator mediator) : base(mediator) { }

    [HttpPost("register")]
    [EndpointSummary("Register a new user")]
    [EndpointDescription("Registers a new user with the provided details.")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Register([FromBody] RegisterUserFeature feature)
    {
        var result = await _mediator.Send(feature);
        return result.IsSuccess ? NoContent() : GetActionResult(result);
    }

    [HttpGet("confirm-email")]
    [EndpointSummary("Confirm email address")]
    [EndpointDescription("Confirms the email address of the user.")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ConfirmEmail([FromQuery] ConfirmEmailFeature feature)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _mediator.Send(feature);
        return GetActionResult(result, "Email confirmed successfully");
    }

    [HttpPost("login")]
    [EndpointSummary("User login")]
    [EndpointDescription("Logs in the user with the provided credentials.")]
    [ProducesResponseType(typeof(SuccessResponse<AuthResponse?>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Login([FromBody] LoginFeature feature)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _mediator.Send(feature);
        return GetActionResult(result, "Login successful");
    }

    [HttpPost("forgot-password")]
    [EndpointSummary("Forgot password")]
    [EndpointDescription("Sends a password reset link to the user's email address.")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordFeature feature)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _mediator.Send(feature);
        return GetActionResult(result, "Password reset link sent to email");
    }

    [HttpPost("reset-password")]
    [EndpointSummary("Reset password")]
    [EndpointDescription("Resets the user's password using the provided token and new password.")]
    [ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordFeature feature)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var result = await _mediator.Send(feature);
        return GetActionResult(result, "Password has been reset successfully");
    }
}
