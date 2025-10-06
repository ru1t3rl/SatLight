using N2YO.Runtime.Domain.Common;
using UnityEngine;

namespace N2YO.Runtime.Domain.Responses
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