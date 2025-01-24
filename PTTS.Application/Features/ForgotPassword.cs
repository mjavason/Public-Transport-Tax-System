using System.Runtime.CompilerServices;
using MediatR;
using PTTS.Core.Domain.UserAggregate.DTOs;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Features.ForgotPassword
{
    public class ForgotPasswordFeature : IRequest<Result>
    {
        public required string Email { get; set; }
    }

    public class ForgotPasswordFeatureHandler : IRequestHandler<ForgotPasswordFeature, Result>
    {
        private readonly IUserService _userService;
        public ForgotPasswordFeatureHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(ForgotPasswordFeature request, CancellationToken cancellationToken)
        {
            return await _userService.ForgotPassword(request.Email);
        }
    }
}