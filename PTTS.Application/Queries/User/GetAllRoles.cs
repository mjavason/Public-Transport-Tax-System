using MediatR;
using Microsoft.AspNetCore.Identity;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.User
{
	public class GetAllRolesQuery : IRequest<Result<IList<IdentityRole>>>
	{
	}

	public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result<IList<IdentityRole>>>
	{
		private readonly IUserService _userService;

		public GetAllRolesQueryHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<Result<IList<IdentityRole>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
		{
			var roles = await _userService.GetRoles();
			return Result.Success(roles);
		}
	}
}
