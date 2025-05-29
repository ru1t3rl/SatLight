using System;
using System.Threading;
using System.Threading.Tasks;
using SatLight.Runtime.Domain.Common;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    [RequireComponent(typeof(SatelliteData))]
    public class SatellitePositionUpdater : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Update rate in seconds.\r\nPositions in FuturePositions are approximately 1sec apart.")]
        private float updateRate = 1f;

        private SatelliteData _data;
        private CancellationTokenSource _tokenSource = new();

        public bool IsActive { get; private set; } = false;

        public float UpdateRateInSeconds => updateRate;

        private void Awake()
        {
            _data = GetComponent<SatelliteData>();
        }

        private void OnEnable()
        {
            if (_tokenSource.IsCancellationRequested)
            {
                _tokenSource.Dispose();
                _tokenSource = new CancellationTokenSource();
            }

            _ = UpdateSatellitePosition(_tokenSource.Token);
        }

        private void OnDisable()
        {
            _tokenSource.Cancel();
        }

        private void OnDestroy()
        {
            _tokenSource.Dispose();
        }

        public void SetUpdateRate(float updateRate)
        {
            this.updateRate = updateRate;
        }

        private async Task UpdateSatellitePosition(CancellationToken token)
        {
            IsActive = true;
            while (!token.IsCancellationRequested)
            {
                try
                {
                    await Task.Delay((int)(updateRate * 1000f), token);
                }
                catch (TaskCanceledException)
                {
                    break;
                }

                if (_data.SatInfo == null)
                {
                    continue;
                }

                Location nextLocation = _data.GetNextLocation();
                _data.UpdateCurrentLocation(nextLocation.Latitude, nextLocation.Longitude, nextLocation.Altitude);

                Vector3 direction = _data.Location.ToDirection();
                try
                {
                    Vector3 newPosition = _data.Location.ToUnityCoordinates(
                        direction * (float)((transform.parent?.localScale.magnitude ?? 1) + _data.SatInfo.SatAlt)
                    );
                    transform.position = newPosition;
                }
                catch (Exception ex)
                {
                    Debug.LogError(ex);
                }
            }
            IsActive = false;
        }
    }
}