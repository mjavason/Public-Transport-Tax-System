using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Features.User
{
    public class ConfirmEmailFeature : IRequest<Result>
    {
        public string Token { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
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
            return await _userService.ConfirmEmail(request.UserId, request.Token);
        }
    }
}
