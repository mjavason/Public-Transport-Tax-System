using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PTTS.API.Filters.Model;
using PTTS.Application.Commands.User;
using PTTS.Application.Features;
using PTTS.Application.Queries.User;

namespace PTTS.API.Controllers;

[Authorize(Roles = "Admin")]
[ApiController]
[Route("api/[controller]")]
public class RoleController : ApiBaseController
{
	public RoleController(IMediator mediator) : base(mediator) { }

	[HttpPost("create")]
	[EndpointSummary("Create a new role")]
	[EndpointDescription("Creates a new role with the provided details.")]
	[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand command)
	{
		var result = await _mediator.Send(command);
		return result.IsSuccess ? NoContent() : GetActionResult(result);
	}

	[HttpPost("user")]
	[EndpointSummary("Add user to role")]
	[EndpointDescription("Adds the specified user to the specified role.")]
	[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> AddUserToRole([FromBody] AddUserToRoleCommand command)
	{
		var result = await _mediator.Send(command);
		return result.IsSuccess ? NoContent() : GetActionResult(result);
	}

	[HttpGet()]
	[EndpointSummary("Get all roles")]
	[EndpointDescription("Retrieves the roles")]
	[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status200OK)]
	public async Task<IActionResult> GetAllRoles()
	{
		var query = new GetAllRolesQuery();
		var result = await _mediator.Send(query);
		return GetActionResult(result, "All roles retrieved successfully");
	}

	[HttpPut()]
	[EndpointSummary("Update role")]
	[EndpointDescription("Updates the role with the specified name.")]
	[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status400BadRequest)]
	public async Task<IActionResult> UpdateRole([FromBody] UpdateRoleCommand command)
	{
		var result = await _mediator.Send(command);
		return result.IsSuccess ? NoContent() : GetActionResult(result);
	}

	[HttpDelete("user")]
	[EndpointSummary("Delete user from role")]
	[EndpointDescription("Deletes the user from the specific role")]
	[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteUserFromRole([FromBody] RemoveUserFromRoleCommand command)
	{
		var result = await _mediator.Send(command);
		return result.IsSuccess ? NoContent() : GetActionResult(result);
	}

	[HttpDelete("{RoleName}")]
	[EndpointSummary("Delete role")]
	[EndpointDescription("Deletes the role with the specified ID.")]
	[ProducesResponseType(typeof(SuccessResponse), StatusCodes.Status204NoContent)]
	[ProducesResponseType(typeof(ErrorResponse), StatusCodes.Status404NotFound)]
	public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommand command)
	{
		var result = await _mediator.Send(command);
		return result.IsSuccess ? NoContent() : GetActionResult(result);
	}


}
