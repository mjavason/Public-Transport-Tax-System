using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTTS.Application.Commands.User;
using PTTS.Application.Queries.User;
using PTTS.Core.Domain.UserAggregate.DTOs;

namespace PTTS.API.Controllers
{
	[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public class UserController : ApiBaseController
	{
		public UserController(IMediator mediator) : base(mediator)
		{
		}

		[HttpGet()]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetMyProfile()
		{
			string userId = GetUserId();
			var query = new GetUserByIdQuery { UserId = userId };
			var result = await _mediator.Send(query);

			return GetActionResult(result, "User profile retrieved successfully");
		}

		[HttpGet("{UserId}")]
		[ProducesResponseType(StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status404NotFound)]
		public async Task<IActionResult> GetUserProfile([FromRoute] GetUserByIdQuery query)
		{
			var result = await _mediator.Send(query);
			return GetActionResult(result, "User profile retrieved successfully");
		}

		[HttpPut()]
		public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserDto model)
		{
			string id = GetUserId();
			var command = new UpdateUserProfileCommand { Update = model, UserId = id };
			var result = await _mediator.Send(command);

			return result.IsSuccess ? NoContent() : GetActionResult(result);
		}

	}
}
