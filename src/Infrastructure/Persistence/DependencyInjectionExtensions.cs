using System;
using Application.Dao;
using Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Dao;

namespace Persistence
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddMysql(this IServiceCollection services,
            IConfiguration configuration) =>
            services
                .AddScoped<IDaoAsync<Work>, DaoAsync<Work>>()
                .AddScoped<IWorkDao, WorkDao>()
                .AddDbContext<ApplicationDbContext>(options =>
                {
                    options.UseMySql(configuration.GetConnectionString("mysql"),
                        new MySqlServerVersion(new Version(8, 0, 26)));
                });
    }
}
