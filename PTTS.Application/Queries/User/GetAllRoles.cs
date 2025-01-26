using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.User
{
    public class GetAllRolesQuery : IRequest<Result>
    {
    }

    public class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, Result>
    {
        private readonly IUserService _userService;

        public GetAllRolesQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
        {
            return await _userService.GetRoles();
        }
    }
}