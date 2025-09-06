using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using N2YO.Runtime.Domain.Common;
using N2YO.Runtime.Domain.Enums;
using N2YO.Runtime.Domain.Factories;
using N2YO.Runtime.Domain.Responses;
using OneOf;
using OneOf.Types;
using Ru1t3rl.Utilities;

namespace SatLight.Runtime.Utilities
{
    public class SatDataStore : UnitySingleton<SatDataStore>
    {
        private readonly ConcurrentDictionary<SatCategory, List<TLEResponse>> _satellites = new();
        public List<TLEResponse> GetSatellites(SatCategory category) => _satellites[category];

        public TLEResponse GetSatellite(int id) => _satellites
            .SelectMany(kv => kv.Value)
            .SingleOrDefault(v => v.Info.SatId == id);

        public void AddSatellite(SatCategory category, SatAbove sat)
        {
            if (!_satellites.TryGetValue(category, out List<TLEResponse> satellites))
            {
                satellites = new();
                _satellites.TryAdd(category, satellites);
            }

            TLEResponse tle = ConvertSatelliteToTLEResponse(sat);
            satellites.Add(tle);
        }

        public void AddSatelliteRange(SatCategory category, IEnumerable<SatAbove> sats)
        {
            if (!_satellites.TryGetValue(category, out List<TLEResponse> satellites))
            {
                satellites = new();
                _satellites.TryAdd(category, satellites);
            }

            IEnumerable<SatAbove> satAboves = sats as SatAbove[] ?? sats.ToArray();
            IEnumerable<TLEResponse> tles = satAboves.Select(ConvertSatelliteToTLEResponse);
            satellites.AddRange(tles);
        }

        public void AddSatelliteRange(IEnumerable<KeyValuePair<SatCategory, IEnumerable<SatAbove>>> values)
        {
            foreach (var (key, value) in values)
            {
                AddSatelliteRange(key, value);
            }
        }

        public bool TryUpdateSatellite(int satId, TLEResponse tle)
        {
            if (!TryGetSatCategory(satId, out SatCategory category))
            {
                return false;
            }

            List<TLEResponse> satellites = _satellites[category];
            satellites.RemoveAll(s => s.Info.SatId == satId);
            satellites.Add(tle);

            return true;
        }

        private TLEResponse ConvertSatelliteToTLEResponse(SatAbove sat)
        {
            SatInfo satInfo = SatInfoFactory.Create(
                sat.SatId,
                sat.SatName
            );
            return TLEResponseFactory.Create(
                satInfo,
                ""
            );
        }

        private bool TryGetSatCategory(int satId, out SatCategory satCategory)
        {
            ICollection<SatCategory> keys = _satellites.Keys;
            foreach (SatCategory key in keys)
            {
                if (_satellites[key].Any(s => s.Info.SatId == satId))
                {
                    satCategory = key;
                    return true;
                }
            }

            satCategory = default;
            return false;
        }
    }
}