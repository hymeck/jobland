using System.Reflection;
using Jobland.Application.Logic.Abstractions;
using Jobland.Application.Persistence.Abstractions;
using Jobland.Infrastructure.Common.Identity;
using Jobland.Infrastructure.Common.Logic.Implementations;
using Jobland.Infrastructure.Common.Persistence;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace Jobland.Infrastructure.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddCommonInfrastructure(this IServiceCollection services)
    {
        services
            .AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddMediatR(Assembly.GetExecutingAssembly())
            .AddIdentity<User, IdentityRole>(identityOptions =>
            {
                identityOptions.Password.RequireNonAlphanumeric = false;
                identityOptions.Password.RequireLowercase = false;
                identityOptions.Password.RequireUppercase = false;
                identityOptions.Password.RequireDigit = false;
            })
            .AddEntityFrameworkStores<ApplicationDbContext>();
        
        return services
            .AddDbContext<ApplicationDbContext>()
            .AddScoped<IApplicationDbContext, ApplicationDbContext>()
            .AddScoped<ICategoryRepository, CategoryRepository>()
            .AddScoped<IWorkRepository, WorkRepository>();
    }
}
