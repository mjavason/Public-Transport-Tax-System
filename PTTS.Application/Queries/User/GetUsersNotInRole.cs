using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.User
{
    public class GetUsersNotInRoleQuery : IRequest<Result>
    {
        public string Role { get; set; }

        public GetUsersNotInRoleQuery(string role)
        {
            Role = role;
        }
    }

    public class GetUsersNotInRoleQueryHandler : IRequestHandler<GetUsersNotInRoleQuery, Result>
    {
        private readonly IUserService _userService;

        public GetUsersNotInRoleQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(GetUsersNotInRoleQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetUsersNotInRole(request.Role);
            return Result.Success(users);
        }
    }
}