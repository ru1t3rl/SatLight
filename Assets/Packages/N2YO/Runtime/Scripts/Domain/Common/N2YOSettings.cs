using UnityEngine;

namespace N2YO.Runtime.Domain
{
    [CreateAssetMenu(menuName = "N2YO/Settings", fileName = "N2YO_Settings")]
    public class N2YOSettings : ScriptableObject
    {
        [field: SerializeField]
        public string ApiUrl { get; private set; }
        
        [field: SerializeField]
        public string ApiKey { get; private set; }
    }
}