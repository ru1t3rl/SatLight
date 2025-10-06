using SatLight;
using SatLight.Domain;
using SatLight.Services;
using Scalar.AspNetCore;
using TickerQ.Dashboard.DependencyInjection;
using TickerQ.DependencyInjection;
using TickerQ.EntityFrameworkCore.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.AddDatabase();

builder.Services.AddOpenApi();
builder.Services.AddTickerQ(options =>
    {
        options.AddOperationalStore<SatLightDbContext>(efOpt =>
            {
                efOpt.UseModelCustomizerForMigrations();
                efOpt.CancelMissedTickersOnAppStart();
            }
        );
        options.AddDashboard(c => c.BasePath = "/cron");
    }
);

builder.Services.Scan(s => s
    .FromAssemblyOf<NamespaceAnchor>()
    .AddClasses()
    .AsSelf()
    .WithScopedLifetime()
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference("/api");
}

app.UseTickerQ();
app.UseHttpsRedirection();
app.MapSatLightEndpoints();

app.Run();