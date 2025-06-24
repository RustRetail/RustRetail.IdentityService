using RustRetail.IdentityService.API.Configuration;
using RustRetail.IdentityService.Application;
using RustRetail.IdentityService.Infrastructure;
using RustRetail.IdentityService.Persistence;
using RustRetail.SharedInfrastructure.Logging.Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .ConfiguringOptions(builder.Configuration)
    .AddSharedServices(builder.Configuration)
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddApi(builder.Configuration);
builder.Host.UseSharedSerilog();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.ConfigureApplicationPipeline();

app.Run();
