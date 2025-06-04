using RustRetail.IdentityService.API.Configuration;
using RustRetail.IdentityService.Infrastructure;
using RustRetail.IdentityService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApi(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.ConfigureApplicationPipeline();

app.MapGet("test", () =>
{
    return "Identity Service is running!";
});

app.Run();
