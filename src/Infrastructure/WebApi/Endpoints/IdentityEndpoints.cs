using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobland.Infrastructure.Api.Web.Endpoints;

public sealed class IdentityEndpoints : ApiEndpointBase
{
    public const string RegisterRoot = "/register";
    public const string LoginRoot = "/login";
    public const string VerifyCredentialsRoot = "/verify";
    
    [AllowAnonymous]
    [HttpPost(RegisterRoot)]
    public async Task<IActionResult> Register([FromBody] RegistrationRequest request, CancellationToken token)
    {
        var response = await Sender.Send(request, token);
        return response switch
        {
            // todo: what about UnprocessableEntity?
            { Token.Length: > 0 } => Created(RegisterRoot, response),
            _ => BadRequest(response)
        };
    }

    [AllowAnonymous]
    [HttpPost(LoginRoot)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken token)
    {
        var response = await Sender.Send(request, token);
        return response switch
        {
            // todo: what about UnprocessableEntity 2?
            { Token.Length: > 0 } => Ok(response),
            _ => BadRequest(response)
        };
    }

    [AllowAnonymous]
    [HttpGet(VerifyCredentialsRoot)]
    public async Task<IActionResult> VerifyCredentials([FromBody] CheckCredentialsValidityRequest request,
        CancellationToken token) =>
        (await Sender.Send(request, token)).Match(r => (IActionResult)Ok(r), BadRequest);

}
