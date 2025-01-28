using MediatR;
using PTTS.Core.Domain.UserAggregate.DTOs;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Features.Login
{
	public class LoginFeature : IRequest<Result<AuthResponse?>>
	{
		public required string Email { get; set; }
		public required string Password { get; set; }
	}

	public class LoginFeatureHandler : IRequestHandler<LoginFeature, Result<AuthResponse?>>
	{
		private readonly IUserService _userService;
		public LoginFeatureHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<Result<AuthResponse?>> Handle(LoginFeature request, CancellationToken cancellationToken)
		{
			var result = await _userService.Login(request.Email, request.Password);
			return result;
		}
	}
}
