using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Jobland.Infrastructure.Api.Web.Identity;

public class JwtBearerAuthorizeAttribute : AuthorizeAttribute
{
    public JwtBearerAuthorizeAttribute() => AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
}
