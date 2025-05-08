using System;
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
            transform.position = _data.Location.ToUnityCoordinates();
        }
    }
}