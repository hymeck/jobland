using Jobland.Infrastructure.Api.Web;
using Microsoft.AspNetCore;

WebHost.CreateDefaultBuilder(args)
    .UseStartup<JoblandStartup>()
    .Build()
    .Run();
