using System.Collections.Generic;
using N2YO.Runtime.Domain.Common;
using UnityEngine;
using Logger = Ru1t3rl.Utilities.Logger;

namespace SatLight.Runtime.Domain.Common
{
    public class SatelliteData : MonoBehaviour
    {
        private SatAbove _satInfo;
        private readonly Queue<Location> _futurePositions = new();
        private readonly List<Location> _orbitPositions;

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

        public List<Location> OrbitPositions => _orbitPositions;

        public Location Location => new(
            _satInfo.SatLat,
            _satInfo.SatLng,
            _satInfo.SatAlt
        );

        public void SetOrbitPositions(List<Location> orbitPositions)
        {
            _orbitPositions.Clear();
            _orbitPositions.AddRange(orbitPositions);
        }

        public void UpdateCurrentLocation(double latitude, double longitude, double altitude)
        {
            _satInfo.SatLat = latitude;
            _satInfo.SatLng = longitude;
            _satInfo.SatAlt = altitude;
        }

        public void EnqueueFutureLocations(IEnumerable<Location> locations)
        {
            foreach (Location location in locations)
            {
                _futurePositions.Enqueue(location);
            }
        }

        public Location GetNextLocation(bool dequeue = true)
        {
            return dequeue ? _futurePositions.Dequeue() : _futurePositions.Peek();
        }
    }
}