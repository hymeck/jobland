using Jobland.Dependencies;
using Jobland.Endpoints;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDependencies();

await using var app = builder.Build();

if (app.Environment.IsDevelopment()) 
    app.UseDeveloperExceptionPage();

app.UseRouting();
app.AddEndpoints();

await app.RunAsync();
