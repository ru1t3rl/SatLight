using System.Threading;
using System.Threading.Tasks;
using SatLight.Models;
using SatLight.Models.Responses;
using SatLight.Runtime.Domain.Common;
using TLE.Runtime.Controllers;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    [RequireComponent(typeof(SatelliteData))]
    public class SatelliteTLEDataUpdater : SatelliteDataUpdaterBase
    {
        private TLEReponse _tle;
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
            }

            return result.Match<bool>(model =>
                {
                    _tle = new TLEReponse
                    {
                        Info = new SatInfo
                        {
                            SatId = _data.SatInfo.SatId,
                            SatName = _data.SatInfo.SatName,
                        },
                        Tle = $"{model.line1}\r\n{model.line2}"
                    };
                    return true;
                },
                _ => false
            );
        }

        protected override Task UpdateInfo(CancellationToken cancellationToken)
        {
            var orbitPositions = _orbitCalculator.CalculateOrbitPositions(updateRate);
            _data.EnqueueFutureLocations(orbitPositions);
            return Task.CompletedTask;
        }
    }
}