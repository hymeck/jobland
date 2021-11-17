var builder = WebApplication.CreateBuilder(args);
await using var app = builder.Build();

app.MapGet("/", () => "Hello World!");

await app.RunAsync();
