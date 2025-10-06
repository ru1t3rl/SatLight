using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using SatLight.Runtime.Domain.Common;
using SatLight.Utilities;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    [RequireComponent(typeof(SatelliteData))]
    public class SatelliteN2YODataUpdater : SatelliteDataUpdaterBase
    {
        protected override async Task UpdateInfo(CancellationToken cancellationToken)
        {
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