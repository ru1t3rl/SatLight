using System;
using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using SatLight.Models;
using SatLight.Runtime.Behaviours;
using SatLight.Runtime.Domain.Common;
using UnityEngine;
using UnityEngine.TestTools;

public class SatellitePositionUpdaterTests
{
    private GameObject _satellite;
    private SatelliteData _data;
    private SatAbove _satInfo;
    private SatellitePositionUpdater _positionUpdater;

    [UnitySetUp]
    public IEnumerator Setup()
    {
        _satellite = new("test_satellite");
        _data = _satellite.AddComponent<SatelliteData>();
        _satInfo = new()
        {
            SatId = 0,
            SatName = "test_satellite",
            IntDesignator = null,
            LaunchDate = DateTime.UtcNow.Date.ToShortDateString(),
            SatLat = 0,
            SatLng = 0,
            SatAlt = 0
        };

        _data.SatInfo = _satInfo;
        _positionUpdater = _satellite.AddComponent<SatellitePositionUpdater>();

        yield return null;
    }

    [UnityTearDown]
    public IEnumerator TearDown()
    {
        UnityEngine.Object.Destroy(_satellite);
        yield return null;
    }

    [UnityTest]
    public IEnumerator PositionIsUpdatedAfterSingleDelay()
    {
        GameObject earth = new GameObject("earth");
        _satellite.transform.SetParent(earth.transform);

        Vector3 initialPosition = _satellite.transform.position;
        List<Location> futureLocations = new()
        {
            new(0, 0, 0),
            new(15, 15, 15)
        };

        _data.EnqueueFutureLocations(futureLocations);

        yield return new WaitForSeconds(_positionUpdater.UpdateRateInSeconds * (futureLocations.Count));
        yield return null;

        Vector3 newPosition = _satellite.transform.position;
        Assert.AreNotEqual(initialPosition, newPosition);

        Assert.AreEqual(futureLocations[1].Latitude, _data.Location.Latitude);
        Assert.AreEqual(futureLocations[1].Longitude, _data.Location.Longitude);
        Assert.AreEqual(futureLocations[1].Altitude, _data.Location.Altitude);
    }

    [UnityTest]
    public IEnumerator PositionUpdatesContinueAfterDisablingAndEnabling()
    {
        var futureLocations = new List<Location>
        {
            new(15.0, 25.0, 510.0),
            new(20.0, 30.0, 520.0),
            new(25.0, 35.0, 530.0)
        };

        _data.EnqueueFutureLocations(futureLocations);

        yield return new WaitForSeconds(_positionUpdater.UpdateRateInSeconds);
        yield return null;
        
        _positionUpdater.enabled = false;
        
        yield return new WaitForSeconds(_positionUpdater.UpdateRateInSeconds);
        yield return null;

        Assert.AreEqual(futureLocations[0].Latitude, _data.Location.Latitude);
        Assert.AreEqual(futureLocations[0].Longitude, _data.Location.Longitude);
        Assert.AreEqual(futureLocations[0].Altitude, _data.Location.Altitude);

        _positionUpdater.enabled = true;

        yield return null;
        yield return new WaitForSeconds(_positionUpdater.UpdateRateInSeconds);
        yield return null;

        Assert.AreEqual(futureLocations[1].Latitude, _data.Location.Latitude);
        Assert.AreEqual(futureLocations[1].Longitude, _data.Location.Longitude);
        Assert.AreEqual(futureLocations[1].Altitude, _data.Location.Altitude);
    }

    [UnityTest]
    public IEnumerator PositionIsUpdatedCorrectlyWithSpecifiedInterval()
    {
        float updateRate = 0.25f;
        _positionUpdater.SetUpdateRate(updateRate);
        Assert.AreEqual(updateRate, _positionUpdater.UpdateRateInSeconds);

        var futureLocations = new List<Location>
        {
            new(15.0, 25.0, 510.0),
            new(20.0, 30.0, 520.0),
            new(25.0, 35.0, 530.0)
        };

        _positionUpdater.enabled = false;
        yield return new WaitForSeconds(1f);
        Assert.IsFalse(_positionUpdater.IsActive);
        
        _data.EnqueueFutureLocations(futureLocations);
        _positionUpdater.enabled = true;

        yield return new WaitForSeconds(updateRate);
        yield return null;

        Assert.AreEqual(futureLocations[0].Latitude, _data.Location.Latitude);
        Assert.AreEqual(futureLocations[0].Longitude, _data.Location.Longitude);
        Assert.AreEqual(futureLocations[0].Altitude, _data.Location.Altitude);

        yield return new WaitForSeconds(updateRate);
        yield return null;

        Assert.AreEqual(futureLocations[1].Latitude, _data.Location.Latitude);
        Assert.AreEqual(futureLocations[1].Longitude, _data.Location.Longitude);
        Assert.AreEqual(futureLocations[1].Altitude, _data.Location.Altitude);
        
        yield return new WaitForSeconds(updateRate);
        yield return null;

        Assert.AreEqual(futureLocations[2].Latitude, _data.Location.Latitude);
        Assert.AreEqual(futureLocations[2].Longitude, _data.Location.Longitude);
        Assert.AreEqual(futureLocations[2].Altitude, _data.Location.Altitude);
    }
}