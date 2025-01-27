using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.User
{
    public class GetUsersInRoleQuery : IRequest<Result>
    {
        public string Role { get; set; }

        public GetUsersInRoleQuery(string role)
        {
            Role = role;
        }
    }

    public class GetUsersInRoleQueryHandler : IRequestHandler<GetUsersInRoleQuery, Result>
    {
        private readonly IUserService _userService;

        public GetUsersInRoleQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(GetUsersInRoleQuery request, CancellationToken cancellationToken)
        {
            var users = await _userService.GetUsersInRole(request.Role);
            return Result.Success(users);
        }
    }
}