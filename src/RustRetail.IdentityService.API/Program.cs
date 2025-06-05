using RustRetail.IdentityService.API.Configuration;
using RustRetail.IdentityService.Application;
using RustRetail.IdentityService.Infrastructure;
using RustRetail.IdentityService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi()
    .AddPersistence(builder.Configuration)
    .AddInfrastructure(builder.Configuration)
    .AddApplication()
    .AddApi(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.ConfigureApplicationPipeline();

app.Run();
