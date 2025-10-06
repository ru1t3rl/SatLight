using TMPro;
using UnityEngine;

namespace StyleSmith.Runtime.Domain
{
    [System.Serializable]
    public class Typography
    {
        [field: SerializeField]
        public TMP_FontAsset Font { get; set; }
        
        [field: SerializeField]
        public float Size { get; set; }
    }
}