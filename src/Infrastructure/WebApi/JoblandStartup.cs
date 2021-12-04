using System.Text;
using Jobland.Application;
using Jobland.Infrastructure.Api.Web.Identity;
using Jobland.Infrastructure.Common;
using Jobland.Infrastructure.Common.Identity.Abstractions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Jobland.Infrastructure.Api.Web;

public class JoblandStartup
{
    private readonly IConfiguration _configuration;
    private readonly IWebHostEnvironment _env;

    public JoblandStartup(IConfiguration configuration, IWebHostEnvironment env)
    {
        _configuration = configuration;
        _env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers();

        services
            .AddScoped<IJwtTokenService, JwtTokenService>()
            .AddScoped<IJwtConfigurationProvider, JwtConfigurationProvider>()
            .AddScoped<IProfileImageSensitiveStorageDataProvider, ProfileImageSensitiveStorageDataProvider>()
            .AddScoped<IProfileImageService, ProfileImageService>();

        var issuer = _configuration.JwtIssuer();
        var key = _configuration.JwtKey(_env);
        services
            .AddCors()
            .AddRouting()
            .AddAuthentication(auth =>
            {
                auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                auth.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.ClaimsIssuer = issuer;
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = issuer,
                    ValidAudience = issuer,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    RequireExpirationTime = false, // todo : configure depending of appsettings.json JWT section value
                };
            });
        services.AddAuthorization();

        services
            .AddApplication()
            .AddCommonInfrastructure();
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseDeveloperExceptionPage();

        app.UseRouting();
        app.UseAuthentication();
        app.UseAuthorization();
        
        app.UseCors(c => c
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}
