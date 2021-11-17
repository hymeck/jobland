using Jobland.Dependencies;
using Jobland.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependencies(builder.Configuration);

await using var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseRouting();

app.UseCors(c => c
    .AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader());

app.UseAuthentication()
    .UseAuthorization();

app.AddEndpoints();

await app.RunAsync();
