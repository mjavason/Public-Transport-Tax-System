using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.User
{
    public class DeleteRoleCommand : IRequest<Result>
    {
        public required string RoleName { get; set; }
    }

    public class DeleteRoleCommandHandler : IRequestHandler<DeleteRoleCommand, Result>
    {
        private readonly IUserService _userService;

        public DeleteRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
        {
            return await _userService.DeleteRole(request.RoleName);
        }
    }
}