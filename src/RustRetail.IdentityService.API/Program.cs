using RustRetail.IdentityService.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi()
    .AddPersistence(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.Run();
