using System;
using System.Collections.Generic;
using SatLight.Models;
using SatLight.Models.Responses;
using SatLight.Runtime.Domain.Common;
using SGPdotNET.CoordinateSystem;
using UnityEngine;
using SGPdotNET.Propagation;
using SGPdotNET.TLE;

public class SatelliteOrbitCalculator
{
    private const double MINTUES_IN_A_DAY = 1440.0;

    private readonly SatAbove _satData;
    private readonly TLEReponse _tleResponse;

    private Tle _satelliteTle;
    private Sgp4 _sgp4;
    private List<Location> _orbitPositions = new();

    public SatelliteOrbitCalculator(SatAbove satData, TLEReponse tleResponse)
    {
        _satData = satData;
        _tleResponse = tleResponse;
    }

    private void InitializeSatellite()
    {
        string[] tleLines = _tleResponse.Tle.Split("\r\n");
        _satelliteTle = new Tle(_satData.SatName, tleLines[0], tleLines[1]);
        _sgp4 = new Sgp4(_satelliteTle);
    }

    public List<Vector3> CalculateOrbitPositions()
    {
        _orbitPositions.Clear();
        double orbitalPeriod = MINTUES_IN_A_DAY / _satelliteTle.MeanMotionRevPerDay;

        // 360 degrees in a circle
        int degreesInCircle = 360;
        for (int i = 0; i < degreesInCircle; i++)
        {
            double timeOffset = (i * orbitalPeriod) / degreesInCircle;
            DateTime predictionTime = DateTime.UtcNow.AddMinutes(timeOffset);

            EciCoordinate position = _sgp4.FindPosition(predictionTime);
            GeodeticCoordinate geodetic = position.ToGeodetic();
            Location location = new Location(
                geodetic.Latitude.Degrees,
                geodetic.Longitude.Degrees,
                geodetic.Altitude
            );
            
            _orbitPositions.Add(location);
        }

        return _orbitPositions;
    }
}