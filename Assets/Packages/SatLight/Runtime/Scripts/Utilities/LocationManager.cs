using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Ru1t3rl.Models;
using SatLight.Enums;
using SatLight.Runtime.Domain.Common;
using UnityEngine;
using UnityEngine.Networking;
using Logger = Ru1t3rl.Utilities.Logger;

namespace SatLight.Runtime.Utilities
{
    public class LocationManager : MonoBehaviour
    {
        private const string IP_GEOLOCATION_API = "https://ipinfo.io/json";

        [SerializeField] private StartupMode startupMode = StartupMode.OnAwake;

        [SerializeField] private bool useLocationService = true;

        private bool _locationServiceInitialized = false;

        private LocationService _locationService;

        private void Awake()
        {
            if (!useLocationService)
            {
                return;
            }

            if (startupMode != StartupMode.OnAwake)
            {
                return;
            }

            InitializeLocationService();
        }

        private void Start()
        {
            if (startupMode != StartupMode.OnStart)
            {
                return;
            }

            InitializeLocationService();
        }

        public void InitializeLocationService()
        {
            if (_locationServiceInitialized && _locationService != null)
            {
                return;
            }

            _locationService = new LocationService();

            if (_locationService.isEnabledByUser)
            {
                Logger.LogError<LocationManager>(
                    "You're trying to use the location while location service isn't active on the device");
                return;
            }

            _locationService.Start();
            _locationServiceInitialized = true;
        }

        /// <summary>
        /// Retrieves the geographical location using the IPv4 address of the device by making a request
        /// to an external IP geolocation API.
        /// </summary>
        /// <returns>
        /// A <see cref="Location"/> object representing the geographical coordinates (latitude, longitude, and optional altitude)
        /// obtained from the API response.
        /// </returns>
        /// <exception cref="WebException">
        /// Thrown if there is an error in the web request or the response indicates a failure.
        /// </exception>
        private async Task<Location> GetLocationWithIPv4()
        {
            var www = UnityWebRequest.Get(IP_GEOLOCATION_API);

            var request = www.SendWebRequest();
            while (!request.isDone)
            {
                await Task.Yield();
                Logger.Log($"Waiting for request to complete... ({request.progress * 100f}%)");
            }

            if (www.result != UnityWebRequest.Result.Success)
            {
                Logger.LogError(www.error);
                throw new WebException(www.error);
            }
            
            JsonSerializerOptions serializerOptions = new()
            {
                PropertyNameCaseInsensitive = true
            };
            
            var ipLocationJson = JsonSerializer.Deserialize<IpLocationResponse>(www.downloadHandler.text, serializerOptions);
            return ipLocationJson;
        }

        private Location GetLocationFromLocationService()
        {
            if (!_locationServiceInitialized)
            {
                Logger.LogError("The location service isn't initialized yet, please initialize it before usage!");
                throw new InvalidOperationException("Enable location services before using it.");
            }

            var lastLocationInfo = _locationService.lastData;

            return new Location(
                lastLocationInfo.latitude,
                lastLocationInfo.longitude,
                lastLocationInfo.altitude
            );
        }

        public async Task<Location> GetCurrentLocation(bool useIPv4)
        {
            return useIPv4 ? await GetLocationWithIPv4() : GetLocationFromLocationService();
        }
    }
}