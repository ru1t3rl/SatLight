using UnityEngine;

namespace SatLight.Models.Responses
{
    [System.Serializable]
    public class VisualPassesResponse
    {
        [field: SerializeField]
        public SatInfo Info { get; set; }
        
        [field: SerializeField]
        public VisualPassesResponse Passes { get; set; }
    }
}