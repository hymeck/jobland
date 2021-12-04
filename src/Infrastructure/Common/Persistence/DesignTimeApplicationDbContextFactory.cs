using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jobland.Infrastructure.Common.Persistence;

public class DesignTimeApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    { 
        var configManager = new ConfigurationManager();
        configManager.GetSection("ConnectionStrings")["RemoteMysql"] = args.Length switch
        {
            > 0 => args[0],
            _ => "no connection string provided"
        };
        return new ApplicationDbContext(configManager);
    }
}
