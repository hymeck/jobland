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
                var connectionString = ConnectionString(provider);
                optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
            });

    private static string ConnectionString(IServiceProvider provider) =>
        provider.GetRequiredService<IWebHostEnvironment>().IsDevelopment()
            ? provider.GetRequiredService<IConfiguration>().GetConnectionString("RemoteMysql")
            : Environment.GetEnvironmentVariable("MYSQLCONNSTR_RemoteMysql") ?? "";
}
