using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Features
{
    public class RemoveUserFromRoleCommand : IRequest<Result>
    {
        public string UserId { get; set; }
        public string Role { get; set; }

        public RemoveUserFromRoleCommand(string userId, string role)
        {
            UserId = userId;
            Role = role;
        }
    }

    public class RemoveUserFromRoleCommandHandler : IRequestHandler<RemoveUserFromRoleCommand, Result>
    {
        private readonly IUserService _userService;

        public RemoveUserFromRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(RemoveUserFromRoleCommand request, CancellationToken cancellationToken)
        {
            return await _userService.RemoveUserFromRole(request.UserId, request.Role);
        }
    }
}