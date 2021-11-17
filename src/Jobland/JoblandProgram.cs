using Jobland.Dependencies;
using Jobland.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependencies();

await using var app = builder.Build();

app.UseDeveloperExceptionPage();

app.UseRouting();
app.AddEndpoints();

await app.RunAsync();
