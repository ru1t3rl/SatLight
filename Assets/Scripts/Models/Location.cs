using System;
using UnityEngine;

namespace SatLight.Models
{
    [System.Serializable]
    public class Location
    {
        private const double Wgs84SemiMajorAxis = 6378137.0;
        private const double Wgs84SemiMinorAxis = 6356752.314245;

        private const double EccentricitySquared = ((Wgs84SemiMajorAxis * Wgs84SemiMajorAxis) -
                                                    (Wgs84SemiMinorAxis * Wgs84SemiMinorAxis)) /
                                                   (Wgs84SemiMajorAxis * Wgs84SemiMajorAxis);

        public float Latitude { get; } = 0;
        public float Longitude { get; } = 0;
        public float Altitude { get; } = 0;

        public Location(float latitude, float longitude, float altitude = 0)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        public static Vector3 ToECEFLocation(Location location)
        {
            location = new Location(
                location.Latitude * Mathf.Deg2Rad,
                location.Longitude * Mathf.Deg2Rad,
                location.Altitude
            );

            double radiusCurvature = Wgs84SemiMajorAxis / Mathf.Sqrt(
                1 -
                (float)EccentricitySquared *
                Mathf.Sin(location.Latitude) *
                Mathf.Sin(location.Latitude)
            );

            Vector3 ecefLocation = Vector3.zero;

            ecefLocation.x = (float)(radiusCurvature + location.Altitude) *
                             Mathf.Cos(location.Latitude) *
                             Mathf.Cos(location.Longitude);
            ecefLocation.y = (float)(radiusCurvature + location.Altitude) *
                             Mathf.Cos(location.Latitude) *
                             Mathf.Sin(location.Longitude);
            ecefLocation.z = (float)((Wgs84SemiMinorAxis * Wgs84SemiMinorAxis) /
                                     (Wgs84SemiMajorAxis * Wgs84SemiMajorAxis) *
                                     radiusCurvature +
                                     location.Altitude) *
                             Mathf.Sin(location.Latitude);

            return ecefLocation;
        }

        public Vector3 ToECEFLocation() => ToECEFLocation(this);

        /// <summary>
        /// Will map to a scale/range of -1 to 1 for all axis
        /// </summary>
        public Vector3 ToUnityCoordinates()
        {
            return ToUnityCoordinates(Vector3.one);
        }

        public Vector3 ToUnityCoordinates(Vector3 scale, Quaternion rotationOffset = default)
        {
            var location = ToECEFLocation();

            var unityPosition = new Vector3(
                location.x / 6400000f * scale.x,
                location.z / 6400000f * scale.y,
                location.y / 6400000f * scale.z
            );

            unityPosition = unityPosition;

            return unityPosition;
        }
    }
}