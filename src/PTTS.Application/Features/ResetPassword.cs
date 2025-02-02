using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Features.ResetPassword
{
	public class ResetPasswordFeature : IRequest<Result>
	{
		public required string UserId { get; set; }
		public required string NewPassword { get; set; }
		public required string Token { get; set; }
	}

	public class ResetPasswordFeatureHandler : IRequestHandler<ResetPasswordFeature, Result>
	{
		private readonly IUserService _userService;
		public ResetPasswordFeatureHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<Result> Handle(ResetPasswordFeature request, CancellationToken cancellationToken)
		{
			return await _userService.ResetPassword(request.UserId, request.Token, request.NewPassword);
		}
	}
}
