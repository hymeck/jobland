using System.Reflection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application
{
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Add dependencies of application layer into specified <see cref="IServiceCollection"/> object.
        /// </summary>
        /// <param name="services">Collection of services.</param>
        /// <returns>A reference to this instance after the operation has completed.</returns>
        public static IServiceCollection AddApplication(this IServiceCollection services) =>
            services
                .AddAutoMapper(Assembly.GetExecutingAssembly())
                .AddFluentValidation(conf => conf.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()))
                .AddMediatR(Assembly.GetExecutingAssembly());
    }
}
