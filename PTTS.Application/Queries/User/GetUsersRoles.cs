using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.User
{
    public class GetUsersRolesQuery : IRequest<Result>
    {
        public required string UserId { get; set; }
    }

    public class GetUsersRolesQueryHandler : IRequestHandler<GetUsersRolesQuery, Result>
    {
        private readonly IUserService _userService;

        public GetUsersRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(GetUsersRolesQuery request, CancellationToken cancellationToken)
        {
            var roles = await _userService.GetUserRoles(request.UserId);
            return Result.Success(roles);
        }
    }
}