using UnityEngine;

namespace SatLight.Models
{
    [System.Serializable]
    public class SatPosition
    {
        /// <summary>
        /// Satellite footprint latitude (decimal degrees format) 
        /// </summary>
        [field: SerializeField]
        public float SatLatitude { get; set; }
        
        /// <summary>
        /// Satellite footprint longitude (decimal degrees format)
        /// </summary>
        [field: SerializeField]
        public float SatLongitude { get; set; }
        
        /// <summary>
        /// Satellite azimuth with respect to observer's location (degrees)
        /// </summary>
        [field: SerializeField]
        public float Azimuth { get; set; }
        
        /// <summary>
        /// Satellite elevation with respect to observer's location (degrees)
        /// </summary>
        [field: SerializeField]
        public float Elevation { get; set; }
        
        /// <summary>
        /// Satellite right ascension (degrees)
        /// </summary>
        [field: SerializeField]
        public float Ra { get; set; }
        
        /// <summary>
        /// Satellite declination (degrees)
        /// </summary>
        [field: SerializeField]
        public float Dec { get; set; }
        
        /// <summary>
        /// Unix time for this position (seconds). You should convert this UTC value to observer's time zone
        /// </summary>
        [field: SerializeField]
        public uint Timestamp { get; set; }
    }
}