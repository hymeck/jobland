using System.Reflection;
using System.Text;
using Jobland.Authentication;
using Jobland.Persistence;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Jobland.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration) =>
        services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddScoped<IJwtTokenService, JwtTokenService>()
            .AddIdentity()
            .AddJwt(configuration)
            .AddDbContext<ApplicationDbContext>((provider, optionsBuilder) =>
            {
                var connectionString = ConnectionString(provider);
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

    private static string ConnectionString(IServiceProvider provider) =>
        provider.GetRequiredService<IWebHostEnvironment>().IsDevelopment()
            ? provider.GetRequiredService<IConfiguration>().GetConnectionString("RemoteMysql")
            : Environment.GetEnvironmentVariable("MYSQLCONNSTR_RemoteMysql") ?? "";

    private static IServiceCollection AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
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
                jwt.ClaimsIssuer = configuration["Jwt:Issuer"];
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])),
                    RequireExpirationTime = false, // todo : configure depending of appsettings.json JWT section value
                };
            });
        services.AddAuthorization();
        return services;
    }

    private static IServiceCollection AddIdentity(this IServiceCollection services)
    {
        services
            .AddIdentity<User, IdentityRole>(identityOptions =>
            {
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        return services;
    }
}
