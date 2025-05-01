using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SatLight.Models;
using UnityEngine;

namespace SatLight.Behaviours
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
            var satellites = await satGatherer.GetSatellites();

            IEnumerable<SatelliteBehaviour> children = transform.GetComponentsInChildren<SatelliteBehaviour>();

            foreach (var satellite in satellites)
            {
                SatelliteBehaviour? child = children.SingleOrDefault(c => c.gameObject.name == $"Sat_{satellite.SatId}");

                if (!child)
                {
                    CreateSatelliteGameObject(satellite);
                }
                else
                {
                    var location = new Location(satellite.SatLat, satellite.SatLng, satellite.SatAlt);
                    child.transform.localPosition = location.ToUnityCoordinates(
                        (satellitesParent ?? transform).localScale + distanceFromParent,
                        satellitesParent?.localRotation ?? Quaternion.identity);
                    child.SetSatelliteIno(satellite);
                }
            }
        }

        private void CreateSatelliteGameObject(SatAbove satellite)
        {
            Vector3 location = new Location(satellite.SatLat, satellite.SatLng, satellite.SatAlt)
                .ToUnityCoordinates((satellitesParent ?? transform).localScale + distanceFromParent,
                    satellitesParent?.localRotation ?? Quaternion.identity);

            var satGameObject =
                Instantiate(satellitePrefab, location, Quaternion.identity, satellitesParent ?? transform);
            var satBehaviour = satGameObject.GetComponent<SatelliteBehaviour>() ??
                               satGameObject.AddComponent<SatelliteBehaviour>();

            satGameObject.name = $"Sat_{satellite.SatId}";
            satBehaviour?.SetSatelliteIno(satellite);
        }
    }
}