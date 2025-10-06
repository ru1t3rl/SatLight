using SatLight.Runtime.Domain;
using SatLight.Runtime.Utilities;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    public class UserLocationVisual : MonoBehaviour
    {
        [SerializeField] private LocationManager locationManager;
        [SerializeField] private UserSettings userSettings;

        private async void Awake()
        {
            locationManager.InitializeLocationService();
            var location = await locationManager.GetCurrentLocation(true);
            userSettings.SetUserLocation(location);
            transform.position = location.ToUnityCoordinates(transform.parent.localScale);
        }
    }
}