using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Features
{
	public class AddUserToRoleCommand : IRequest<Result>
	{
		public required string UserId { get; set; }
		public required string Role { get; set; }
	}

	public class AddUserToRoleCommandHandler : IRequestHandler<AddUserToRoleCommand, Result>
	{
		private readonly IUserService _userService;

		public AddUserToRoleCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<Result> Handle(AddUserToRoleCommand request, CancellationToken cancellationToken)
		{
			return await _userService.AddUserToRole(request.UserId, request.Role);
		}
	}
}
