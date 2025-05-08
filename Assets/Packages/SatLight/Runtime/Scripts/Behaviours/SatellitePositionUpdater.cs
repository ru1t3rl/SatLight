using SatLight.Runtime.Domain.Common;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    [RequireComponent(typeof(SatelliteData))]
    public class SatellitePositionUpdater : MonoBehaviour
    {
        private SatelliteData _data;

        private void Awake()
        {
            _data = GetComponent<SatelliteData>();
        }

        private void FixedUpdate()
        {
            Vector3 direction = transform.localPosition.normalized;
            
            // TODO: Check if we need to fix anything altitude related
            transform.position = _data.Location.ToUnityCoordinates(
                direction * (float)(transform.parent.localScale.magnitude + _data.SatInfo.SatAlt) 
            );
        }
    }
}