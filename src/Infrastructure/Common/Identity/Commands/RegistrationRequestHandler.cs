using AutoMapper;
using Jobland.Infrastructure.Common.Identity.Abstractions;
using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Jobland.Infrastructure.Common.Identity.Commands;

public sealed class RegistrationRequestHandler : IRequestHandler<RegistrationRequest, AuthResponse>
{
    private readonly UserManager<User> _userManager;
    private readonly IJwtTokenService _tokenService;
    private readonly IMapper _mapper;

    public RegistrationRequestHandler(UserManager<User> userManager, IJwtTokenService tokenService, IMapper mapper)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _mapper = mapper;
    }

    public async Task<AuthResponse> Handle(RegistrationRequest request, CancellationToken cancellationToken) =>
        request switch
        {
            null => AuthResponses.InvalidInput(),
            _ => await HandleValidRequestAsync(request, cancellationToken)
        };

    private async Task<AuthResponse> HandleValidRequestAsync(RegistrationRequest request, CancellationToken token) =>
        await _userManager.FindByEmailAsync(request.Email) != null
            ? AuthResponses.AccountAlreadyExists()
            : await HandleNewUserAsync(request, token);

    private async Task<AuthResponse> HandleNewUserAsync(RegistrationRequest request, CancellationToken token)
    {
        var user = _mapper.Map<RegistrationRequest, User>(request).SetUsername();
        var created = await _userManager.CreateAsync(user, request.Password);
        return !created.Succeeded
            ? AuthResponse.Fail(created.Errors.Select(e => e.Description).ToList())
            : AuthResponse.Ok(_tokenService.GenerateJwtToken(user), user.Id);
    }
}
