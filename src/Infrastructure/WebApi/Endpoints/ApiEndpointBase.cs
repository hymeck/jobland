using Jobland.Infrastructure.Api.Web.Identity;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Jobland.Infrastructure.Api.Web.Endpoints;

[ApiController]
[JwtBearerAuthorize]
[Route("")]
public class ApiEndpointBase : ControllerBase
{
    public string CurrentUserId => HttpContext.User.Claims.First(c => c.Type == "Id").Value;
    private ISender? _sender;

    protected ISender Sender => _sender ??= HttpContext.RequestServices.GetRequiredService<ISender>();
}
