using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Features.User
{
    public class ConfirmEmailFeature : IRequest<Result>
    {
        public required string Token { get; set; }
        public required string UserId { get; set; }
    }

    public class ConfirmEmailFeatureHandler : IRequestHandler<ConfirmEmailFeature, Result>
    {
        private readonly IUserService _userService;
        public ConfirmEmailFeatureHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(ConfirmEmailFeature request, CancellationToken cancellationToken)
        {
            var result = await _userService.ConfirmEmail(request.UserId, request.Token);
            return result;
        }
    }
}
