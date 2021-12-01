using Jobland.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Jobland.Endpoints;

[ApiController]
[JwtBearerAuthorize]
[Route("")]
public class ApiEndpointBase : ControllerBase
{
    public string CurrentUserId => HttpContext.User.Claims.First(c => c.Type == "Id").Value;
}
