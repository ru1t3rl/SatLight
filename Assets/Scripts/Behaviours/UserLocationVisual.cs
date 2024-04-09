using Ru1t3rl.Utilities;
using UnityEngine;

public class UserLocationVisual : MonoBehaviour
{
    [SerializeField]
    private LocationManager locationManager;

    private async void Awake()
    {
        locationManager.InitializeLocationService();
        var location = await locationManager.GetCurrentLocation(true);
        transform.position = location.ToUnityCoordinates(transform.parent.localScale);
    }
}