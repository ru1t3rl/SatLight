using Microsoft.Extensions.Logging;
using TickerQ.Utilities.Base;

namespace SatLight.Services;

public class SatelliteScraperJob
{
    private readonly ILogger<SatelliteScraperJob> _logger;

    public SatelliteScraperJob(ILogger<SatelliteScraperJob> logger)
    {
        _logger = logger;
    }

    [TickerFunction("Scrape Satellites")]
    public async Task ScrapeSatellites()
    {
        List<string> noradIds = await GetNoradIds();
        List<object> satellites = await GetSatellites(noradIds);
        await StoreSatellitesInDatabase(satellites);
    }

    /// <summary>
    /// Get the norad ids of all satellites.
    /// </summary>
    /// <returns>The list of norad ids as strings.</returns>
    private async Task<List<string>> GetNoradIds()
    {
        return [];
    }

    /// <summary>
    /// Get detailed info about multiple satellites based on their norad ids.
    /// </summary>
    /// <param name="noradIds">A list of satellite (norad) ids.</param>
    /// <returns>Returns detailed info about the provided norad ids.</returns>
    private async Task<List<object>> GetSatellites(List<string> noradIds)
    {
        return [];
    }

    private async Task StoreSatellitesInDatabase(List<object> satellites)
    {
        
    }
}