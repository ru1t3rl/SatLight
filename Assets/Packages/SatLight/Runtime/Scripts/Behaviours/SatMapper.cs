using System.Collections.Generic;
using System.Linq;
using SatLight.Models;
using SatLight.Runtime.Domain.Common;
using SatLight.Runtime.Utilities;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    public class SatMapper : MonoBehaviour
    {
        [SerializeField] private SatGatherer satGatherer;

        [SerializeField] private GameObject satellitePrefab;

        [SerializeField] private Transform satellitesParent;

        [SerializeField] private Vector3 distanceFromParent = new(0.1f, 0.1f, 0.1f);

        private async void Awake()
        {
            var satellites = await satGatherer.GetSatellites();
            InstantiateSatellites(satellites);
        }

        private void InstantiateSatellites(SatAbove[] satellites)
        {
            foreach (var satellite in satellites)
            {
                CreateSatelliteGameObject(satellite);
            }
        }

#if UNITY_EDITOR
        [ContextMenu("Update Satellites")]
#endif
        private async void UpdateSatellites()
        {
            SatAbove[] satellites = await satGatherer.GetSatellites();

            IEnumerable<SatelliteData> children =
                (satellitesParent ?? transform).GetComponentsInChildren<SatelliteData>();

            foreach (var satellite in satellites)
            {
                SatelliteData child = children.SingleOrDefault(c => c.gameObject.name == $"Sat_{satellite.SatId}");

                if (child)
                {
                    CreateSatelliteGameObject(satellite);
                }
                else
                {
                    var location = new Location(satellite.SatLat, satellite.SatLng, satellite.SatAlt);
                    child.transform.position = location.ToUnityCoordinates(
                        (satellitesParent ?? transform).localScale + distanceFromParent
                    );
                    child.SatInfo = satellite;
                }
            }
        }

        private void CreateSatelliteGameObject(SatAbove satellite)
        {
            Vector3 location = new Location(satellite.SatLat, satellite.SatLng, satellite.SatAlt)
                .ToUnityCoordinates((satellitesParent ?? transform).localScale + distanceFromParent);

            var satGameObject =
                Instantiate(satellitePrefab, location, Quaternion.identity, satellitesParent ?? transform);
            var satData = satGameObject.GetComponent<SatelliteData>() ??
                          satGameObject.AddComponent<SatelliteData>();

            satGameObject.name = $"Sat_{satellite.SatId}";
            satData.SatInfo = satellite;
        }
    }
}