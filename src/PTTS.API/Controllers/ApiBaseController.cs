using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PTTS.API.Filters.Model;
using PTTS.Core.Shared;


namespace PTTS.API.Controllers;

[ApiController]
[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
public abstract class ApiBaseController : ControllerBase
{
	protected readonly IMediator _mediator;

	protected ApiBaseController(IMediator mediator)
	{
		_mediator = mediator;
	}

	protected string GetUserId()
	{
		return User.FindFirstValue(ClaimTypes.NameIdentifier)
			?? throw new UnauthorizedAccessException("User ID not found in claims.");
	}

	protected static IActionResult GetActionResult<T>(Result<T> result, string successMessage = "")
	{
		return result.IsSuccess ? new OkObjectResult(result.GetSuccessResult(successMessage)) : ErrorActionResult(result.ErrorType, result.Errors);
	}

	protected static IActionResult GetActionResult<T>(Result<T> result, string createdAtAction, string successMessage)
	{
		return result.IsSuccess ? new CreatedResult(createdAtAction, result.GetSuccessResult(successMessage)) : ErrorActionResult(result.ErrorType, result.Errors);
	}

	protected static IActionResult GetActionResult(Result result, string successMessage = "")
	{
		return result.IsSuccess ? new OkObjectResult(Result.GetSuccessResult(successMessage)) : ErrorActionResult(result.ErrorType, result.Errors);
	}

	protected static IActionResult GetActionResult(Result result, string createdAtAction, string successMessage)
	{
		return result.IsSuccess ? new CreatedResult(createdAtAction, Result.GetSuccessResult(successMessage)) : ErrorActionResult(result.ErrorType, result.Errors);
	}

	private static IActionResult ErrorActionResult(ErrorType errorType, List<string> errors)
	{
		if (errorType == ErrorType.InternalServerError)
		{
			return new StatusCodeResult(StatusCodes.Status500InternalServerError);
		}
		return new ObjectResult(errors) { StatusCode = (int)errorType };
	}
}
