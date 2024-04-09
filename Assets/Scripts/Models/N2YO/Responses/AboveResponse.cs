using UnityEngine;

namespace SatLight.Models.Responses
{
    [System.Serializable]
    public class AboveResponse
    {
        [field: SerializeField]
        public SatInfoAboveRequest Info { get; set; }
        
        [field: SerializeField]
        public SatAbove[] Above { get; set; }
    }
}