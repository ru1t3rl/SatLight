using System.Collections;
using System.Collections.Generic;
using SatLight.Enums;
using SatLight.Models;
using UnityEngine;
using UnityEngine.Serialization;

namespace SatLight.Behaviours
{
    public class SatMapper : MonoBehaviour
    {
        [SerializeField]
        private SatGatherer satGatherer;

        [SerializeField]
        private GameObject satellitePrefab;

        [SerializeField]
        private Transform satellitesParent;

        [SerializeField]
        private Vector3 distanceFromParent = new (0.1f, 0.1f, 0.1f);

        private async void Awake()
        {
            var satellites = await satGatherer.GetSatellites();
            InstantiateSatellites(satellites);
        }

        private void InstantiateSatellites(SatAbove[] sats)
        {
            foreach (var satellite in sats)
            {
                Vector3 location = new Location(satellite.SatLat, satellite.SatLng, satellite.SatAlt)
                    .ToUnityCoordinates((satellitesParent ?? transform).localScale + distanceFromParent, satellitesParent.localRotation);

                var satGameObject = Instantiate(satellitePrefab, location, Quaternion.identity, satellitesParent ?? transform);
                var satBehaviour = satGameObject.GetComponent<SatelliteBehaviour>() ??
                                   satGameObject.AddComponent<SatelliteBehaviour>();
                
                satBehaviour?.SetSatelliteIno(satellite);
            }
        }
    }
}