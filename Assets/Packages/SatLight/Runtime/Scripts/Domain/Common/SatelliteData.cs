using SatLight.Models;
using UnityEngine;
using Logger = Ru1t3rl.Utilities.Logger;

namespace SatLight.Runtime.Domain.Common
{
    public class SatelliteData : MonoBehaviour
    {
        private SatAbove _satInfo;

        public SatAbove SatInfo
        {
            get => _satInfo;
            set
            {
                if (_satInfo == null || _satInfo.SatId == value.SatId)
                {
                    _satInfo = value;
                }
                else
                {
                    Logger.LogWarning<SatelliteData>("The satellite Id doesn't match with the existing satellite Id.");
                }
            }
        }

        public Location Location => new(
            _satInfo.SatLat,
            _satInfo.SatLng,
            _satInfo.SatAlt
        );

        public void UpdateLocation(double latitude, double longitude, double altitude)
        {
            _satInfo.SatLat = latitude;
            _satInfo.SatLng = longitude;
            _satInfo.SatAlt = altitude;
        }
    }
}