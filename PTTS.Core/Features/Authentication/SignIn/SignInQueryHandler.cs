using MediatR;
using Microsoft.Extensions.Logging;
using ShopAllocationPortal.Core.Domain.UserAggregate;
using ShopAllocationPortal.Core.Domain.UserAggregate.DTOs;
using ShopAllocationPortal.Core.Shared;

namespace ShopAllocationPortal.Core.Features.Authentication.SignIn;

public class SignInQueryHandler : IRequestHandler<SignInQuery, Result<AuthResponse?>>
{
    private readonly IUserService _userService;
    private readonly ILogger<SignInQueryHandler> _logger;

    public SignInQueryHandler(IUserService userService, ILogger<SignInQueryHandler> logger)
    {
        _userService = userService;
        _logger = logger;
    }
    
    public async Task<Result<AuthResponse?>> Handle(SignInQuery request, CancellationToken cancellationToken)
    {
       var result = await _userService.AuthenticateUserAsync(request.Email, request.Password);
       return result;
    }
}