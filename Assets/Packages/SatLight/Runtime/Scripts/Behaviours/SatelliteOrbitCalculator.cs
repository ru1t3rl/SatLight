using System;
using System.Collections.Generic;
using N2YO.Runtime.Domain.Common;
using N2YO.Runtime.Domain.Responses;
using SatLight.Runtime.Domain.Common;
using SGPdotNET.CoordinateSystem;
using SGPdotNET.Propagation;
using SGPdotNET.TLE;

public class SatelliteOrbitCalculator
{
    // private const double MINTUES_IN_A_DAY = 1440.0;
    private const double MINUTES_IN_A_DAY = 2880.0;
    private readonly SatAbove _satData;
    private readonly TLEResponse _tleResponse;
    private readonly List<Location> _orbitPositions = new();

    private Tle _satelliteTle;
    private Sgp4 _sgp4;

    public SatelliteOrbitCalculator(SatAbove satData, TLEResponse tleResponse)
    {
        _satData = satData;
        _tleResponse = tleResponse;
        
        InitializeSatellite();
    }

    private void InitializeSatellite()
    {
        string[] tleLines = _tleResponse.Tle.Split("\r\n");
        _satelliteTle = new Tle(_satData.SatName, tleLines[0], tleLines[1]);
        _sgp4 = new Sgp4(_satelliteTle);
    }

    public List<Location> CalculateAllOrbitPositions()
    {
        _orbitPositions.Clear();
        double orbitalPeriod = MINUTES_IN_A_DAY / _satelliteTle.MeanMotionRevPerDay;

        // 360 degrees in a circle
        int degreesInCircle = 360;
        for (int i = 0; i < degreesInCircle; i++)
        {
            double timeOffset = (i * orbitalPeriod) / degreesInCircle;
            DateTime predictionTime = DateTime.UtcNow.AddMinutes(timeOffset);
            
            Location location = CalculateOrbitPosition(predictionTime);
            _orbitPositions.Add(location);
        }

        return _orbitPositions;
    }
    
    public List<Location> CalculateOrbitPositions(float timeDuration)
    {
        _orbitPositions.Clear();
        double orbitalPeriod = MINUTES_IN_A_DAY / _satelliteTle.MeanMotionRevPerDay;
        
        double portionOfOrbit = timeDuration / orbitalPeriod;
        int numberOfPoints = Math.Max(2, (int)(360 * portionOfOrbit));
    
        for (int i = 0; i < numberOfPoints; i++)
        {
            // Calculate time offset for this point
            double timeOffset = (i * timeDuration) / (numberOfPoints - 1);
            DateTime predictionTime = DateTime.UtcNow.AddMinutes(timeOffset);

            Location location = CalculateOrbitPosition(predictionTime);
            _orbitPositions.Add(location);
        }

        return _orbitPositions;
    }

    private Location CalculateOrbitPosition(DateTime time)
    {
        EciCoordinate position = _sgp4.FindPosition(time);
        GeodeticCoordinate geodetic = position.ToGeodetic();
        return new Location(
            geodetic.Latitude.Degrees,
            geodetic.Longitude.Degrees,
            geodetic.Altitude
        );
    }
}