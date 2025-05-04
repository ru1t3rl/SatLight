using System.Threading.Tasks;
using Ru1t3rl.Utilities;
using SatLight.Enums;
using SatLight.Models;
using SatLight.Models.Responses;
using SatLight.Utilities;
using UnityEngine;

public class SatGatherer : MonoBehaviour
{
    [SerializeField] 
    private StartupMode startupMode = StartupMode.OnAwake;

    [Header("Sat Discovery Settings")]
    [SerializeField] 
    private N2YOController satController;
    
    [SerializeField] 
    private SatCategory satCategory = SatCategory.Starlink;
    
    [SerializeField, Range(0, 360)] 
    private int radius = 15;

    [Header("Location Settings")]
    [SerializeField] 
    private LocationManager locationManager;
    
    [SerializeField]
    private bool useLocationService = true;
    

    private async void Awake()
    {
        satController ??= N2YOController.Instance;

        if (startupMode != StartupMode.OnAwake)
        {
            return;
        }
        
        await GetSatellites();
    }

    private async void Start()
    {
        if (startupMode != StartupMode.OnStart)
        {
            return;
        }

        await GetSatellites();
    }

    public async Task<SatAbove[]> GetSatellites(bool fromCurrentLocation = true)
    {
        var observerLocation = await locationManager.GetCurrentLocation(!useLocationService);

        AboveResponse response = await satController.WhatsAbove(
            observerLocation.Latitude,
            observerLocation.Longitude,
            observerLocation.Altitude,
            radius,
            (int)satCategory
        );

        return response.Above;
    }
}