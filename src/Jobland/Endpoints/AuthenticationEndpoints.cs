using AutoMapper;
using Jobland.Authentication;
using Microsoft.AspNetCore.Identity;

namespace Jobland.Endpoints;

public static class AuthenticationEndpoints
{
    private const string RegisterRoot = "/register";
    private const string LoginRoot = "/login";

    public static WebApplication AddAuthenticationEndpoints(this WebApplication app) =>
        app
            .AddRegistration()
            .AddLogin();

    private static WebApplication AddRegistration(this WebApplication app)
    {
        app.MapPost(RegisterRoot,
            async (HttpContext http, UserManager<User> userManager, IMapper mapper, IJwtTokenService tokenService) =>
            {
                var dto = await http.SafeGetJsonAsync<RegistrationRequest>();
                if (dto == null)
                    return Results.BadRequest(AuthResults.InvalidInput());

                if (await userManager.FindByEmailAsync(dto.Email) != null)
                    return Results.BadRequest(AuthResults.AccountAlreadyExists());

                var user = mapper.Map<RegistrationRequest, User>(dto).SetUsername();
                var created = await userManager.CreateAsync(user, dto.Password);
                return !created.Succeeded
                    ? Results.UnprocessableEntity(AuthResult.Fail(created.Errors.Select(e => e.Description).ToList()))
                    : Results.Created(RegisterRoot, AuthResult.Ok(tokenService.GenerateJwtToken(user)));
            });
        return app;
    }

    private static WebApplication AddLogin(this WebApplication app)
    {
        app.MapPost(LoginRoot, async (HttpContext http, UserManager<User> userManager, IJwtTokenService tokenService) =>
        {
            var dto = await http.SafeGetJsonAsync<LoginRequest>();
            if (dto == null)
                return Results.BadRequest(AuthResults.InvalidInput());

            var user = await userManager.FindByEmailAsync(dto.Email);
            if (user == null)
                return Results.BadRequest(AuthResults.EmailNotFound());
            var valid = await userManager.CheckPasswordAsync(user, dto.Password);
            if (!valid)
                return Results.BadRequest(AuthResults.IncorrectPassword());

            return Results.Ok(AuthResult.Ok(tokenService.GenerateJwtToken(user)));
        });
        return app;
    }
}
