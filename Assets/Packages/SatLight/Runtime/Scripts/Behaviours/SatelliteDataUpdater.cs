using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using SatLight.Runtime.Domain;
using SatLight.Runtime.Domain.Common;
using SatLight.Utilities;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    [RequireComponent(typeof(SatelliteData))]
    public class SatelliteDataUpdater : MonoBehaviour
    {
        [Range(1, 300), SerializeField, Tooltip("Update rate in seconds!")]
        private int updateRate;

        private SatelliteData _data;
        private CancellationTokenSource _cancellationTokenSource = null;

        [CanBeNull, SerializeField,
         Tooltip("The user settings are used to share the user location between the satellite objects.")]
        private UserSettings userSettings;

        private void Awake()
        {
            _data = GetComponent<SatelliteData>();
        }

        private void OnEnable()
        {
            if (_cancellationTokenSource == null)
            {
                _cancellationTokenSource = new CancellationTokenSource();
                _ = UpdateInfo(_cancellationTokenSource.Token);
            }
        }

        private void OnDisable()
        {
            if (_cancellationTokenSource != null)
            {
                _cancellationTokenSource.Cancel();
            }
        }

        private async Task UpdateInfo(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(updateRate, cancellationToken);
                if (_data.SatInfo == null)
                {
                    continue;
                }

                var result = await N2YOController.Instance.GetSatellitePositions(
                    _data.SatInfo.SatId,
                    userSettings?.Location.Latitude ?? _data.SatInfo.SatLat,
                    userSettings?.Location.Longitude ?? _data.SatInfo.SatLng,
                    userSettings?.Location.Altitude ?? _data.SatInfo.SatAlt,
                    1
                );

                _data.EnqueueFutureLocations(result.Positions.Select(p => new Location(
                    p.SatLatitude,
                    p.SatLongitude,
                    p.Elevation
                )));
            }
        }
    }
}