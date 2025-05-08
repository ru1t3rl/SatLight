using SatLight.Runtime.Domain.Common;
using UnityEngine;

namespace SatLight.Runtime.Domain
{
    [CreateAssetMenu(fileName = "UserSettings", menuName = "SatLight/User Settings")]
    public class UserSettings : ScriptableObject
    {
        private Location _location = new (0, 0);
        public Location Location => _location;

        public void SetUserLocation(Location userLocation)
        {
            _location = userLocation;
        }
    }
}
