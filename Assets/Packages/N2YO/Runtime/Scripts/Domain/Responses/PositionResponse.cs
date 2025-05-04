using UnityEngine;

namespace SatLight.Models.Responses
{
    [System.Serializable]
    public class PositionResponse
    {
        [field: SerializeField] 
        public SatInfo Info { get; set; }
        
        [field: SerializeField] 
        public SatPosition[] Positions { get; set; }
    }
}