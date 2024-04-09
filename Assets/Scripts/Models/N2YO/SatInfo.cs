using UnityEngine;

namespace SatLight.Models
{
    [System.Serializable]
    public class SatInfo
    {
        /// <summary>
        /// NORAD id used in input
        /// </summary>
        [field: SerializeField]
        public int SatId { get; set; }
        
        /// <summary>
        /// Satellite name
        /// </summary>
        [field: SerializeField]
        public string SatName { get; set; }
        
        /// <summary>
        /// Count of transactions performed with the API key in last 60 minutes
        /// </summary>
        [field: SerializeField]
        public int TransactionCount { get; set; }
    }
}