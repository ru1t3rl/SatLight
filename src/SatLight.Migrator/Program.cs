using Microsoft.EntityFrameworkCore;
using SatLight.Domain;

var builder = WebApplication.CreateBuilder(args);
builder.AddDatabase();

var app = builder.Build();

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<SatLightDbContext>();
await dbContext.Database.MigrateAsync();