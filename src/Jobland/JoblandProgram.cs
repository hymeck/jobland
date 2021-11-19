using Jobland.Dependencies;
using Jobland.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependencies(builder.Configuration, builder.Environment);

await using var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseCors(c => c
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication()
    .UseRouting()
    .UseAuthorization();

app.AddEndpoints();

await app.RunAsync();
