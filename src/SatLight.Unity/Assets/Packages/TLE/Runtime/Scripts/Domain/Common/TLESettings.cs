using UnityEngine;

namespace TLE.Runtime.Domain
{
    [CreateAssetMenu(menuName = "TLE/Settings", fileName = "TLE_Settings")]
    public class TLESettings : ScriptableObject
    {
        [field: SerializeField]
        public string ApiUrl { get; private set; }
    }
}