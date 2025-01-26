using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.User
{
    public class UpdateRoleCommand : IRequest<Result>
    {
        public required string OldRoleName { get; set; }
        public required string NewRoleName { get; set; }
    }

    public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, Result>
    {
        private readonly IUserService _userService;

        public UpdateRoleCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            return await _userService.UpdateRole(request.OldRoleName, request.NewRoleName);
        }
    }
}