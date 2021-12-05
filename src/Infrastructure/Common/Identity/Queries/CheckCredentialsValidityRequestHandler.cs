using AutoMapper;
using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Jobland.Infrastructure.Common.Identity.Dtos.Responses;
using LanguageExt;
using LanguageExt.Common;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Jobland.Infrastructure.Common.Identity.Queries;

public sealed class CheckCredentialsValidityRequestHandler : IRequestHandler<CheckCredentialsValidityRequest, Option<CheckCredentialsValidityResponse>>
{
    private readonly UserManager<User> _manager;
    private readonly IMapper _mapper; 

    public CheckCredentialsValidityRequestHandler(UserManager<User> manager, IMapper mapper)
    {
        _manager = manager;
        _mapper = mapper;
    }

    public async Task<Option<CheckCredentialsValidityResponse>> Handle(CheckCredentialsValidityRequest request, CancellationToken cancellationToken)
    {
        var user = _mapper.Map<CheckCredentialsValidityRequest, User>(request);
        async Task<Result<IdentityResult>> TryResult() => await _manager.CreateAsync(user.SetUsername(), request.Password);
        var result = await TryResult();
        var response = result.Match(r => new CheckCredentialsValidityResponse(r.Succeeded, r.Errors.Select(x => x.Description).ToList()), _ => null!);
        return response;
    }
}
