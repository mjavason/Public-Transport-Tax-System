using MediatR;
using PTTS.Core.Domain.Common;
using PTTS.Core.Domain.UserAggregate.DTOs;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Commands.User
{
	public class UpdateUserProfileCommand : IRequest<Result>
	{
		public required string UserId { get; set; }
		public required UpdateUserDto Update { get; set; }
}

public class UpdateUserProfileCommandHandler : IRequestHandler<UpdateUserProfileCommand, Result>
{
	private readonly IUserRepository _userRepository;
	private readonly IUnitOfWork _unitOfWork;

	public UpdateUserProfileCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
	{
		_userRepository = userRepository;
		_unitOfWork = unitOfWork;
	}

	public async Task<Result> Handle(UpdateUserProfileCommand request, CancellationToken cancellationToken)
	{
		var userToUpdate = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
		if (userToUpdate == null)
			return Result.NotFound(new List<string> { "User not found." });

		userToUpdate.Update(request.Update);
		_userRepository.Update(userToUpdate, cancellationToken);
		await _unitOfWork.SaveChangesAsync(cancellationToken);

		return Result.Success();
	}
}
}
