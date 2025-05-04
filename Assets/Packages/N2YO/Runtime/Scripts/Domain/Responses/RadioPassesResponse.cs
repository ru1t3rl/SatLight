using Unity.VisualScripting;
using UnityEngine;

namespace SatLight.Models.Responses
{
    [System.Serializable]
    public class RadioPassesResponse
    {
        [field: SerializeField]
        public SatInfo Info { get; set; }
        
        [field: SerializeField] 
        public SatRadioPass[] Passes { get; set; }
    }
}