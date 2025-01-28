using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.User
{
	public class SeedDbCommand : IRequest<Result>
	{
	}

	public class SeedDbCommandHandler : IRequestHandler<SeedDbCommand, Result>
	{
		private readonly IUserService _userService;

		public SeedDbCommandHandler(IUserService userService)
		{
			_userService = userService;
		}

		public async Task<Result> Handle(SeedDbCommand request, CancellationToken cancellationToken)
		{
			return await _userService.SeedDb();
		}
	}
}
