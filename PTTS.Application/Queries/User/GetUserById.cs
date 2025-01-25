using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Queries.User
{
    public class GetUserByIdQuery : IRequest<Result<Core.Domain.UserAggregate.User>>
    {
        public Guid UserId { get; set; }
    }

    public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, Result<Core.Domain.UserAggregate.User>>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Result<Core.Domain.UserAggregate.User>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            if (user == null) return Result.NotFound<Core.Domain.UserAggregate.User>(["User not found"]);

            return Result.Success<Core.Domain.UserAggregate.User>(user);
        }
    }
}