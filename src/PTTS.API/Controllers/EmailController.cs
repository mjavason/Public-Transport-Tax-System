using Microsoft.AspNetCore.Mvc;
using PTTS.Core.Domain.UserAggregate.DTOs;

[Route("api/email")]
[ApiController]
public class EmailController : ControllerBase
{
	private readonly RazorViewToStringRenderer _razorRenderer;

	public EmailController(RazorViewToStringRenderer razorRenderer)
	{
		_razorRenderer = razorRenderer;
	}

	[HttpGet("test-confirm-email")]
	public async Task<IActionResult> TestConfirmEmail()
	{
		var model = new ConfirmEmailDTO
		{
			Name = "John Doe",
			ConfirmationLink = "https://example.com/confirm?token=abc123"
		};

		var emailBody = await _razorRenderer.RenderViewToStringAsync("Emails/ConfirmEmail", model);
		return Content(emailBody, "text/html");
	}
}
