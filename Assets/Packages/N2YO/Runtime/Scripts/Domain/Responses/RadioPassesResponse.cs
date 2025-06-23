using System;
using N2YO.Runtime.Domain.Common;
using UnityEngine;

namespace N2YO.Runtime.Domain.Responses
{
    [Serializable]
    public class RadioPassesResponse
    {
        [field: SerializeField]
        public SatInfo Info { get; set; }
        
        [field: SerializeField] 
        public SatRadioPass[] Passes { get; set; }
    }
}