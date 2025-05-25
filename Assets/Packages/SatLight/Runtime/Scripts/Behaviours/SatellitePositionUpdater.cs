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
        [SerializeField] private float updateRate = 1f; // The positions in FuturePositions are approximately 1sec apart
        private SatelliteData _data;
        private CancellationTokenSource _tokenSource = new ();

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

        private async Task UpdateSatellitePosition(CancellationToken token)
        {
            while (!token.IsCancellationRequested)
            {
                await Task.Delay((int)(updateRate * 1000), token);

                if (_data.SatInfo == null)
                {
                    continue;
                }

                Location nextLocation = _data.GetNextLocation();
                _data.UpdateCurrentLocation(nextLocation.Latitude, nextLocation.Longitude, nextLocation.Altitude);
                
                Vector3 direction = transform.localPosition.normalized;
                try
                {
                    transform.position = _data.Location.ToUnityCoordinates(
                        direction * (float)((transform.parent?.localScale.magnitude ?? 1) + _data.SatInfo.SatAlt)
                    );
                }
                catch (Exception ex) {Debug.LogError(ex);}
            }
        }
    }
}