using Jobland.Infrastructure.Common.Identity.Dtos.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jobland.Infrastructure.Api.Web.Endpoints;

public sealed class IdentityEndpoints : ApiEndpointBase
{
    public const string RegisterRoot = "/register";
    public const string LoginRoot = "/login";
    
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
}
