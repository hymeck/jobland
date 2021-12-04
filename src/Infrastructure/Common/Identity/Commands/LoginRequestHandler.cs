using Jobland.Infrastructure.Common.Identity.Abstractions;
using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Jobland.Infrastructure.Common.Identity.Commands;

public sealed class LoginRequestHandler : IRequestHandler<LoginRequest, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _tokenService;

    public LoginRequestHandler(UserManager<User> userManager, IJwtTokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }

    public async Task<AuthResponse> Handle(LoginRequest request, CancellationToken cancellationToken) =>
        request switch
        {
            null => AuthResponses.InvalidInput(),
            _ => await HandleValidRequestAsync(request, cancellationToken)
        };

    private async Task<AuthResponse> HandleValidRequestAsync(LoginRequest request, CancellationToken token)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);
        return user == null
            ? AuthResponses.EmailNotFound()
            : await HandleExistingUserAsync(user, request.Password, token);
    }

    private async Task<AuthResponse> HandleExistingUserAsync(User user, string password, CancellationToken cancellationToken) =>
        !await _userManager.CheckPasswordAsync(user, password)
            ? AuthResponses.IncorrectPassword()
            : AuthResponse.Ok(_tokenService.GenerateJwtToken(user));
}
