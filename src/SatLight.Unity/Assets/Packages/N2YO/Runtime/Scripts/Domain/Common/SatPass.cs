using UnityEngine;

namespace N2YO.Runtime.Domain.Common
{
    [System.Serializable]
    public class SatPass
    {
        /// <summary>
        /// Satellite azimuth for the start of this pass (relative to the observer, in degrees)
        /// </summary>
        [field: SerializeField]
        public float StartAz { get; set; }
        
        /// <summary>
        /// Satellite azimuth for the start of this pass (relative to the observer). Possible values: N, NE, E, SE, S, SW, W, NW
        /// </summary>
        [field: SerializeField]
        public string StartAzCompass { get; set; }
        
        /// <summary>
        /// Satellite elevation for the start of this pass (relative to the observer, in degrees)
        /// </summary>
        [field: SerializeField]
        public float StartEl { get; set; }
        
        /// <summary>
        /// Unix time for the start of this pass. You should convert this UTC value to observer's time zone
        /// </summary>
        [field: SerializeField]
        public int StartUTC { get; set; }

        /// <summary>
        /// Satellite azimuth for the max elevation of this pass (relative to the observer, in degrees)
        /// </summary>
        [field: SerializeField]
        public float MaxAz { get; set; }
        
        /// <summary>
        /// Satellite azimuth for the max elevation of this pass (relative to the observer). Possible values: N, NE, E, SE, S, SW, W, NW
        /// </summary>
        [field: SerializeField]
        public string MaxAzCompass { get; set; }
        
        /// <summary>
        /// Satellite max elevation for this pass (relative to the observer, in degrees)
        /// </summary>
        [field: SerializeField]
        public float MaxEl { get; set; }
        
        /// <summary>
        /// Unix time for the max elevation of this pass. You should convert this UTC value to observer's time zone
        /// </summary>
        [field: SerializeField]
        public int MaxUTC { get; set; }
        
        /// <summary>
        /// Satellite azimuth for the end of this pass (relative to the observer, in degrees)
        /// </summary>
        [field: SerializeField]
        public float EndAz { get; set; }
        
        /// <summary>
        /// 	Satellite azimuth for the end of this pass (relative to the observer). Possible values: N, NE, E, SE, S, SW, W, NW
        /// </summary>
        [field: SerializeField]
        public string EndAzCompass { get; set; }
        
        /// <summary>
        /// Satellite elevation for the end of this pass (relative to the observer, in degrees)
        /// </summary>
        [field: SerializeField]
        public float EndEl { get; set; }
        
        /// <summary>
        /// Unix time for the end of this pass. You should convert this UTC value to observer's time zone
        /// </summary>
        [field: SerializeField]
        public int EndUTC { get; set; }
        
        /// <summary>
        /// Max visual magnitude of the pass, same scale as star brightness. If magnitude cannot be determined, the value is 100000
        /// </summary>
        [field: SerializeField]
        public float Mag { get; set; }
        
        /// <summary>
        /// Total visible duration of this pass (in seconds)
        /// </summary>
        [field: SerializeField]
        public int Duration { get; set; }
    }
}