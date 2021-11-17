using AutoMapper;
using Jobland.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Jobland.Endpoints;

public static class AuthenticationEndpoints
{
    private const string RegisterRoot = "/register";
    public static WebApplication AddAuthenticationEndpoints(this WebApplication app)
    {
        app.MapPost(RegisterRoot, async (HttpContext http, UserManager<User> userManager, IMapper mapper, IJwtTokenService tokenService) =>
        {
            var dto = await http.Request.ReadFromJsonAsync<RegistrationRequest>();
            if (dto == null)
                return Results.BadRequest();
            
            if (await userManager.FindByEmailAsync(dto.Email) != null)
                return Results.BadRequest(AuthResult.Fail("account with specified email already exists"));

            var user = mapper.Map<RegistrationRequest, User>(dto).SetUsername();
            var created = await userManager.CreateAsync(user, dto.Password);
            return !created.Succeeded 
                ? Results.UnprocessableEntity(AuthResult.Fail(created.Errors.Select(e => e.Description).ToList())) 
                : Results.Created(RegisterRoot, AuthResult.Ok(tokenService.GenerateJwtToken(user)));
        });
        return app;
    }
}
