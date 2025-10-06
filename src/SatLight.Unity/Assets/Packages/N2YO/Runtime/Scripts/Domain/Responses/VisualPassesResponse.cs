using N2YO.Runtime.Domain.Common;
using UnityEngine;

namespace N2YO.Runtime.Domain.Responses
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