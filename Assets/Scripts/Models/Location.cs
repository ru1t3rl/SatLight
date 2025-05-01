using System;
using UnityEngine;

namespace SatLight.Models
{
    [System.Serializable]
    public class Location
    {
        private const double FlatteningWGS84 = 1.0 / 298.257223563;

        private const double Wgs84SemiMajorAxis = 6378137.0;
        private const double Wgs84SemiMinorAxis = 6356752.314245;
        private const int ECEFReference = 6378137;

        public float Latitude { get; } = 0;
        public float Longitude { get; } = 0;
        public float Altitude { get; } = 0;

        public Location(float latitude, float longitude, float altitude = 0)
        {
            Latitude = latitude;
            Longitude = longitude;
            Altitude = altitude;
        }

        public static Vector3 ToEcefLocation(Location location)
        {
            Location locationInRadians = new Location(
                location.Latitude * Mathf.Deg2Rad,
                location.Longitude * Mathf.Deg2Rad,
                location.Altitude
            );

            double firstEccentricitySquared = 2 * FlatteningWGS84 - (FlatteningWGS84 * FlatteningWGS84);
            double primeVerticalRadiusOfCurvature = Wgs84SemiMajorAxis /
                                                    (Math.Sqrt(1 - firstEccentricitySquared *
                                                        (Math.Sin(locationInRadians.Latitude) * Math.Sin(locationInRadians.Latitude))));

            Vector3 ecefLocation = Vector3.zero;
            ecefLocation.x = (float)((primeVerticalRadiusOfCurvature + locationInRadians.Altitude) *
                                     Math.Cos(locationInRadians.Latitude) *
                                     Math.Cos(locationInRadians.Longitude));

            ecefLocation.y = (float)((primeVerticalRadiusOfCurvature + locationInRadians.Altitude) *
                                     Math.Cos(locationInRadians.Latitude) *
                                     Math.Sin(locationInRadians.Longitude));

            ecefLocation.z = (float)(((1 - firstEccentricitySquared) * 
                                      primeVerticalRadiusOfCurvature + locationInRadians.Altitude) *
                                     Math.Sin(locationInRadians.Latitude));
            
            return ecefLocation;
        }

        public Vector3 ToEcefLocation() => ToEcefLocation(this);

        /// <summary>
        /// Will map to a scale/range of -1 to 1 for all axis
        /// </summary>
        public Vector3 ToUnityCoordinates()
        {
            return ToUnityCoordinates(Vector3.one);
        }

        public Vector3 ToUnityCoordinates(Vector3 scale, Quaternion rotationOffset = default)
        {
            var ecefLocation = ToEcefLocation();

            var unityPosition = new Vector3(
                ecefLocation.x / ECEFReference * scale.x,
                ecefLocation.z / ECEFReference * scale.y,
                ecefLocation.y / ECEFReference * scale.z
            );

            return unityPosition;
        }
    }
}