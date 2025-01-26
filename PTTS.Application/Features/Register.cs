using System.ComponentModel.DataAnnotations;
using MediatR;
using PTTS.Core.Domain.UserAggregate.Interfaces;
using PTTS.Core.Shared;

namespace PTTS.Application.Features.User
{
    public class RegisterUserFeature : IRequest<Result>
    {
        [Required]
        [EmailAddress]
        public required string Email { get; set; }

        [Required]
        [MinLength(6)]
        public required string Password { get; set; }

        public required string FirstName { get; set; }
        public required string LastName { get; set; }
    }

    public class RegisterUserFeatureHandler : IRequestHandler<RegisterUserFeature, Result>
    {
        private readonly IUserService _userService;
        public RegisterUserFeatureHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<Result> Handle(RegisterUserFeature request, CancellationToken cancellationToken)
        {
            return await _userService.Register(request.FirstName, request.LastName, request.Email, request.Password);
        }
    }
}