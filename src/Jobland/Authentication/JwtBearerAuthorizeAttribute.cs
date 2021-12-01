using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace Jobland.Authentication;

public class JwtBearerAuthorizeAttribute : AuthorizeAttribute
{
    public JwtBearerAuthorizeAttribute()
    {
        AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme;
    }
}
