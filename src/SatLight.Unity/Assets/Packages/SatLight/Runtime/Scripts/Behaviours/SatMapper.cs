using System.Collections.Generic;
using System.Linq;
using N2YO.Runtime.Domain.Common;
using SatLight.Runtime.Domain.Common;
using SatLight.Runtime.Utilities;
using UnityEngine;

namespace SatLight.Runtime.Behaviours
{
    public class SatMapper : MonoBehaviour
    {
        public enum Status
        {
            NotInitialized,
            Initializing,
            Updating,
            Ready
        }

        [SerializeField] private SatGatherer satGatherer;

        [SerializeField] private GameObject satellitePrefab;

        [SerializeField] private Transform satellitesParent;

        [SerializeField] private Vector3 distanceFromParent = new(0.1f, 0.1f, 0.1f);

        public Status State { get; private set; } = Status.NotInitialized;

        private async void Awake()
        {
            var satellites = await satGatherer.GetSatellites();
            InstantiateSatellites(satellites);
            State = Status.Ready;
        }

        private void InstantiateSatellites(SatAbove[] satellites)
        {
            State = Status.Initializing;
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
            State = Status.Updating;

            SatAbove[] satellites = await satGatherer.GetSatellites();

            var parent = !satellitesParent ? transform : satellitesParent;
            IEnumerable<SatelliteData> children = parent.GetComponentsInChildren<SatelliteData>();

            foreach (var satellite in satellites)
            {
                SatelliteData child = children.SingleOrDefault(c => c.gameObject.name == $"Sat_{satellite.SatId}");

                if (!child)
                {
                    CreateSatelliteGameObject(satellite);
                }
                else
                {
                    var location = new Location(satellite.SatLat, satellite.SatLng, satellite.SatAlt);
                    child.transform.position = location.ToUnityCoordinates(
                        parent.localScale + distanceFromParent
                    );
                    child.SatInfo = satellite;
                }
            }

            State = Status.Ready;
        }

        private void CreateSatelliteGameObject(SatAbove satellite)
        {
            var parent = !satellitesParent ? transform : satellitesParent;

            Vector3 location = new Location(satellite.SatLat, satellite.SatLng, satellite.SatAlt)
                .ToUnityCoordinates(parent.localScale + distanceFromParent);

            var satGameObject =
                Instantiate(satellitePrefab, location, Quaternion.identity, parent);
            var satData = satGameObject.GetComponent<SatelliteData>() ??
                          satGameObject.AddComponent<SatelliteData>();

            satGameObject.name = $"Sat_{satellite.SatId}";
            satData.SatInfo = satellite;
        }
    }
}