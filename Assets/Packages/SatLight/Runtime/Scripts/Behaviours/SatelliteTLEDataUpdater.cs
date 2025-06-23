using System.Threading;
using System.Threading.Tasks;
using N2YO.Runtime.Domain.Factories;
using N2YO.Runtime.Domain.Responses;
using SatLight.Runtime.Domain.Common;
using TLE.Runtime.Controllers;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    [RequireComponent(typeof(SatelliteData))]
    public class SatelliteTLEDataUpdater : SatelliteDataUpdaterBase
    {
        private TLEResponse _tle;
        private SatelliteOrbitCalculator _orbitCalculator;

        private CancellationTokenSource _cancellationTokenSource = null;

        protected override async void Awake()
        {
            base.Awake();

            _cancellationTokenSource = new CancellationTokenSource();
            if (await TryInitTLE(_cancellationTokenSource.Token))
            {
                _orbitCalculator = new(_data.SatInfo, _tle);
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            _cancellationTokenSource?.Cancel();
        }

        private async Task<bool> TryInitTLE(CancellationToken cancellationToken = default)
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
                gameObject.SetActive(false);
                return false;
            }

            var info = SatInfoFactory.Create(_data.SatInfo.SatId, _data.SatInfo.SatName);
            _tle = TLEResponseFactory.Create(info, model.line1, model.line2);
            
            return true;
        }

        protected override Task UpdateInfo(CancellationToken cancellationToken)
        {
            if (_orbitCalculator == null || !_data)
            {
                return Task.CompletedTask;
            }
            
            var orbitPositions = _orbitCalculator.CalculateOrbitPositions(updateRate);
            _data.EnqueueFutureLocations(orbitPositions);
            return Task.CompletedTask;
        }
    }
}