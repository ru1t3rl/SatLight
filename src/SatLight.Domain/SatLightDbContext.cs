using Microsoft.EntityFrameworkCore;
using SatLight.Domain.Configuration;
using TickerQ.EntityFrameworkCore.Configurations;

namespace SatLight.Domain;

public class SatLightDbContext : DbContext
{
    public SatLightDbContext(DbContextOptions<SatLightDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new TimeTickerConfigurations());
        modelBuilder.ApplyConfiguration(new CronTickerConfigurations());
        modelBuilder.ApplyConfiguration(new CronTickerOccurrenceConfigurations());

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(NamespaceAnchor).Assembly);
    }
}