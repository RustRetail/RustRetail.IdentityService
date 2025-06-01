using Microsoft.EntityFrameworkCore;
using RustRetail.IdentityService.Persistence;
using RustRetail.IdentityService.Persistence.Database;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi()
    .AddPersistence(builder.Configuration);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<IdentityDbContext>();
    try
    {
        db.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Error when trying to apply migration to postgres database: " + ex.Message);
    }
}

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapGet("test", () =>
{
    return "Identity Service is running!";
});

app.Run();
