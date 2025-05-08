using UnityEngine;

namespace SatLight.Models
{
    [System.Serializable]
    public class SatAbove
    {
        /// <summary>
        /// Satellite NORAD id
        /// </summary>
        [field: SerializeField]
        public int SatId { get; set; }
        
        /// <summary>
        /// Satellite name
        /// </summary>
        [field: SerializeField]
        public string SatName { get; set; }
        
        /// <summary>
        /// Satellite international designator
        /// </summary>
        [field: SerializeField]
        public string IntDesignator { get; set; }
        
        /// <summary>
        /// Satellite launch date (YYYY-MM-DD)
        /// </summary>
        [field: SerializeField]
        public string LaunchDate { get; set; }
        
        /// <summary>
        /// Satellite footprint latitude (decimal degrees format)
        /// </summary>
        [field: SerializeField]
        public double SatLat { get; set; }
        
        /// <summary>
        /// Satellite footprint longitude (decimal degrees format)
        /// </summary>
        [field: SerializeField]
        public double SatLng { get; set; }
        
        /// <summary>
        /// Satellite altitude (km)
        /// </summary>
        [field: SerializeField]
        public double SatAlt { get; set; }
    }
}