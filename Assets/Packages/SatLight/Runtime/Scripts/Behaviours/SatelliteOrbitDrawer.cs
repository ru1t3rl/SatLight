using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using N2YO.Runtime.Domain.Factories;
using SatLight.Runtime.Domain.Common;
using TLE.Runtime.Controllers;
using Unity.Collections;
using UnityEngine;
using Logger = Ru1t3rl.Utilities.Logger;

namespace SatLight.Runtime.Behaviours
{
    [RequireComponent(typeof(SatelliteData))]
    public class SatelliteOrbitDrawer : MonoBehaviour
    {
        [SerializeField] private bool active = false;
        [SerializeField] private LineRenderer lineRenderer;
        private SatelliteData _data;

        private CancellationTokenSource _cancellationTokenSource = null;

        private async void Awake()
        {
            _data = GetComponent<SatelliteData>();
            _cancellationTokenSource = new CancellationTokenSource();
            
            if (!await TryInitOrbit(_cancellationTokenSource.Token))
            {
                string message = $"Failed to init orbit ({_data.SatInfo?.SatName ?? "NULL"}).";
                Logger.LogError(message);
                return;
            }

            SetupLineRenderer();
        }

        private async Task<bool> TryInitOrbit(CancellationToken cancellationToken = default)
        {
            while (_data.SatInfo == null)
            {
                await Task.Yield();

                if (cancellationToken.IsCancellationRequested)
                {
                    return false;
                }
            }

            var result = await TLEController.Instance.GetTLE(_data.SatInfo.SatId);
            if (result.TryPickT1(out _, out var model))
            {
                return false;
            }

            var satInfo = SatInfoFactory.Create(
                _data.SatInfo.SatId,
                _data.SatInfo.SatName
            );

            var tle = TLEResponseFactory.Create(
                satInfo,
                model.line1,
                model.line2
            );

            SatelliteOrbitCalculator calculator = new(_data.SatInfo, tle);
            
            var positions = calculator.CalculateAllOrbitPositions();
            _data.SetOrbitPositions(positions);

            return true;
        }

        private void SetupLineRenderer()
        {
            var unityPosition = _data.OrbitPositions
                .Select(position => position.ToUnityCoordinates(new Vector3(2.5f, 2.5f, 2.5f)))
                .ToArray();
            
            lineRenderer.positionCount = unityPosition.Length;
            lineRenderer.SetPositions(unityPosition);
            
            lineRenderer.enabled = active;
        }
        
        [ContextMenu("Toggle Active")]
        public void ToggleActive()
        {
            active = !active;
            lineRenderer.enabled = active;
        }
    }
}