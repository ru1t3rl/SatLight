using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using N2YO.Runtime.Domain.Common;
using NUnit.Framework;
using SatLight.Runtime.Behaviours;
using SatLight.Runtime.Domain.Common;
using SatLight.Runtime.Utilities;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using Object = UnityEngine.Object;

public class SatMapperTests
{
    private SatMapper _satMapper;
    private MockSatGatherer _satGatherer;
    private GameObject _satPrefab;

    [SetUp]
    public void Setup()
    {
        // Mock SatGatherer
        GameObject gathererObj = new GameObject("SatGatherer");
        _satGatherer = gathererObj.AddComponent<MockSatGatherer>();

        // Arrange
        GameObject mapperObj = new GameObject("SatMapper");
        mapperObj.SetActive(false);

        _satMapper = mapperObj.AddComponent<SatMapper>();
        SerializedObject serializedMapper = new SerializedObject(_satMapper);

        _satPrefab = new GameObject("SatellitePrefab");
        _satPrefab.AddComponent<SatelliteData>();


        var gatherProperty = serializedMapper.FindProperty("satGatherer");
        Assert.IsNotNull(gatherProperty);
        gatherProperty.objectReferenceValue = _satGatherer;

        var prefabProperty = serializedMapper.FindProperty("satellitePrefab");
        Assert.IsNotNull(prefabProperty);
        prefabProperty.objectReferenceValue = _satPrefab;

        serializedMapper.ApplyModifiedProperties();
    }

    [Test]
    public async Task SatMapper_AwakeInitialization_CreatesSatellites()
    {
        // Set up satellites to return
        _satGatherer.TestSatellites = new[]
        {
            new SatAbove { SatId = 1, SatLat = 10, SatLng = 20, SatAlt = 100 },
            new SatAbove { SatId = 2, SatLat = 30, SatLng = 40, SatAlt = 200 }
        };

        _satMapper.gameObject.SetActive(true);

        while (_satMapper.State != SatMapper.Status.Ready)
        {
            await Task.Yield();
        }

        // Assert
        var satellites = Object.FindObjectsByType<SatelliteData>(FindObjectsSortMode.None);
        satellites = satellites.Where(s => s.gameObject.name != _satPrefab.gameObject.name).ToArray();

        Assert.AreEqual(2, satellites.Length);
        Assert.IsTrue(satellites.Any(s => s.gameObject.name == "Sat_1"));
        Assert.IsTrue(satellites.Any(s => s.gameObject.name == "Sat_2"));
    }

    [Test]
    public async Task UpdateSatellites_UpdatesExistingSatellites()
    {
        // Initial satellites
        _satGatherer.TestSatellites = new[]
        {
            new SatAbove { SatId = 1, SatLat = 10, SatLng = 20, SatAlt = 100 }
        };

        _satMapper.gameObject.SetActive(true);

        while (_satMapper.State != SatMapper.Status.Ready)
        {
            await Task.Yield();
        }

        _satGatherer.TestSatellites = new[]
        {
            new SatAbove { SatId = 1, SatLat = 50, SatLng = 60, SatAlt = 150 },
            new SatAbove { SatId = 2, SatLat = 30, SatLng = 40, SatAlt = 200 }
        };

        MethodInfo updateMethodInfo = typeof(SatMapper).GetMethod(
            "UpdateSatellites",
            BindingFlags.NonPublic | BindingFlags.Instance
        );

        Assert.IsNotNull(updateMethodInfo);
        updateMethodInfo.Invoke(_satMapper, null);

        while (_satMapper.State != SatMapper.Status.Ready)
        {
            await Task.Yield();
        }

        // Assert
        var satellites = Object.FindObjectsByType<SatelliteData>(FindObjectsSortMode.None);
        satellites = satellites.Where(s => s.gameObject.name != _satPrefab.gameObject.name).ToArray();
        Assert.AreEqual(2, satellites.Length);

        var sat1 = satellites.SingleOrDefault(s => s.gameObject.name == "Sat_1");
        Assert.IsNotNull(sat1);
        Assert.AreEqual(50, sat1.SatInfo.SatLat);
        Assert.AreEqual(60, sat1.SatInfo.SatLng);
    }

    [UnityTearDown]
    private void TearDown()
    {
        Object.DestroyImmediate(_satMapper.gameObject);
        Object.DestroyImmediate(_satGatherer.gameObject);
        Object.DestroyImmediate(_satPrefab);
    }

    // Mock class to use for testing
    private class MockSatGatherer : SatGatherer
    {
        public SatAbove[] TestSatellites { get; set; }

        public override async Task<SatAbove[]> GetSatellites()
        {
            // Simulate async behavior
            await Task.Delay(10);
            return TestSatellites;
        }
    }
}