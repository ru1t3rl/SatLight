using Endpoints;

namespace SatLight;

public static class WebApplicationExtensions
{
    public static WebApplication MapSatLightEndpoints(this WebApplication app)
    {
        SatelliteDetailEndpoints.Map(app);
        TleEndpoints.Map(app);
        return app;
    }
}