using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace SatLight.Domain;

public static class ServiceCollectionExtensions
{
    public static IHostApplicationBuilder AddDatabase(this IHostApplicationBuilder builder)
    {
        builder.Services.AddDbContext<SatLightDbContext>(
            options => { options.UseNpgsql(builder.Configuration.GetConnectionString("SatLight")); },
            ServiceLifetime.Transient
        );
        return builder;
    }
}