using System.Reflection;
using Jobland.Persistence;
using Microsoft.EntityFrameworkCore;

namespace Jobland.Dependencies;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services) =>
        services.AddAutoMapper(Assembly.GetExecutingAssembly())
            .AddDbContext<ApplicationDbContext>((provider, optionsBuilder) =>
            {
                var connectionString = provider
                    .GetRequiredService<IConfiguration>()
                    .GetConnectionString("RemoteMysql");
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });
}
