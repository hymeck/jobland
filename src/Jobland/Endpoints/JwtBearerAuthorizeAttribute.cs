using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Jobland.Endpoints;

public class JwtBearerAuthorizeAttribute : AuthorizeAttribute
{
    public JwtBearerAuthorizeAttribute()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}
