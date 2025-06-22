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

        protected override async void Awake()
        {
            base.Awake();
            await InitTLE();

            _orbitCalculator = new(_data.SatInfo, _tle);
        }

        private async Task InitTLE()
        {
            var result = await TLEController.Instance.GetTLE(_data.SatInfo.SatId);
            _tle = new TLEReponse
            {
                Info = new SatInfo
                {
                    SatId = _data.SatInfo.SatId,
                    SatName = _data.SatInfo.SatName,
                },
                Tle = $"{result.line1}\r\n{result.line2}"
            };
        }

        protected override Task UpdateInfo(CancellationToken cancellationToken)
        {
            var orbitPositions = _orbitCalculator.CalculateOrbitPositions(updateRate);
            _data.EnqueueFutureLocations(orbitPositions);
            return Task.CompletedTask;
        }
    }
}